// Health.cs
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    private List<IHealthObserver> observers = new();
    private int maxGems = 3;
    private int currentGems;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentGems = 0;
    }

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentGems => currentGems;

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        NotifyObservers();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void AddGem()
    {
        currentGems += 1;
        NotifyObservers();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        NotifyObservers();
    }

    public void RegisterObserver(IHealthObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IHealthObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentHealth, maxHealth);
            observer.OnGemAdded();
        }

    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} is dead.");
        Destroy(gameObject);
    }
}