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

    private int coins = 0;
    public int healthUpgradeCost = 5;
    public int manaUpgradeCost = 5;
    

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

    public int Coins => coins;

    
    public bool BuyHealthUpgrade()
    {
        if (coins >= healthUpgradeCost)
        {
            coins -= healthUpgradeCost;
            maxHealth += 2;               
            currentHealth = maxHealth;    
            NotifyObservers();
            return true;
        }
        return false;
    }

    public bool BuyManaUpgrade()
    {
        if (coins >= manaUpgradeCost)
        {
            coins -= manaUpgradeCost;
            maxMana += 5; 
            currentMana = maxMana; 
            NotifyObservers();
            return true;
        }

        return false;   

    }



    public void AddCoins(int amount)
    {
        coins += amount;
        NotifyObservers();
    }
    
    public void UseCoins(int amount)
    {
        coins -= amount;
        NotifyObservers();
    }
    
    
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
            manaRegenTimer = 0f;
        }
    }

    public void AddMana(int amount)
    {
        Music.Instance.PlaySFX("Heal",0.7f);
        currentMana += amount;
        NotifyObservers();
    }

    public void TakeDamage(int amount)
    {
        Music.Instance.PlaySFX("Damage",0.7f);
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        NotifyObservers();
        if (currentHealth == 0)
        {
            Die();
            GameManager.Instance.GameOver();
        }
    }

    public void AddGem()
    {
        Music.Instance.PlaySFX("Gems",0.7f);
        currentGems += 1;
        NotifyObservers();
    }

    public void Heal(int amount)
    {
        Music.Instance.PlaySFX("Heal",0.7f);
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
            observer.OnCoinsChanged(coins);
        }

    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} is dead.");
        Destroy(gameObject);
    }
}