using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected Animator anim;

    [SerializeField] float speed = 10f;

    private Transform targetWaypoint;
    private int waypointIndex = 0;

    private bool dead;

    protected bool unblockable = false;
    public bool Unblockable { get { return unblockable; } }

    protected bool blocked;
    public bool Blocked { set { blocked = value; } }

    protected GameObject blockedBy;
    public GameObject Blockedby {  set { blockedBy = value; } }

    [SerializeField] AudioSource DeathSound;

    public virtual void Start() {
        targetWaypoint = Waypoints.points[0];

        dead = false;

        GameManager.Instance.EnemiesInTheScene++;

        anim = GetComponent<Animator>();
    }

    public virtual void Update() {
        if (!dead && !blocked) {
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
        DeathSound.Play();
        GetComponent<Animator>().SetTrigger("Die");
        if (!dead)
            ResourceManager.Instance.GetResource(ResourceType.GOLD, transform.position);
        dead = true;
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        GameManager.Instance.EnemyDie();
        Destroy(gameObject);
    }
}
