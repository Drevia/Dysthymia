using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDossier : MonoBehaviour
{
    public Document_Information docInfo;
    public Image image;

   
    public void ReadMe()
    {
        image.sprite = docInfo.dossier; 
    }

    
}
