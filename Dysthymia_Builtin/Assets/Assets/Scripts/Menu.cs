using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnButtonPlay()
    {
        SceneManager.LoadSceneAsync("PlayScene");
        SceneManager.UnloadSceneAsync("MenuPrincipal");
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }


}
