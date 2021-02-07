using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(Attack(other.gameObject));
        }
    }

    IEnumerator Attack(GameObject enemy) {
        while (enemy != null) {
            enemy.GetComponent<Health>().ChangeHP(-50);
            yield return new WaitForSeconds(1f);
        }
    }
}
