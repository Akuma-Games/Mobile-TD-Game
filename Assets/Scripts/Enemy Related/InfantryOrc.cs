using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryOrc : Enemy
{
    // Start is called before the first frame update
    public override void Start() {
        base.Start();

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
    }

    IEnumerator Attack() {
        while (true) {
            if (blocked) {

                Debug.Log("im blocked");
                GameObject attackTarget = blockedBy;
                anim.SetBool("Attacking", true);

                yield return new WaitForSeconds(1.3333f);

                if (attackTarget != null) {
                    if (!attackTarget.GetComponent<Health>().WillDieFromDamage(25)) {
                        attackTarget.GetComponent<Health>().ChangeHP(-25);
                    }
                    else {
                        attackTarget.GetComponent<Health>().ChangeHP(-25);
                        
                    }
                }
                else {
                    blocked = false;
                }
            }
            else {
                anim.SetBool("Attacking", false);
                yield return new WaitUntil(() => blocked);
            }
        }
    }
}
