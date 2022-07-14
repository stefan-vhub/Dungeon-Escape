using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UI Manager is Null!");
            }
            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImg;
    public TextMeshProUGUI gemCountText;
    public Image[] healthBars;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int liveRemaining)
    {
        for (int i = 0; i <= liveRemaining; i++)
        {
            if(i==liveRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
