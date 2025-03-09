using System;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    [SerializeField] private EnemyView view;
    [SerializeField] private Transform spawnPoint;
    
    private EnemyModel model;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.On2PCharacterDataReady += Initialize;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.On2PCharacterDataReady -= Initialize;
        }
    }

    public void Initialize(CharacterData characterData)
    {
        GameObject characterObject = Instantiate(characterData.characterPrefab, spawnPoint.position, Quaternion.identity);
        
        model = new EnemyModel(characterData);
        
        model.OnHealthChanged += OnHealthChanged;
        model.OnMPChanged += OnMPChanged;
        model.OnDeath += OnEnemyDeath;
        
        view.OnPlayDamageEffect += () => Debug.Log("EnemyView: ダメージエフェクト再生");
        view.OnPlayAttackEffect += () => Debug.Log("EnemyView: 攻撃エフェクト再生");
        view.OnPlayDeathEffect += () => Debug.Log("EnemyView: 死亡エフェクト再生");
        
        Debug.Log($"EnemyPresenter: {characterData.characterName} を生成しました。");
    }

    private void OnHealthChanged(int health)
    {
        Debug.Log($"EnemyPresenter: 現在のHPは {health} です");
        if (health <= 0)
        {
            view.PlayDeathEffect();
        }
    }

    private void OnMPChanged(int mp)
    {
        Debug.Log($"EnemyPresenter: 現在のMPは {mp} です");
    }

    private void OnEnemyDeath()
    {
        Debug.Log("EnemyPresenter: 敵が死亡しました");
        view.PlayDeathEffect();
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.OnHealthChanged -= OnHealthChanged;
            model.OnMPChanged -= OnMPChanged;
            model.OnDeath -= OnEnemyDeath;
        }
    }
}