using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitZone : MonoBehaviour
{
    EnemyAI aiScript;
    MoveCharacter mc;
    GameObject actualEnemy;

    bool _locked;
    bool locked
    {
        get
        {
            if (aiScript == null)
                _locked = false;
            return _locked;
        }
        set {
            _locked = value;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitZone") && !locked)
        {
            aiScript = other.GetComponentInParent<EnemyAI>();
            UIManager._instance.ToggleEnemyKillZone(true);
            actualEnemy = other.GetComponentInParent<EnemyAI>().gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyHitZone") && !locked)
        {
            aiScript = null;
            UIManager._instance.ToggleEnemyKillZone(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyHitZone"))
        {
            if (Input.GetMouseButtonDown(0) && aiScript)
            {
                Debug.Log("Kill");
                locked = true;
                aiScript.StopMove();
                // Empecher de controler ton perso (can play false ?)
                KillEnemy();
                //mc.canPlay = false;
                
            }
        }
    }

    public void KillEnemy()
    {
        // FX Couteau
        // Lancer anim Mort enemy
        aiScript.GetComponent<Animator>().SetTrigger("isKilled");
        mc.anim.SetTrigger("KillAnim");
        aiScript.Die();
    }


    public void EndKilling()
    {
        locked = false;
        // Redonner controle au joueur (can play true ?)
        mc.canPlay = true;
        
    }

    private void Start()
    {
        mc = GetComponent<MoveCharacter>();
    }
}
