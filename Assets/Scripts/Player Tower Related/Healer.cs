using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Tower
{
    public GameObject healingTarget;
    public List<GameObject> alliesInRange;


    // Start is called before the first frame update
    public override void Start() {
        anim = GetComponent<Animator>();

        alliesInRange = new List<GameObject>();

        StartCoroutine(Heal());
    }

    // Update is called once per frame
    public override void Update() {
        if (anim.GetBool("Healing") && healingTarget != null) {
            transform.rotation = Quaternion.LookRotation(healingTarget.transform.position - transform.position);
        }
    }

    public override void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Tower")) {
            Debug.Log("In Range: " + other.gameObject.name);
            alliesInRange.Add(other.gameObject);
        }
    }

    IEnumerator Heal() {
        while (true) {
            if (alliesInRange.Count > 0) {
                if (alliesInRange.Count == 1)
                    healingTarget = alliesInRange[0];
                else
                    healingTarget = PickTargetToHeal();

                if (healingTarget != null) {
                    if (healingTarget.GetComponent<Health>().HealthPercentage < 1) {
                        healingTarget.GetComponent<Health>().ChangeHP(10);
                        anim.SetBool("Healing", true);
                    }
                }
                else {
                    alliesInRange.Remove(healingTarget);
                    anim.SetBool("Healing", false);
                }

                yield return new WaitForSeconds(1.333f);

                if (healingTarget == null) {
                     alliesInRange.Remove(healingTarget);
                    anim.SetBool("Healing", false);
                }
            }
            else {
                anim.SetBool("Healing", false);
                yield return new WaitUntil(() => alliesInRange.Count > 0);
            }
        }

        GameObject PickTargetToHeal() {


            float lowestHealthPercentage = alliesInRange[0].GetComponent<Health>().HealthPercentage;
            GameObject targetAlly = alliesInRange[0];

            foreach (var ally in alliesInRange) {
                if (ally.GetComponent<Health>().HealthPercentage < lowestHealthPercentage) {
                    Debug.Log(ally.name + ": " + ally.GetComponent<Health>().HealthPercentage);

                    lowestHealthPercentage = ally.GetComponent<Health>().HealthPercentage;
                    targetAlly = ally;
                }
            }

            if (lowestHealthPercentage == 1) {
                anim.SetBool("Healing", false);
                return null;
            }

            return targetAlly;
        }
    }
}
