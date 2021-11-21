using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;
    [SerializeField] private bool circledMove;
    public bool moveOnTrigger;
    [SerializeField] private int curWayPointIndex;
    [SerializeField] private Transform _transform;
    private bool reverseMove;
    private List<Transform> objectOnPlatform = new List<Transform>();

    private void Start() 
    {
        curWayPointIndex = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
            objectOnPlatform.Add(other.transform);
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
            objectOnPlatform.Remove(other.transform);
    }

    private void Update()
    {  
        if(moveOnTrigger == false)
        {
            Vector3 direction = wayPoints[curWayPointIndex].position -_transform.position;
            direction.z = 0;
            if(direction.magnitude <= 0.5f) 
                NextWayPoint();

            _transform.position += direction.normalized * speed * Time.deltaTime;
            foreach(Transform trf in objectOnPlatform)
            {
                trf.position += direction.normalized * speed * Time.deltaTime;
            }
        }
    }

    private void NextWayPoint()
    {
        if(curWayPointIndex + 1 == wayPoints.Length) 
        {
            if(circledMove){ curWayPointIndex = 0; return; }
            else reverseMove = true;
        }

        if(reverseMove)
        {
            curWayPointIndex--;
            if(curWayPointIndex == 0) reverseMove = false;
        }
        else
        {
            curWayPointIndex++;
        }
    }

    private void OnDrawGizmos() 
    {
        if(wayPoints != null)
        {
            for(int i = 0; i < wayPoints.Length; i++)
            {
                if(i < wayPoints.Length-1)
                {
                    Gizmos.DrawLine(wayPoints[i].position, wayPoints[i+1].position);
                }
                else if(circledMove)
                {
                    Gizmos.DrawLine(wayPoints[i].position, wayPoints[0].position);
                }
                else break;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_transform.position, wayPoints[curWayPointIndex].position);
        }
    }
}
