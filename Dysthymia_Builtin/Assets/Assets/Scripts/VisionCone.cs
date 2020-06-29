using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{


    MeshCollider visionCone;

    public bool isSeeingPlayer = false;

    public LayerMask groundMask;
    public Transform visionOrigin;
    public Vector3 lastSeenPosition;
    public EnemyAI ai;
    public BoxCollider killTrigger;
    private void Start()
    {
        visionCone = GetComponent<MeshCollider>();
        if (visionOrigin == null)
            visionOrigin = transform;

        ai = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerStay(Collider other)
    {
        
            if (other.CompareTag("Player") && !killTrigger)
            {

                Vector3 dir = other.transform.position + Vector3.up - transform.position;
                Ray ray = new Ray(visionOrigin.transform.position, dir);


                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, dir.magnitude + 10, groundMask))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        ai.state.lastSeenPosition = other.transform.position;
                        ai.state.joueurVu = true;
                        Debug.Log("touché");
                    }
                    else
                    {
                        ai.state.joueurVu = false;
                        Debug.Log("touch" + other.tag);
                    }
                }
                else
                {

                    ai.state.joueurVu = false;
                }

            }
        
    }

    private void OnTriggerExit(Collider other)
    {
        isSeeingPlayer = false;
    }
}
