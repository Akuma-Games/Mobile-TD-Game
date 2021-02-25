using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundIMG;
    [SerializeField]
    private float updateSpeed = 0.5f;
    // Start is called before the first frame update

    private void Awake()
    {
        GetComponentInParent<Health>().OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChange = foregroundIMG.fillAmount;
        float elapsed = 0f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            foregroundIMG.fillAmount = Mathf.Lerp(preChange, pct, elapsed / updateSpeed);
            yield return null;
        }
        foregroundIMG.fillAmount = pct;

    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
