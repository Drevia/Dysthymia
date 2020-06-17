using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class MoveCharacter : MonoBehaviour
{
    public static MoveCharacter instance;
    CharacterController characterController;

    public float walkSpeed = 6.0f;
    public float runSpeed = 10f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float rotationSpeed = 3.0f;
    GameManager gameManager;

    [Header("Camera")]
    public Vector3 camForward;
    public Vector3 camRight;
    public Transform cam;

    public Vector3 moveDirection = Vector3.zero;
    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 mouseDelta = Vector3.zero;
    public Animator anim;
    float h, v;
    public bool isGrounded;

    void OnEnable()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;

        Debug.Log("Enable");
    }

    public bool isRunning = false;
    public bool canPlay = true;
    public bool isDead;
    void Update()
    {
        if (GameManager.gameIsPaused)
            return;
        if (GameManager._instance.isHidden)
            return;
        if (canPlay || GameManager._instance.inCinematic == true)
        {
            isGrounded = characterController.isGrounded;
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            if (isGrounded)
            {

                // We are grounded, so recalculate
                // move direction directly from axes


                //moveDirection = new Vector3(h, 0, v);
                camForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up);

                camRight = Vector3.ProjectOnPlane(cam.right, Vector3.up);
                moveDirection = camRight.normalized * h + camForward.normalized * v;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }

                if (Input.GetButton("Fire3"))
                {
                    moveDirection *= runSpeed;
                    anim.SetBool("IsRunning", true);
                }
                else
                {
                    moveDirection *= walkSpeed;
                    anim.SetBool("IsRunning", false);
                }

                anim.SetFloat("ForwardSpeed", Vector3.Dot(transform.forward, characterController.velocity));

                
            }


            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            OrientToSpeed();



            mouseDelta = Input.mousePosition - lastMousePosition;

            #region Crounch
            /*if (Input.GetKeyDown(KeyCode.C))
            {
                capsCollider.direction = 2;
                meshPlayer.transform.rotation = Quaternion.FromToRotation(new Vector3(transform.position.x, transform.position.y, transform.position.z),new Vector3(0f,0f,0f));
                capsCollider.center = new Vector3(0f, -0.83f, 0.62f);

            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                capsCollider.direction = 1;
                //meshPlayer.transform.rotation = Quaternion.Euler(-90, 0, 0);
                capsCollider.center = new Vector3(0f, -0.23f, -0.08f);
            }*/

            #endregion

            //transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        }
        
    }


    public void DeathByMonster(Transform monster)
    {
        anim.SetTrigger("isTouched");
        canPlay = false;
    }


    void OrientToSpeed()
    {

        Vector3 lookRot = characterController.velocity;
        lookRot.y = 0;
        if (lookRot.sqrMagnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(lookRot);
            //transform.rotation = Quaternion.LookRotation(lookRot);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
    }

    public void IsDead()
    {
        anim.SetBool("IsKilled", true);
    }




    /*public void PlayerInCinematic(bool cinematic)
    {
        cinematic = GameManager._instance.inCinematic;
    }*/

    public void PlayerInCinematic()
    {
        canPlay = !canPlay;
    }

}
