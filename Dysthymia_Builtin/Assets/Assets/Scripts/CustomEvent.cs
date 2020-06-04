using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvent : MonoBehaviour
{

    public UnityEvent OnEvent;
    public UnityEvent OnEnterEventZone;
    public UnityEvent OnExitEventZone;
    public UnityEvent OnDoorInteract;
    public UnityEvent OnStayEnterZone;




    public enum EventTriggerMethod
    {
        TriggerEnter,
        TriggerExit,
        ObjectInteract,
        Door,
        EnemyKilled,
        TriggerStay
    }
    public string trigTag;
    public EventTriggerMethod eventMode;
    public EnemyAI enemyToKill;
    bool isInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(trigTag))
        {
            OnEnterEventZone.Invoke();
            isInTrigger = true;
            if (eventMode == EventTriggerMethod.TriggerEnter)
            {
                OnEvent.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameManager._instance.ennemyInZone = true;
        if (other.CompareTag(trigTag))
        {
            OnStayEnterZone.Invoke();
            isInTrigger = true;
            if (eventMode == EventTriggerMethod.TriggerStay && GameManager._instance.DoorGardienClose == true)
            {
                OnEvent.Invoke();

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(trigTag))
        {
            OnExitEventZone.Invoke();
            isInTrigger = false;
            if (eventMode == EventTriggerMethod.TriggerEnter)
            {
                OnEvent.Invoke();
            }
        }
    }



    private void Update()
    {
        if (isInTrigger && eventMode == EventTriggerMethod.ObjectInteract || eventMode == EventTriggerMethod.Door)
        {
            // si raycast vers l'objet et input E
            //OnEvent.Invoke();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (eventMode == EventTriggerMethod.Door && GameManager._instance.gotKey)
                    OnDoorInteract.Invoke();
                else
                {
                    OnEvent.Invoke();
                }
            }
        }

    }


    public void ShowPanel(string panelName)
    {
        UIManager._instance.ShowPanel(panelName);
    }
    public void HidePanel(string panelName)
    {
        UIManager._instance.HidePanel(panelName);
    }
}
