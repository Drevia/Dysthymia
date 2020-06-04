using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 4;
    public float maxDist = 10;
    public float minDist = 5;
    public EnemyState state;

    public NavMeshAgent agent;
    CapsuleCollider col;
    public LayerMask playerWeaponLayer, groundMask;
    VisionCone cone;

    public Transform coneOrigin;



    #region AI ennemi

    public float walkingDist;
    public Transform[] waypoints;
    public int currentWaypoint;
    public float startWaitTime;
    private Animator anim;
    private Vector3 direction;
    public float attackDistance;
    private Vector3 smoothVelocity = Vector3.zero;
    private float smoothTime = 10.0f;
    public BoxCollider killTrigger;

    #endregion

    public bool isKilled = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        cone = GetComponentInChildren<VisionCone>();
        


    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        cone = GetComponentInChildren<VisionCone>();
        WalkToNextPatrolPoint();
        anim.SetTrigger("IsEnabled");
    }

    // Update is called once per frame
    void Update()
    {

        /*if (cone.isSeeingPlayer == true)
        {
            //agent.SetDestination()
            //transform.LookAt(player);
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            Debug.Log("joueur vu");
            transform.LookAt(player);

            if (Vector3.Distance(transform.position, player.position) <= walkingDist)
            {
                direction = player.position - transform.position;
                direction.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.9f * Time.deltaTime);

                transform.Translate(0, 0, moveSpeed);

                anim.SetBool("IsWalking", true);
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsPatrol", false);
            
                if (direction.magnitude <= attackDistance)
                {
                    Debug.Log("attack");
                
                }
            }

        }*/
        


        if (anim.GetBool("IsPatrol") == true)
        {
            WalkToNextPatrolPoint();
            agent.stoppingDistance = 0.4f;
        }

        SetAnimator();

        if (state.joueurVu == true)
        {
            anim.SetBool("IsPatrol", false);
            agent.isStopped = false;

            ChasePlayer();

        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // Avoid any reload.
            Debug.Log("t'es mort");
        }
    }

    //fonction pour quand l'ennemy a detecté le joueur, le suit
    public void ChasePlayer()
    {
        /*
        if (Vector3.Distance(transform.position, player.position) >= minDist)
        {
            //On lui dit d'avancer selon sa vitesse et le temps
            transform.position = Vector3.SmoothDamp(transform.position, player.position,ref smoothVelocity, smoothTime);
        }
        else if (Vector3.Distance(transform.position, player.position) <= minDist)
        {
            Debug.Log("Attack");
        }*/
        agent.SetDestination(player.position);
        agent.stoppingDistance = 1.2f;
        agent.speed = moveSpeed;
    }

    public void SetAnimator()
    {
        anim.SetBool("PlayerDetect", state.joueurVu);
        anim.SetFloat("PlayerDistance", Vector3.Distance(state.lastSeenPosition, transform.position));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            // L'arme du player le touche
            Debug.Log("Hit from Player");
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !killTrigger)
        {

            Vector3 dir = other.transform.position + Vector3.up - coneOrigin.transform.position;
            Ray ray = new Ray(coneOrigin.transform.position, dir);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, dir.magnitude + 10, groundMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    state.lastSeenPosition = other.transform.position;
                    state.joueurVu = true;
                }
                else
                {
                    state.joueurVu = false;
                }
            }
            else
            {
                
                state.joueurVu = false;
            }

        }
    }

    public void WalkToNextPatrolPoint()
    {
        if (state.alerteState == AlertState.Patrolling)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            agent.isStopped = false;
            agent.SetDestination(waypoints[currentWaypoint].position);


        }
        else
        {
            agent.SetDestination(state.lastSeenPosition);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state.joueurVu = false;
        }
    }

    public void StopMove()
    {
        agent.isStopped = true;
    }

    public void Die()
    {
        // Lancer anim mort
        anim.SetTrigger("isKilled");
    }


    public void SetPatrolPoint(int patrolIndex)
    {
        if (GameManager._instance.patrolRoutes.Length > patrolIndex)
            waypoints = GameManager._instance.patrolRoutes[patrolIndex].waypoints;
    }
    public void SetPatrolPoint(string patrolName)
    {
        foreach (PatrolRoute p in GameManager._instance.patrolRoutes)
        {
            if (p.routeName == patrolName)
            {
                waypoints = p.waypoints;
                break;
            }
        }
    }

}
/*
 * 
public Transform patrolRoute;
 public int currentPatrolPoint;
public Transform[] patrolPoints;
public LayerMask groundMask;
 public MeshCollider visionCone;
public NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		patrolPoints = patrolRoute.GetComponentsInChildren<Transform>();
		agent = GetComponent<NavMeshAgent>();
	}
	private void OnTriggerStay(Collider other)
    {
        Vector3 dir = other.transform.position + Vector3.up - visionCone.transform.position;
        Ray ray = new Ray(visionCone.transform.position, dir);
        if (other.CompareTag("Player"))
        {

            if (Physics.Raycast(ray, dir.magnitude, groundMask))
            {
                states.isSeeingPlayer = false;
			}
                else
                {
                    states.lastPosition = other.transform.position;
                    states.isSeeingPlayer = true;
                }
            
        }
    }
	private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //le personnage n'est plus vu mais on garde la derniere position connu

            states.isSeeingPlayer = false;
        }
    }
	// Update is called once per frame
	void Update () {
		setAnimator();
	}
	
	void setAnimator()
	{
		anim.SetBool("playerVisible", states.isSeeingPlayer);
		anim.SetFloat("distToPlayer",Vector3.Distance(states.lastPosition, transform.position));
	}
}

 */
