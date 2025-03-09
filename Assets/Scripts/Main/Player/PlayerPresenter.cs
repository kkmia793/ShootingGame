using System;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView view;
    [SerializeField] private Transform spawnPoint;

    private PlayerModel model;

    private void OnEnable()
    {
        GameManager.Instance.On1PCharacterDataReady += Initialize;
    }

    private void OnDisable()
    {
        GameManager.Instance.On1PCharacterDataReady -= Initialize;
        
        if (model != null)
        {
            model.OnHealthChanged -= OnHealthChanged;
            model.OnMPChanged -= OnMPChanged;
            model.OnDeath -= OnPlayerDeath;
        }
    }
    public void Initialize(CharacterData characterData)
    {
        if (characterData == null || characterData.characterPrefab == null)
        {
            Debug.LogWarning("PlayerPresenter: キャラクター生成に必要なデータが不足しています。");
            return;
        }
        
        GameObject characterObject = Instantiate(characterData.characterPrefab, spawnPoint.position, Quaternion.identity);
        
        model = new PlayerModel(characterData);
        
        model.OnHealthChanged += OnHealthChanged;
        model.OnMPChanged += OnMPChanged;
        model.OnDeath += OnPlayerDeath;
        
        // あとから変更予定
        view.OnPlayDamageEffect += () => Debug.Log("View: ダメージエフェクト再生");
        view.OnPlayAttackEffect += () => Debug.Log("View: 攻撃エフェクト再生");
        view.OnPlayDeathEffect += () => Debug.Log("View: 死亡エフェクト再生");
        
        view.SetAppearance(characterData.characterSprite);
        view.UpdatePosition(spawnPoint.position);

        Debug.Log($"PlayerPresenter: {characterData.characterName} を生成しました。");
    }
    
    private void OnHealthChanged(int health)
    {
        Debug.Log($"Presenter: 現在のHPは {health} です");
        if (health <= 0)
        {
            view.PlayDeathEffect();
        }
    }

    private void OnMPChanged(int mp)
    {
        Debug.Log($"Presenter: 現在のMPは {mp} です");
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Presenter: プレイヤーが死亡しました");
        view.PlayDeathEffect();
    }
}