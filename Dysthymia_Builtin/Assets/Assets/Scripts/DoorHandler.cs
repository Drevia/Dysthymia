using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private Animator _animator = null;
    public bool openAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();  
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (openAnim == false)
            {
                openAnim = true;
                _animator.SetBool("Open", true);
            }

            if (openAnim == true)
            {
                openAnim = false;
                _animator.SetBool("Open", false);
            }
        }
    }

    
}
