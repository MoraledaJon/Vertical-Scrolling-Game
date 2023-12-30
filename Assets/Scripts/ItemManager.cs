using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Transform ItemCanvasParent;

    [Header("Refernces for making the line half the size")]
    public GameObject HalfItem_CanvasPrefab;
    public LineDrawer lineDrawer;
    public int secondsToLast;
    private bool half_Line_Playing;
    private int seconds;
    private float lineNormal;
    private GameObject cooldownGameObject;
    public TextMeshProUGUI currentCoinText;
  


    private void Start()
    {
        lineNormal = lineDrawer.maxLineLength;
        seconds = secondsToLast;

        CoinManager.Instance.ResetCoin();
        currentCoinText.text = CoinManager.Instance.GetCurrentGameCoins().ToString();
    }

    public void Turbo_Item(Rigidbody2D rb, float bounceForceTurbo, GameObject collisionObject)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity
        rb.AddForce(Vector2.up * bounceForceTurbo, ForceMode2D.Impulse);

        StartCoroutine(Turbo());

        // Destroy the collisionObject
        Destroy(collisionObject);
    }

    IEnumerator Turbo()
    {
        GameObject[] blackClouds = GameObject.FindGameObjectsWithTag("BlackCloud");

        foreach (GameObject blackCloud in blackClouds)
        {
            PolygonCollider2D polygonCollider = blackCloud.GetComponent<PolygonCollider2D>();
            if (polygonCollider != null)
            {
                polygonCollider.isTrigger = true;
            }
        }

        yield return new WaitForSeconds(1.5f);

        foreach (GameObject blackCloud in blackClouds)
        {
            if(blackCloud)
            {
                PolygonCollider2D polygonCollider = blackCloud.GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    polygonCollider.isTrigger = false;
                }
            }
        }
    }
    public void Coin_Item(GameObject collisionObject)
    {
        CoinManager.Instance.AddCoin();

        int currentCoins = CoinManager.Instance.GetCurrentGameCoins();

        if (currentCoins > 9999)
        {
            float thousands = currentCoins / 1000f;
            currentCoinText.text = thousands.ToString("0.#") + "k"; // Formats to one decimal place, e.g., 10.5k
        }
        else
        {
            currentCoinText.text = currentCoins.ToString();
        }

        Destroy(collisionObject);
    }

    public void Half_Line_Item(GameObject collisionObject)
    {
        Destroy(collisionObject);
        if (half_Line_Playing)
        {
            StopAllCoroutines();
            StartCoroutine(Half_Time_Enumerator());
        }
        else
        {
            StartCoroutine(Half_Time_Enumerator());
        }
    }

    IEnumerator Half_Time_Enumerator()
    {
        half_Line_Playing = true;

        lineDrawer.maxLineLength = lineNormal;

        lineDrawer.maxLineLength = lineNormal / 2;

        secondsToLast = seconds;

        if(cooldownGameObject)
        {
            Destroy(cooldownGameObject);
            cooldownGameObject = Instantiate(HalfItem_CanvasPrefab, ItemCanvasParent);
        }
        else
        {
            cooldownGameObject = Instantiate(HalfItem_CanvasPrefab, ItemCanvasParent);
        }
       
        while (secondsToLast >= 0)
        {
            cooldownGameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = secondsToLast.ToString();

            yield return new WaitForSeconds(1f);

            secondsToLast--;
        }

        Destroy(cooldownGameObject);

        lineDrawer.maxLineLength = lineNormal;

        secondsToLast = seconds;

        half_Line_Playing = false;
    }
}
