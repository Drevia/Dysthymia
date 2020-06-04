using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChangePostProcess : MonoBehaviour
{

    public PostProcessProfile firstPlayer;
    public PostProcessProfile secondPlayer;

    PostProcessProfile ppp;
    private void Start()
    {
        ppp = firstPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (ppp == firstPlayer)
            {
                 ppp = GetComponent<PostProcessVolume>().profile = secondPlayer;

            }
            else if (ppp == secondPlayer)
            {
                ppp = GetComponent<PostProcessVolume>().profile = firstPlayer;
            }
        }
    }
}
