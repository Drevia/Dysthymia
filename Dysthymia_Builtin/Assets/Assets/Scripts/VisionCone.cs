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
    private void Start()
    {
        visionCone = GetComponent<MeshCollider>();
        if (visionOrigin == null)
            visionOrigin = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir = other.transform.position - visionOrigin.transform.position;
            dir.y = 0;
            Ray ray = new Ray(visionOrigin.transform.position, dir);

            //Debug.Log(ray.ToString());
            if (Physics.Raycast(ray, dir.magnitude, groundMask))
            {
                isSeeingPlayer = false;
            }
            else
            {
                isSeeingPlayer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isSeeingPlayer = false;
    }
}
