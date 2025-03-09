using System;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    public event Action OnPlayDamageEffect;
    public event Action OnPlayAttackEffect;
    public event Action OnPlayDeathEffect;

    public void SetAppearance(Sprite characterSprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = characterSprite;
        }
    }

    public void UpdatePosition(Vector2 position)
    {
        transform.position = position;
    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
    }

    public void PlayDamageEffect()
    {
        OnPlayDamageEffect?.Invoke();
    }
    

    public void PlayDeathEffect()
    {
        OnPlayDeathEffect?.Invoke();
    }

    public void PlayAttackEffect()
    {
        OnPlayAttackEffect?.Invoke();
    }
}

