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
            enemiesInRange.Add(other.transform.parent.gameObject);
            anim.SetBool("Attacking", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("ArcherTarget")) {
            enemiesInRange.Remove(other.transform.parent.gameObject);
        }
    }

    IEnumerator Attack() {
        while (true) {
            if (enemiesInRange.Count != 0) {

                if (enemiesInRange[0] != null) {
                    attackTarget = enemiesInRange[0];
                    yield return new WaitForSeconds(1.4f);
                    GameObject spawnedArrow = Instantiate(arrowPrefab, arrowSpawnPoint);
                    spawnedArrow.GetComponent<Arrow>().SetTarget(attackTarget);
                    try {        
                        if (attackTarget != null) {
                          


                            if (attackTarget.GetComponent<Health>().WillDieFromDamage(25)) {
                                enemiesInRange.Remove(attackTarget);
                                //attackTarget.GetComponent<Health>().ChangeHP(-25);
                            }
                            else {
                                //attackTarget.GetComponent<Health>().ChangeHP(-25);
                            }
                        }
                        else {
                            Destroy(spawnedArrow);
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

    private void OnDestroy() {
        
    }

    public void RemoveFromList(GameObject deadEnemy) {
        enemiesInRange.Remove(deadEnemy);
    }

}
