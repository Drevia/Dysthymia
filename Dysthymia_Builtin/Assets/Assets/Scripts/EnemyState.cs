using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState
{
    public bool joueurVu;

            
    public AlertState alerteState = AlertState.Patrolling;
    public float distanceToPlayer;
    public Vector3 lastSeenPosition;


        
}

public enum AlertState
        {
            Patrolling,
            Alert,
            Fighting
        }
