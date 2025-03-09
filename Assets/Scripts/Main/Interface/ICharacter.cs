using UnityEngine;

public interface ICharacter
{
    void Jump();
    void Attack();
    void Reflect();
    void TakeDamage(int amount);
    void SetMP(int amount);
    void SetProjectileType(string type);
    void SetInfiniteMP(bool enabled);
    void RecoverMP(float amount);
}
