using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectRotation : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.DORotate(transform.position, 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }
}
