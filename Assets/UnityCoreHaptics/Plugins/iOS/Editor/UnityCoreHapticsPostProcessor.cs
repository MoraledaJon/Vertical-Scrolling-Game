﻿#if UNITY_IOS
using System;
using System.IO;

using UnityEngine;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class UnityCoreHapticsPostProcessor
{
  // Set to path that this script is in
  const string MODULE_MAP_FILENAME = "module.modulemap";

  [PostProcessBuild]
  public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
  {
    if (buildTarget == BuildTarget.iOS)
    {
      var pbxProjectPath = PBXProject.GetPBXProjectPath(buildPath);
      var proj = new PBXProject();
      proj.ReadFromFile(pbxProjectPath);

      string targetGUID = proj.GetUnityFrameworkTargetGuid();

      // Get relative path of the plugin the from Assets folder
      // Should be something like "UnityCoreHaptics/Plugins/iOS/UnityCoreHaptics/Source"
      var pluginRelativePathInUnity = GetPluginPathRelativeToAssets();

      // Get relative path of the plugin in XCode
      string pluginRelativePathInXCode = Path.Combine("Libraries", pluginRelativePathInUnity);

      proj.AddFrameworkToProject(targetGUID, "CoreHaptics.framework", false);
      proj.AddBuildProperty(targetGUID, "SWIFT_VERSION", "5.0");
      proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");

      proj.AddBuildProperty(targetGUID, "CLANG_ENABLE_MODULES", "YES");
      proj.AddBuildProperty(targetGUID, "SWIFT_INCLUDE_PATHS", pluginRelativePathInXCode);
      proj.AddBuildProperty(targetGUID, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

      WriteModuleToFramework(proj, targetGUID, pbxProjectPath, pluginRelativePathInUnity, pluginRelativePathInXCode);
    }
  }
  
  // Made to check if two paths are the same
  // Based on this: https://stackoverflow.com/questions/2281531/how-can-i-compare-directory-paths-in-c
  private static string _normalizePath(string path)
  {
      return Path.GetFullPath(new Uri(path).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                .ToUpperInvariant();
  }

  private static string _getRelativePath(string basePath, string fullPath) {
    // Base case 1: not found
    if (fullPath == null || fullPath == "") {
      return null;
    }
    // Base case 2: found
    if (_normalizePath(fullPath) == _normalizePath(basePath)) {
      return "";
    }
    // Recursive case
    var dirPath = Path.GetDirectoryName(fullPath);
    return Path.Combine(_getRelativePath(basePath, dirPath), Path.GetFileName(fullPath));
  }

  private static string GetPluginPathRelativeToAssets() {
      string[] files = System.IO.Directory.GetFiles(Application.dataPath, "UnityCoreHapticsProxy.cs", SearchOption.AllDirectories);
      if (files.Length != 1) {
        throw new Exception("[UnityCoreHapticsPostProcessor] Error: there should exactly be one file named UnityCoreHapticsProxy.cs");
      }
      return Path.GetDirectoryName(_getRelativePath(Application.dataPath, files[0]));
  }

  private static void WriteModuleToFramework(
    PBXProject proj,
    string targetGUID,
    string pbxProjectPath,
    string pluginRelativePathInUnity,
    string pluginRelativePathInXCode
  )
  {
    // Add a module map reference to the XCode project
    string moduleMapDestRelativePath = Path.Combine(pluginRelativePathInXCode, MODULE_MAP_FILENAME);
    Debug.Log("[UnityCoreHapticsPostProcessor] Adding properties to XCode framework. Module path : " + moduleMapDestRelativePath);
    string file_guid = proj.AddFile(moduleMapDestRelativePath, moduleMapDestRelativePath, PBXSourceTree.Source);
    proj.AddFileToBuild(targetGUID, file_guid);
    proj.WriteToFile(pbxProjectPath);

    // Copy the module file from Unity to XCode
    string sourcePath = Path.Combine(Application.dataPath, pluginRelativePathInUnity, MODULE_MAP_FILENAME);
    string destPath = Path.Combine(Path.GetDirectoryName(pbxProjectPath), "..", moduleMapDestRelativePath);
    if (!Directory.Exists(Path.GetDirectoryName(destPath)))
    {
      Debug.Log("[UnityCoreHapticsPostProcessor] Creating directory " + destPath);
      Directory.CreateDirectory(Path.GetDirectoryName(destPath));
    }
    Debug.Log("[UnityCoreHapticsPostProcessor] Copy module file to project : " + sourcePath + " -> " + destPath);
    File.Copy(sourcePath, destPath);
  }
}
#endif
