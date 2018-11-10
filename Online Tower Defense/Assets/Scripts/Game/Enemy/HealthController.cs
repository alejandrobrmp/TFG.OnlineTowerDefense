using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void HealthChanged(float health);
public class HealthController : MonoBehaviour {

    public Image HealthBar;
    public event HealthChanged OnHealthChanged;

    private float DesiredHealth = 100f;
    public float CurrentHealth = 100f;

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    private void Update()
    {
        float initialValue = HealthBar.fillAmount;
        float desired = DesiredHealth / 100f;
        
        if (initialValue != desired)
        {
            HealthBar.fillAmount = Mathf.Lerp(initialValue, desired, Time.deltaTime * 10);
        }
        
    }

    public void ApplyHealthChanges(float amount)
    {
        DesiredHealth = Mathf.Clamp(DesiredHealth += amount, 0f, 100f);
        if (OnHealthChanged != null)
        {
            OnHealthChanged(DesiredHealth);
        }
    }

}
