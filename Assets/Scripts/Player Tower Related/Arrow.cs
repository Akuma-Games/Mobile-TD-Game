using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject targetEnemy;
    float speed = 12;
    Vector3 enemyPositionOffset = new Vector3(0, 1.1f, 0);

    Vector3 targetPosition;
    float radius = 0.01f;
    float radiusSq;

    Vector3 origin, currentPosition;
    float distanceTravelled;
    float arcFactor = 0.1f;

    private void Start() {
        targetPosition = targetEnemy.transform.position + enemyPositionOffset;
        origin = currentPosition = transform.position;
        radiusSq = radius * radius;

        Vector3 direction = targetPosition - currentPosition;
        if (direction.sqrMagnitude < radiusSq) {
            Debug.Log("DESTROYED ON START");
            targetEnemy.GetComponent<Health>().ChangeHP(-25);
            Destroy(gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetEnemy) {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = targetPosition - currentPosition;
        currentPosition += direction.normalized * speed * Time.deltaTime;
        distanceTravelled += speed * Time.deltaTime;

        float totalDistance = Vector3.Distance(origin, targetPosition);
        float heightOffset = arcFactor * totalDistance * Mathf.Sin(distanceTravelled * Mathf.PI / totalDistance);
        transform.position = currentPosition + new Vector3(0, 0, heightOffset);

        if (direction.sqrMagnitude < radiusSq) {
            Destroy(gameObject);
            targetEnemy.GetComponent<Health>().ChangeHP(-25);
        }

    }

    public void SetTarget(GameObject enemy) {
        targetEnemy = enemy;
    }
}
