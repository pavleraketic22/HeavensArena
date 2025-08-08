// Health.cs
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IStats
{
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int currentHealth;
    
    [SerializeField] private int maxMana = 20;
    [SerializeField] private int currentMana = 20;
    [SerializeField] private float manaRegenRate = 2f;
    private float manaRegenTimer = 0f;
    
    private int maxGems = 3;
    private int currentGems;
    
    private List<IStatsObserver> observers = new();
    

    private void Start()
    {
        currentHealth = maxHealth;
        currentGems = 0;
        currentMana = maxMana;
        NotifyObservers();
    }

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentGems => currentGems;
    public int CurrentMana => currentMana;
    public int MaxMana => maxMana;

    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            NotifyObservers();
            return true;
        }
        else
        {
            return false;
        }
        
    }
    
    public void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            manaRegenTimer += Time.deltaTime;

            if (manaRegenTimer >= 5f)
            {
                currentMana += 2;
                currentMana = Mathf.Min(currentMana, maxMana);
                manaRegenTimer = 0f;
                NotifyObservers();
            }
        }
        else
        {
            // Resetuj timer ako je mana puna
            manaRegenTimer = 0f;
        }
    }

    public void AddMana(int amount)
    {
        currentMana += amount;
        NotifyObservers();
    }

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

    public void RegisterObserver(IStatsObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IStatsObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentHealth, maxHealth);
            observer.OnGemAdded();
            observer.OnManaChanged(currentMana,maxMana);
        }

    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} is dead.");
        Destroy(gameObject);
    }
}