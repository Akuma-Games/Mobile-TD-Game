using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;

public class Tower : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] protected List<GameObject> enemiesInRange;
    protected GameObject attackTarget;

    [SerializeField] BuildableTile myTile;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        enemiesInRange = new List<GameObject>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (anim.GetBool("Attacking") && attackTarget != null) {
            transform.rotation = Quaternion.LookRotation(attackTarget.transform.position - transform.position);
        }
    }

    public virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            enemiesInRange.Add(other.gameObject);
            anim.SetBool("Attacking", true);
        }
    }

    public virtual void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            enemiesInRange.Remove(other.gameObject);

            //if (enemiesInRange.Count <= 0) {
            //    anim.SetBool("Attacking", false);
            //}
        }
    }

    public IEnumerator Die() {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length / 2);
        Destroy(gameObject);
    }

    public void SetTile(BuildableTile tile) {
        myTile = tile;
    }

    private void OnDestroy() {
        myTile.currentTower = TowerType.NONE;
        Healer[] healers = FindObjectsOfType<Healer>();
        foreach (var healer in healers) {
            healer.RemoveFromList(this.gameObject);
        }
    }
}
