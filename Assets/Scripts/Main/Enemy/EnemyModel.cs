using System;
using UnityEngine;

public class EnemyModel : ICharacter
{
    private CharacterData _characterData;
    private int _maxHealth;
    private int _maxMP;
    private int _health;
    private int _mp;
    private bool _infiniteMP;
    private string _projectileType;
    
    public event Action<int> OnHealthChanged;
    public event Action<int> OnMPChanged;
    public event Action OnDeath;

    public EnemyModel(CharacterData data)
    {
        Initialize(data);
    }

    public void Initialize(CharacterData data)
    {
        _characterData = data;
        _maxHealth = data.maxHealth;
        _maxMP = data.maxMP;
        _health = _maxHealth;
        _mp = _maxMP;
        _infiniteMP = false;
        _projectileType = data.defaultProjectileType;
        
        OnHealthChanged?.Invoke(_health);
        OnMPChanged?.Invoke(_mp);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        _health = Mathf.Max(0, _health);
        OnHealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void RecoverMP(float amount)
    {
        _mp += (int)amount;
        _mp = Mathf.Min(_maxMP, _mp);
        OnMPChanged?.Invoke(_mp);
    }

    public void SetProjectileType(string type)
    {
        _projectileType = type;
    }

    public void SetInfiniteMP(bool enabled)
    {
        _infiniteMP = enabled;
    }

    public void Jump()
    {
        Debug.Log("Jump");
    }

    public void Attack()
    {
        Debug.Log($"EnemyModel: {_projectileType} タイプの弾を発射");
    }
    
    public void Reflect()
    {
        Debug.Log("PlayerModel: 弾を反射");
    }

    public void SetMP(int amount)
    {
        _mp = Mathf.Clamp(amount, 0, _maxMP);
    }
}