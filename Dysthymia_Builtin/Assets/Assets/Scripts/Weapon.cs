using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool attack;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        attack = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            attack = true;
            anim.SetBool("Weapon", true);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            attack = false;
            anim.SetBool("Weapon", false);
        }


    }

    public void AnimationEnded()
    {
        anim.SetBool("Weapon", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ennemi"))
        {
            Debug.Log("destroy");
            Destroy(other.gameObject);
        }
    }
}
