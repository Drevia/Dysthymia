using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    /*void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //on veut passer a la scene 2 si on est sur la 1 et inversement
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                StartCoroutine(LoadLevel(2));
            }
            else
            {
                StartCoroutine(LoadLevel(1));
            } 

             
        } 
    }*/

    public void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadLevel(2));
        }
        else
        {
            StartCoroutine(LoadLevel(1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
    }
}
