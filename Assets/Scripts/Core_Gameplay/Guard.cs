using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Guard : MonoBehaviour
{
    public static event System.Action OnGuardSpot;

    public Transform path;
    public float speed = 5f;
    public float WaitTime = .3f;
    public float timeToSpot = .5f;
    float TurnSpeed = 90;

    public Light spotlight;
    public float viewDistance;
    float viewAngle;

    Transform player;
    public LayerMask view;
    Color originalLightColor;

    float viewingAngle;
    float playerVisibleTime;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalLightColor = spotlight.color;

        Vector3[] waypoints = new Vector3[path.childCount];
        for (int i =0; i<waypoints.Length; i++)
        {
            waypoints[i]= path.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        StartCoroutine(Follow(waypoints));

    }

    IEnumerator Follow(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetIndex = 1;
        Vector3 targetWayPoint = waypoints[targetIndex];
        transform.LookAt(targetWayPoint);

        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if (transform.position == targetWayPoint)
            {
                targetIndex = (targetIndex+1)%waypoints.Length;
                targetWayPoint = waypoints[targetIndex];
                yield return new WaitForSeconds(WaitTime);
                yield return StartCoroutine(TurnFace(targetWayPoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnFace(Vector3 lookTarget)
    {
        Vector3 dirToTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToTarget.z, dirToTarget.x)*Mathf.Rad2Deg;

        while(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle))>0.05)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, TurnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTime += Time.deltaTime;
        }
        else
        {
            playerVisibleTime -= Time.deltaTime;
        }
        playerVisibleTime = Mathf.Clamp(playerVisibleTime, 0, timeToSpot);
        spotlight.color = Color.Lerp(originalLightColor, Color.red, playerVisibleTime / timeToSpot);

        if(playerVisibleTime>=timeToSpot)
        {
            if(OnGuardSpot!=null)
            {
                OnGuardSpot();
            }
        }

    }
    bool CanSeePlayer()
    {
        float viewDistSquare = viewDistance*viewDistance;
        if((Vector3.SqrMagnitude(transform.position-player.position))< viewDistSquare)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetween = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleBetween < viewAngle/2f)
            {
                if(!Physics.Linecast(transform.position, player.position,view))
                {
                    return true;
                }

            }
        }
        return false;
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

        Gizmos.color= Color.red;
        Gizmos.DrawRay(transform.position, transform.forward*viewDistance);
    }

}
