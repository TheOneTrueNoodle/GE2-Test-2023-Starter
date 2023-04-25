using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;
    private GameObject defaultTarget;
    private float WhistleTime = 10f;
    private float currentWhistleTime;

    private void Start()
    {
        defaultTarget = targetGameObject;
    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGameObject != null)
            {
                target = targetGameObject.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }
    
    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);    
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //Whistle Function
            targetGameObject = Camera.main.gameObject;
            currentWhistleTime = WhistleTime;
        }
        if(currentWhistleTime > 0) { currentWhistleTime -= Time.deltaTime; }
        else if(targetGameObject != defaultTarget) { targetGameObject = defaultTarget; }

        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
    }
}