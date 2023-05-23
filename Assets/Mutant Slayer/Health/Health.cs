using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int health;

    public UnityEvent OnDeath;
    public UnityEvent OnHit;

    [SerializeField] protected Slider healthSlider;

    private void Awake()
    {
        healthSlider.maxValue = maxHealth;
    }

    private void OnEnable()
    {
        Reload();
    }

    public void Reload()
    {
        health = maxHealth;
        healthSlider.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthSlider.value = health;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }
}
