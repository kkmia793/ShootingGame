using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Header("弾速")]
    public float bulletSpeed;

    // ダメージの初期値
    private int damage = 1;
    private int energy = 5 ;
    private int bulletSizeStage  ;
    PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.Consume(energy);
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        offScreen();
    }

    private void Move()
    {
        transform.position += new Vector3(bulletSpeed, 0, 0) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 衝突したオブジェクトが"Player"タグでない場合、ダメージを与える
            CPUManager enemyPlayer = collision.gameObject.GetComponent<CPUManager>();
            Destroy(this.gameObject);

            if (enemyPlayer != null)
            {
                enemyPlayer.Damage(damage);
            }
        }

 

    }

    private void offScreen()
    {
        if (this.transform.position.x > 20.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // ダメージをセットするメソッド
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetEnergy(int newEnergy)
    {
        energy = newEnergy;
        Debug.Log("Energy: " + energy); // デバッグログを追加
    }

    public void SetBulletStage(int newStage)
    {
        bulletSizeStage = newStage;
    }

    // ステージを取得するメソッド
    public int GetBulletStage()
    {
        return bulletSizeStage;
    }
}
