using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryan_PodControlHub : MonoBehaviour
{
    private FPSController pilotReference = null;

    //Boid variables
    private Boid boid;

    private void Start()
    {
        boid = GetComponentInChildren<Boid>();
    }

    private void Update()
    {
        if(pilotReference != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ExitPilot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FPSController>())
        {
            Debug.Log("Entered Pod");   
            pilotReference = other.GetComponent<FPSController>();
            StartCoroutine(EnterPilot());
        }
    }

    IEnumerator EnterPilot()
    {
        //Move pilot to the center of this object
        if(pilotReference != null)
        {
            pilotReference.transform.position = Vector3.Lerp(pilotReference.transform.position, gameObject.transform.position, 0.2f);
            yield return null;
        }
    }

    private void ExitPilot()
    {
        pilotReference = null;
    }
}
