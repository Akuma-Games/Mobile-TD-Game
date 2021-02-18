using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private GameObject coinPrefab;
    private int currentHealth;
    //public delegate void OnHealthChanged();
    public event UnityAction<float> OnHealthChanged = delegate { } ;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHP(int amt)
    {
        currentHealth += amt;
        float currentHPPct = (float)currentHealth / (float)maxHealth;
        OnHealthChanged(currentHPPct);

        if (currentHealth <= 0) {
            Instantiate(coinPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { ChangeHP(-10); }
    }
}

