using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private Transform targetWaypoint;
    private int waypointIndex = 0;

    private void Start() {
        targetWaypoint = Waypoints.points[0];
    }

    private void Update() {
        Vector3 movePos = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
        Vector3 deltaPos = movePos - transform.position;

        transform.position = movePos;
        transform.rotation = Quaternion.LookRotation(deltaPos);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.005f) {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {
        if (waypointIndex >= Waypoints.points.Length - 1) {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        targetWaypoint = Waypoints.points[waypointIndex];
    }
}
