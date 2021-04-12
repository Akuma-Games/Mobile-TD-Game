using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MeleeTower : Tower
{
    [SerializeField] int enemiesBlocked = 2;

    [SerializeField] AudioSource swordAtk;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    IEnumerator Attack() {
        while (true) {
            if (enemiesInRange.Count != 0) {

                if (enemiesInRange[0] != null) {
                    attackTarget = enemiesInRange[0];
                    swordAtk.Play();
                    yield return new WaitForSeconds(1.3333f);

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

    public override void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<OrcArcher>()) {
            Debug.Log("archer in range");
            return;
        }
        base.OnTriggerEnter(other);

        Debug.Log("im here now");
        


        if (other.gameObject.CompareTag("Enemy")) {
            if (other.gameObject.GetComponent<Enemy>().Unblockable)
                return;

            if (enemiesInRange.Count <= enemiesBlocked) {
                other.gameObject.GetComponent<Enemy>().Blocked = true;
                other.gameObject.GetComponent<Enemy>().Blockedby = gameObject;
            }
        }
    }

}
