using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
   
    void Update()
    {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float lookAngle = Mathf.Atan2(inputDir.x, inputDir.z)*Mathf.Rad2Deg;
        transform.eulerAngles=Vector3.up*lookAngle;
    }
}
