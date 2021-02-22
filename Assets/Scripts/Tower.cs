using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;

public class Tower : MonoBehaviour
{
    Animator anim;
    [SerializeField] List<GameObject> enemiesInRange;
    GameObject attackTarget;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemiesInRange = new List<GameObject>();

        StartCoroutine(Attack());

    }

    // Update is called once per frame
    void Update()
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

    IEnumerator Attack() {
        while (true) {
            if (enemiesInRange.Count != 0) {
                //yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);



                if (enemiesInRange[0] != null) {
                    attackTarget = enemiesInRange[0];
                    yield return new WaitForSeconds(1.4f);

                    try {
                        if (attackTarget != null) {

                            if (attackTarget.GetComponent<Health>().WillDieFromDamage(25)) {
                                enemiesInRange.Remove(attackTarget);
                                attackTarget.GetComponent<Health>().ChangeHP(-25);
                            }
                            else {
                                attackTarget.GetComponent<Health>().ChangeHP(-25);
                            }
                        }
                        else {
                            enemiesInRange.Remove(attackTarget);
                        }
                    }
                    catch (Exception e) {
                        print(e.Message);
                    }
                }


            }
            else {
                anim.SetBool("Attacking", false);
                yield return new WaitUntil(() => enemiesInRange.Count > 0);
            }
        }
    }
}
