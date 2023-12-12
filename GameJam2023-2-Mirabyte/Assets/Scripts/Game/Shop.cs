using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using TMPro;

public class Shop : MonoBehaviour
{
    public PlayerBehaviour player;
    public Save save;
    public Points points;
    public Button Baton;
    public Button CandyCane;
    public Button Launcher;
    public Button Cevlar;
    public TextMeshProUGUI buyPoints;

    private void Update()
    {
        buyPoints.text = $"Vásárlási pontok: {points.point}";
        Invoke(nameof(DisableButtons), 1);
        if(points.point > 0)
        {
            if(player.shield < 3 && points.point >= 1)
            {
                Cevlar.interactable = true;
            }
            if(save.weapon >= 0 && points.point >= 3)
            {
                Baton.interactable = true;
            }
            else if (save.weapon >= 1 && points.point >= 6)
            {
                CandyCane.interactable = true;
            }
            else if (save.weapon >= 2 && points.point >= 9)
            {
                Launcher.interactable = true;
            }
        }
    }
    public void ShopStart()
    {
        DisableButtons();
    }

    public void RefreshButtons()
    {
        if (player.shield < 3 && points.point >= 1)
        {
            Cevlar.interactable = true;
        }
        if (save.weapon >= 0 && points.point >= 3)
        {
            Baton.interactable = true;
        }
        if (save.weapon >= 1 && points.point >= 6)
        {
            CandyCane.interactable = true;
        }
        if (save.weapon >= 2 && points.point >= 9)
        {
            Launcher.interactable = true;
        }
    }
    void DisableButtons()
    {
        Cevlar.interactable = false;
        Baton.interactable = false;
        CandyCane.interactable = false;
        Launcher.interactable = false;
    }
    public void BuyCevlar()
    {
        if (player.shield < 3)
        {
            player.shield++;
            points.point--;
            RefreshButtons();
        }
    }

    public void BuyBaton()
    {
        if(save.weapon == 0)
        {
            save.weapon = 1;
            points.point -= 3;
            RefreshButtons();
        }
    }

    public void BuyCandyCane()
    {
        if (save.weapon == 1)
        {
            save.weapon = 2;
            points.point -= 6;
            RefreshButtons();
        }
    }

    public void BuyLauncher()
    {
        if (save.weapon == 2)
        {
            save.weapon = 3;
            points.point -= 9;
            RefreshButtons();
        }
    }
}
