using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class PlayerFollow : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 _cameraOffSet;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    public bool rotateAroundPlayer = true;

    

    public float rotationsSpeed = 3.0f;

    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);      
    public Vector3 camOffset = new Vector3(0.4f, 0.5f, -2.0f);
    private Transform cam;

    public bool lookAtPlayer = false;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = transform;
        cam.position = playerTransform.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        cam.rotation = Quaternion.identity;
    }

    void Start()
    {
        _cameraOffSet = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = playerTransform.position + _cameraOffSet;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationsSpeed, Vector3.up);

        _cameraOffSet = camTurnAngle * _cameraOffSet;

        if (lookAtPlayer || rotateAroundPlayer)
        {
            transform.LookAt(playerTransform);
        }
    }
}
