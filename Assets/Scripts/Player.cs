using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public float smoothMoveTime = .1f;

    float turnSpeed = 8f;
    float angle;

    float smoothInpMag;
    float smoothMoveVelocity;

    bool disabled;

    Vector3 velocity;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Guard.OnGuardSpot += Disable;
    }
    void Update()
    {
        Vector3 inputDir = Vector3.zero;
        if (!disabled)
        {
            inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
        
        float inputMag=inputDir.magnitude;
        smoothInpMag = Mathf.SmoothDamp(smoothInpMag, inputMag, ref smoothMoveVelocity, smoothMoveTime);
        float lookAngle = Mathf.Atan2(inputDir.x, inputDir.z)*Mathf.Rad2Deg;

        angle = Mathf.LerpAngle(angle, lookAngle, Time.deltaTime*turnSpeed*inputMag);

        velocity = transform.forward * speed * smoothInpMag;
    }

    private void Disable()
    {
        disabled = true; 
    }
    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(Vector3.up*angle));
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Guard.OnGuardSpot -= Disable;
    }
}
