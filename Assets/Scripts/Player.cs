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
    void Update()
    {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMag=inputDir.magnitude;
        smoothInpMag = Mathf.SmoothDamp(smoothInpMag, inputMag, ref smoothMoveVelocity, smoothMoveTime);
        float lookAngle = Mathf.Atan2(inputDir.x, inputDir.z)*Mathf.Rad2Deg;

        angle = Mathf.LerpAngle(angle, lookAngle, Time.deltaTime*turnSpeed*inputMag);
        transform.eulerAngles=Vector3.up*angle;

        transform.Translate(translation: transform.forward* speed * Time.deltaTime*smoothInpMag, relativeTo: Space.World);


    }
}
