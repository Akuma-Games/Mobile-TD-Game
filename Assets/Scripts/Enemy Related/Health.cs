using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth { get; }

    [SerializeField]
    private GameObject coinPrefab;
    private int currentHealth;
    public int CurrentHealth { get; }
    //public delegate void OnHealthChanged();
    public event UnityAction<float> OnHealthChanged = delegate { } ;

    public float HealthPercentage {  get { return (float)currentHealth / maxHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHP(int amt)
    {
        currentHealth += amt;

        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }

        float currentHPPct = (float)currentHealth / (float)maxHealth;
        OnHealthChanged(currentHPPct);

        if (currentHealth <= 0) {
            //Instantiate(coinPrefab, transform.position, transform.rotation);

            if (GetComponent<Enemy>() != null) {
                StartCoroutine(GetComponent<Enemy>().Die());
            }
            else {
                StartCoroutine(GetComponent<MeleeTower>().Die());
            }
        }
    }

    public bool WillDieFromDamage(int amt) {
        return currentHealth - amt <= 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { ChangeHP(-10); }
    }
}

