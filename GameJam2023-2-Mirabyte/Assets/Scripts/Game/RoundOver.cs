using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundOver : MonoBehaviour
{
    Save save;
    public PauseMenu pause;
    public GameObject shopPanel;
    public GameObject hospitalPanel;
    public GameObject hospitalPanelImage;
    public GameObject endPanel;
    public GameObject endPanelImage;

    public Sprite boyEnd; 
    public Sprite girlEnd; 
    public Sprite boyHospital; 
    public Sprite girlHospital;
    private void Start()
    {
        save = gameObject.GetComponent<Save>();
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

    public void NextPanel()
    {
        if(save.map == 22)
        {
            endPanel.gameObject.SetActive(true);
            if(save.character == "boy")
            {
                endPanelImage.GetComponent<Image>().sprite = boyEnd;
            }
            else
            {
                endPanelImage.GetComponent<Image>().sprite = girlEnd;
            }
            save.DeleteSave();
        }
        else
        {
            hospitalPanel.gameObject.SetActive(false);
            save.SaveGame();
        }
    }


}
