using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryan_PodControlHub : MonoBehaviour
{
    private FPSController pilotReference = null;

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
        if(GetComponent<FPSController>())
        {
            pilotReference = other.GetComponent<FPSController>();
            EnterPilot();
        }
    }

    private void EnterPilot()
    {

    }

    private void ExitPilot()
    {
        pilotReference = null;
    }
}
