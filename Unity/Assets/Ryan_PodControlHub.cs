using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryan_PodControlHub : MonoBehaviour
{
    private FPSController pilotReference = null;

    //Boid variables
    public Boid boid;

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
            pilotReference = other.GetComponent<FPSController>();
            StartCoroutine(EnterPilot());
        }
    }

    IEnumerator EnterPilot()
    {
        //Move pilot to the center of this object and match rotation
        //Turn of colliders of this object so they dont push camera out.
        //Freeze camera position and change fps controls to affect the boids movement, animation is automatic, simply change what object gets moved from the camera object to the head boid object

        if(pilotReference != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            pilotReference.enabled = false;
            pilotReference.GetComponent<Collider>().enabled = false;

            boid.enabled = false;

            pilotReference.GetComponent<FollowCamera>().enabled = true;
            pilotReference.GetComponent<FollowCamera>().target = boid.camTarget.transform;
            boid.GetComponentInParent<Ryan_ManualBoidControl>().enabled = true;

            yield return null;
        }
    }

    private void ExitPilot()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        pilotReference.enabled = true;
        pilotReference.GetComponent<FollowCamera>().enabled = false;
        pilotReference.GetComponent<Collider>().enabled = true;


        boid.GetComponentInParent<Ryan_ManualBoidControl>().enabled = false;

        boid.enabled = true;
        pilotReference = null;
    }
}
