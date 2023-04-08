using System;

/// <summary>
/// Methods:
/// - Damage
/// - Heal
/// - Revive
/// Events:
/// - OnHealthChanged
/// - OnMaxHealth
/// - OnLowHealth
/// - OnDeath
/// - OnRevive
/// </summary>
public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnMaxHealth;
    public event EventHandler OnLowHealth;
    public event EventHandler OnDeath;
    public event EventHandler OnRevive;

    public bool IsDead { get; private set; }
    public bool Immunity { get; private set; }

    private int health;
    private int healthMax;
    private int healthLow;

    private bool isLowHealth = false;
    private bool isMaxHealth = true;

    private bool hitCountMode;

    private float mercyDuration;
    private float mercyTimer;
    private bool HaveMercy => mercyTimer > 0;

    // Getters
    public int GetHealth => health;
    public int GetMaxHealth => healthMax;
    public float GetHealthPercent => (float)health / healthMax;

    // Setters
    public void SetInmunity(bool enable) => Immunity = enable;


    public HealthSystem(int healthMax, float mercyTime = 0, bool hitCountMode = false)
    {
        this.healthMax = healthMax;
        // Default low health on 30%
        this.healthLow = healthMax * 30 / 100;
        IsDead = false;
        Immunity = false;
        health = healthMax;
        this.hitCountMode = hitCountMode;
    }

    public HealthSystem(int healthMax, int healthLow)
    {
        this.healthMax = healthMax;
        this.healthLow = healthLow;
        IsDead = false;
        health = healthMax;
    }


    /// <param name="damageAmount">Must be greater than 0</param>
    public void Damage(int damageAmount)
    {
        if (IsDead) return;

        if (HaveMercy) return;

        // Validate amount
        if (damageAmount <= 0) return;

        if (hitCountMode)
            damageAmount = 1;
        
        // Apply amount
        health -= damageAmount;
        // Validate minimum
        if (health < 0) health = 0;

        InvokeOnHealthChanged();

        CheckOnLowHealth();

        // Valite OnMaxHealth
        isMaxHealth = false;

        CheckOnDeath();

        if (!IsDead)
            mercyTimer = mercyDuration;
    }

    /// <param name="healAmount">Must be greater than 0</param>
    public void Heal(int healAmount)
    {
        if (IsDead) return;

        // Validate amount
        if (healAmount <= 0) return;
        // Apply amount
        health += healAmount;
        // Validate maximum
        if (health > healthMax) health = healthMax;

        IsDead = false;

        InvokeOnHealthChanged();

        // Valite OnLowHealth
        if (health > healthLow && isLowHealth) isLowHealth = false;

        CheckOnMaxHealth();
    }

    public void Revive(float reviveHealthPercentage = 1f)
    {
        if (!IsDead) return;

        IsDead = false;

        health = UnityEngine.Mathf.RoundToInt(healthMax * reviveHealthPercentage);
        InvokeOnHealthChanged();
        if (OnRevive != null)
            OnRevive.Invoke(this, EventArgs.Empty);
    }

    public void Update()
    {
        if (HaveMercy)
        {
            mercyTimer -= UnityEngine.Time.deltaTime;
        }
    }


    private void InvokeOnHealthChanged()
    {
        if (OnHealthChanged != null) OnHealthChanged.Invoke(this, EventArgs.Empty);
    }

    private void CheckOnDeath()
    {
        // Validate OnDeath
        if (health == 0) IsDead = true;
        // Invoke OnDeath
        if (IsDead && OnDeath != null) OnDeath.Invoke(this, EventArgs.Empty);
    }

    private void CheckOnMaxHealth()
    {
        // Valite OnMaxHealth
        if (health == healthMax && !isMaxHealth)
        {
            // Invoke OnMaxHealth
            if (OnMaxHealth != null) OnMaxHealth.Invoke(this, EventArgs.Empty);
            isMaxHealth = true;
        }
    }

    private void CheckOnLowHealth()
    {
        // Valite OnLowHealth
        if (health <= healthLow && !isLowHealth)
        {
            // Invoke OnLowHealth
            if (OnLowHealth != null) OnLowHealth.Invoke(this, EventArgs.Empty);
            isLowHealth = true;
        }
    }
}
