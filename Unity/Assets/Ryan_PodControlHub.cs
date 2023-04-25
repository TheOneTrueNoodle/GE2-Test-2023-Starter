using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryan_PodControlHub : MonoBehaviour
{
    private FPSController pilotReference = null;
    public int ShiftSpeed = 1;

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
        //Move pilot to the center of this object and match rotation
        //Turn of colliders of this object so they dont push camera out.
        //Freeze camera position and change fps controls to affect the boids movement, animation is automatic, simply change what object gets moved from the camera object to the head boid object

        if(pilotReference != null)
        {
            pilotReference.GetComponent<FPSController>().enabled = false;
            pilotReference.GetComponent<FollowCamera>().enabled = true;
            pilotReference.GetComponent<Collider>().enabled = false;

            if(ShiftSpeed == 0) { ShiftSpeed = 1; }
            float t = 0;
            while(t < 1f)
            {
                t += Time.deltaTime * ShiftSpeed;
                pilotReference.transform.position = Vector3.Lerp(pilotReference.transform.position, gameObject.transform.position, t);
            }
            yield return null;
        }
    }

    private void ExitPilot()
    {
        pilotReference.GetComponent<FPSController>().enabled = true;
        pilotReference.GetComponent<FollowCamera>().enabled = false;
        pilotReference.GetComponent<Collider>().enabled = true;
        pilotReference = null;
    }
}
