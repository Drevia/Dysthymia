using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public GameObject GoImage;

    public Image imageDossier;

    public DisplayDossier display;

    //public Document_Information docInfo;

    public GameObject panelPorte;

    public GameObject panelCasier;

    public GameManager gameManager;

    public GameObject keyPanel;

    public GameObject killEnemy;

    private void Start()
    {
        _instance = this;
    }



    public void ReadDossier()
    {
        print("Ceci est un dossier");
    }

    public void NearToDossier()
    {
        if (InventoryPlayer.instance.isReading == false)
        {
            //ON recupere la variable displayInfo de InventoryPlayer
            display = InventoryPlayer.instance.displayInfo;
            //L'image de imageDossier est celle recupérer dans la variable display
            display.ReadMe();
            GoImage.SetActive(true);
            InventoryPlayer.instance.isReading = true;
        }
        else
        {
            GoImage.SetActive(false);
            InventoryPlayer.instance.isReading = false;
        }

    }

    public void ToggleEnemyKillZone(bool isActive)
    {
        if (killEnemy)
        {
            killEnemy.SetActive(isActive);
        }
    }

    bool inTrigger = false;
    public bool isDoor = false;
    public bool isCasier = false;
    public void InteractionTrigger()
    {
        if (isDoor)
        {
            panelPorte.SetActive(true);
        }
        else if (isCasier)
        {

            panelCasier.SetActive(true);

        }
    }

    public void ShowPanel(string PanelName)
    {
        switch (PanelName)
        {
            case "Porte":
                panelPorte.SetActive(true);
                break;
            case "Casier":
                panelPorte.SetActive(true);
                break;
            case "Clé":
                panelPorte.SetActive(true);
                break;
            default:
                break;

        }
    }
    public void HidePanel(string PanelName)
    {
        switch (PanelName)
        {
            case "Porte":
                panelPorte.SetActive(false);
                break;
            case "Casier":
                panelPorte.SetActive(false);
                break;
            case "Clé":
                panelPorte.SetActive(false);
                break;
            default:
                break;

        }
    }
}
