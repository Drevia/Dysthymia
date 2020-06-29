using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    EnemyAI ai;
    public AlertState allState;
    public float DistMin = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("DestReach", false);
        ai = animator.GetComponent<EnemyAI>();
        ai.WalkToNextPatrolPoint();
        ai.agent.acceleration = 1f;
        ai.agent.speed = 1.5f;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ai.agent.remainingDistance <= DistMin)
        {
            ai.agent.isStopped = true;
            animator.SetBool("DestReach", true);
        }
        else if (allState == AlertState.Alert)
        {
            ai.agent.SetDestination(ai.state.lastSeenPosition);
        }

        animator.SetFloat("DistanceToDestination", ai.agent.remainingDistance);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai.agent.isStopped = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
