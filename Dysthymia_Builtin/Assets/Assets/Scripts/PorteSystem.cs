using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteSystem : MonoBehaviour
{

    public static PorteSystem instance;
    MeshRenderer mesh;
    Animator animator;
    Quaternion originalRotation;
    

    private void Awake()
    {
        this.originalRotation = this.transform.rotation;
    }

    private void Start()
    {
        instance = this;
        mesh = gameObject.GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>();
        
    }
    
    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Verification");
        //InventoryPlayer.instance.CheckOpenDoor();
        /*if (other.CompareTag("ennemi") || GameManager._instance.gotKey == true)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Ouverture impossible");
            return;
        }*/
        if (other.CompareTag("Player"))
        {
           // OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Open", false);

    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true); 
    }


    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}
