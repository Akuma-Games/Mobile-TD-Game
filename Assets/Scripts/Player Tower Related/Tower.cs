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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            enemiesInRange.Add(other.gameObject);
            anim.SetBool("Attacking", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            enemiesInRange.Remove(other.gameObject);

            //if (enemiesInRange.Count <= 0) {
            //    anim.SetBool("Attacking", false);
            //}
        }
    }

    
}
