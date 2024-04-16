using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform path;

    private void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        Vector3 startPos= path.GetChild(0).position;
        Vector3 endPos = startPos;
        foreach (Transform pathway in path)
        {
            Gizmos.DrawCube(pathway.position, new Vector3(0.5f,0.5f,0.5f));
            Gizmos.DrawLine(endPos, pathway.position);
            endPos = pathway.position;
        }
        Gizmos.DrawLine(endPos, startPos);
    }

}
