﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Play");

        }
    }
}
