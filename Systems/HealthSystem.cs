using System;

/// <summary>
/// Methods:
/// - Damage
/// - Heal
/// Events:
/// - OnHealthChanged
/// - OnMaxHealth
/// - OnLowHealth
/// - OnDeath
/// </summary>
public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnMaxHealth;
    public event EventHandler OnLowHealth;
    public event EventHandler OnDeath;

    public bool IsDead { get; private set; }

    private int health;
    private int healthMax;
    private int healthLow;

    private bool isLowHealth = false;
    private bool isMaxHealth = true;

    #region Constructor
    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        // Default low health on 30%
        this.healthLow = healthMax * 30 / 100;
        IsDead = false;
        health = healthMax;
    }

    public HealthSystem(int healthMax, int healthLow)
    {
        this.healthMax = healthMax;
        this.healthLow = healthLow;
        IsDead = false;
        health = healthMax;
    }
    #endregion

    #region Public Methods
    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    /// <param name="damageAmount">Must be greater than 0</param>
    public void Damage(int damageAmount)
    {
        // Validate amount
        if (damageAmount <= 0) return;
        // Apply amount
        health -= damageAmount;
        // Validate minimum
        if (health < 0) health = 0;

        InvokeOnHealthChanged();

        CheckOnLowHealth();

        // Valite OnMaxHealth
        isMaxHealth = false;

        CheckOnDeath();
    }

    /// <param name="healAmount">Must be greater than 0</param>
    public void Heal(int healAmount)
    {
        // Validate amount
        if (healAmount <= 0) return;
        // Apply amount
        health += healAmount;
        // Validate maximum
        if (health > healthMax) health = healthMax;

        InvokeOnHealthChanged();

        // Valite OnLowHealth
        if (health > healthLow && isLowHealth) isLowHealth = false;

        CheckOnMaxHealth();
    }
    #endregion

    #region Private Methods
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
    #endregion
}
