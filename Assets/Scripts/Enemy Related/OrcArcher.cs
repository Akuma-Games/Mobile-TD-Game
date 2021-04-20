using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class OrcArcher : Enemy
{
    [SerializeField]
    List<GameObject> enemiesInRange;
    GameObject attackTarget;

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnPoint;


    public override void Start() {
        base.Start();

        enemiesInRange = new List<GameObject>();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    public override void Update() {
        if (!anim.GetBool("Attacking"))
            base.Update();

        if (anim.GetBool("Attacking") && attackTarget != null) {
            transform.rotation = Quaternion.LookRotation(attackTarget.transform.position - transform.position);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("ArcherTarget")) {
            enemiesInRange.Add(other.gameObject);
            anim.SetBool("Attacking", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("ArcherTarget")) {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    IEnumerator Attack() {
        while (true) {
            if (enemiesInRange.Count != 0) {

                if (enemiesInRange[0] != null) {
                    attackTarget = enemiesInRange[0];
                    GameObject originalAttackTarget = attackTarget;

                    attackTarget = attackTarget.transform.parent.gameObject;
                    yield return new WaitForSeconds(1.4f);
                    GameObject spawnedArrow = Instantiate(arrowPrefab, arrowSpawnPoint);
                    spawnedArrow.GetComponent<Arrow>().SetTarget(attackTarget);
                    Debug.Log(attackTarget.name);

                    try {
                        if (attackTarget != null) {

                            if (attackTarget.GetComponent<Health>().WillDieFromDamage(25)) {
                                enemiesInRange.Remove(originalAttackTarget);
                                //attackTarget.GetComponent<Health>().ChangeHP(-25);
                            }
                            else {
                                //attackTarget.GetComponent<Health>().ChangeHP(-25);
                            }
                        }
                        else {
                            Destroy(spawnedArrow);
                            enemiesInRange.Remove(originalAttackTarget);

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
