using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryan_ManualBoidControl : MonoBehaviour
{
    public float speed = 50.0f;
    public float lookSpeed = 150.0f;

    public bool allowPitch = true;

    public GUIStyle style;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
    }

    void Yaw(float angle)
    {
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = rot * transform.rotation;
    }

    float invcosTheta1;

    void Pitch(float angle)
    {
        float theshold = 0.95f;
        if ((angle > 0 && invcosTheta1 < -theshold) || (angle < 0 && invcosTheta1 > theshold))
        {
            return;
        }
        // A pitch is a rotation around the right vector
        Quaternion rot = Quaternion.AngleAxis(angle, transform.right);

        transform.rotation = rot * transform.rotation;
    }

    void Walk(float units)
    {
        Vector3 forward = gameObject.transform.forward;
        //forward.y = 0;
        forward.Normalize();
        transform.position += forward * units;
    }

    // Update is called once per frame
    void Update()
    {

        //Cursor.lockState = CursorLockMode.Confined;

        float mouseX, mouseY;
        float speed = this.speed;

        invcosTheta1 = Vector3.Dot(transform.forward, Vector3.up);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        Yaw(mouseX * lookSpeed * Time.deltaTime);
        if (allowPitch)
        {
            Pitch(-mouseY * lookSpeed * Time.deltaTime);
        }

        float contWalk = Input.GetAxis("Vertical");
        Walk(contWalk * speed * Time.deltaTime);
    }
}
