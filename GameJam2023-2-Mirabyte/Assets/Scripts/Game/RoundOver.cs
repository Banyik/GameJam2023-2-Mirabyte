using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class RoundOver : MonoBehaviour
{
    public Save save;
    public PauseMenu pause;
    public Shop shop;
    public GameObject shopPanel;
    public GameObject hospitalPanel;
    public GameObject hospitalPanelImage;
    public GameObject endPanel;
    public GameObject endPanelImage;
    public TextMeshProUGUI endScoreText;

    public Sprite boyEnd; 
    public Sprite girlEnd; 
    public Sprite boyHospital; 
    public Sprite girlHospital;
    private void Start()
    {
        save = gameObject.GetComponent<Save>();
    }

    public void ShowShopPanel()
    {
        shopPanel.gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerAudioHandler>().PlayShopMusic();
        shop.RefreshButtons();
    }

    public void ShowHospitalPanel()
    {
        pause.PauseOnly();
        hospitalPanel.gameObject.SetActive(true);
        if (save.character == "boy")
        {
            hospitalPanelImage.GetComponent<Image>().sprite = boyHospital;
        }
        else
        {
            hospitalPanelImage.GetComponent<Image>().sprite = girlHospital;
        }
    }
    public void NextFromShopPanel()
    {
        save.SaveGame();
        GameObject.Find("Player").transform.position = new Vector2(0, 2);
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().Refresh();
        gameObject.GetComponent<ThiefSpawner>().KillAll();
        shopPanel.SetActive(false);
        pause.ResumeOnly();
    }
    public void NextPanel()
    {
        if(save.map + 1 >= 22)
        {
            shopPanel.SetActive(false);
            hospitalPanel.gameObject.SetActive(false);
            endPanel.gameObject.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerAudioHandler>().PlayEndMusic();
            if (save.character == "boy")
            {
                endPanelImage.GetComponent<Image>().sprite = boyEnd;
            }
            else
            {
                endPanelImage.GetComponent<Image>().sprite = girlEnd;
            }
            endScoreText.text = $"Elfogott tolvajok: {GameObject.Find("Player").GetComponent<PlayerBehaviour>().thiefStunned}";
            save.DeleteSave();
        }
        else
        {
            hospitalPanel.gameObject.SetActive(false);
            save.SaveGame();
            ShowShopPanel();
        }
    }


}
