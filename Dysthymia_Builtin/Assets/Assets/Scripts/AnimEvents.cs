using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{

    public void PlayFootStep()
    {
        //Debug.Log("FootStep");
        SoundManager.instance.Play("Walk");
    }


}
