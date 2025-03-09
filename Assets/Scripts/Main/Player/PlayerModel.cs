using UnityEngine;
using System;

public class PlayerModel : ICharacter
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

    public PlayerModel(CharacterData data)
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

        Debug.Log($"PlayerModel: 初期化完了 - HP: {_health}, MP: {_mp}, 弾タイプ: {_projectileType}");
    }

    public void Jump()
    {
        Debug.Log("PlayerModel: ジャンプを実行");
    }
    
    public void Attack()
    {
        Debug.Log($"PlayerModel: {_projectileType} タイプの弾を発射");
    }
    
    public void Reflect()
    {
        Debug.Log("PlayerModel: 弾を反射");
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
    
    public void SetMP(int amount)
    {
        _mp = Mathf.Clamp(amount, 0, _maxMP);
        Debug.Log($"PlayerModel: MPを {amount} に設定しました");
    }
    
    public void SetProjectileType(string type)
    {
        _projectileType = type;
        Debug.Log($"PlayerModel: 弾のタイプを {_projectileType} に設定しました");
    }
    
    public void SetInfiniteMP(bool enabled)
    {
        _infiniteMP = enabled;
        Debug.Log($"PlayerModel: 無限MPモードを {_infiniteMP} に設定");
    }
    
    public int GetHealth()
    {
        return _health;
    }
    
    public int GetMP()
    {
        return _mp;
    }
}
