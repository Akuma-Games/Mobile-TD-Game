using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private Transform targetWaypoint;
    private int waypointIndex = 0;

    private bool dead;

    private void Start() {
        targetWaypoint = Waypoints.points[0];

        dead = false;

        GameManager.Instance.EnemiesInTheScene++;
    }

    private void Update() {
        if (!dead) {
            Vector3 movePos = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
            Vector3 deltaPos = movePos - transform.position;

            transform.position = movePos;
            transform.rotation = Quaternion.LookRotation(deltaPos);

            if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.005f) {
                GetNextWaypoint();
            }
        }
    }

    void GetNextWaypoint() {
        // Reached the End
        if (waypointIndex >= Waypoints.points.Length - 1) {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        targetWaypoint = Waypoints.points[waypointIndex];
    }

    public IEnumerator Die() {
        GetComponent<Animator>().SetTrigger("Die");
        dead = true;
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        GameManager.Instance.EnemyDie();
        Destroy(gameObject);
    }
}
