using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Tower : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(Attack(other.gameObject));
            anim.SetBool("Attacking", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StopCoroutine(Attack(other.gameObject));
            anim.SetBool("Attacking", false);
        }
    }

    IEnumerator Attack(GameObject enemy) {
        while (enemy != null) {
            enemy.GetComponent<Health>().ChangeHP(-50);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        }
        anim.SetBool("Attacking", false);
    }
}
