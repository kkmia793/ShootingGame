using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBullet : MonoBehaviour
{
    [SerializeField, Header("弾速")]
    public int bulletSpeed;

    // ダメージの初期値
    private int damage = 1;

    private int bulletSizeStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ステージを設定
        SetCPUBulletStageFromSize();
        Debug.Log("CPUBullet Stage on Start: " + bulletSizeStage);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        offScreen();
    }

    private void Move()
    {
        transform.position += new Vector3(-bulletSpeed, 0, 0) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 衝突したオブジェクトが"Player"タグでない場合、ダメージを与える
           PlayerManager enemyPlayer = collision.gameObject.GetComponent<PlayerManager>();
            Destroy(this.gameObject);
            if (enemyPlayer != null)
            {
                enemyPlayer.Damage(damage);
            }

        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            PlayerBullet hitBullet = collision.gameObject.GetComponent<PlayerBullet>();


            if (hitBullet != null)
            {
                // 衝突した弾のステージを取得
                int hitBulletStage = hitBullet.GetBulletStage();
                 bulletSizeStage = GetBulletStage();
    


                Debug.Log("CPUBullet Collision with PlayerBullet. CPUBullet Stage: " + bulletSizeStage + ", PlayerBullet Stage: " + hitBulletStage);

                // ステージが同じか小さい方が破壊される
                if (hitBulletStage <= bulletSizeStage)
                {
                    Destroy(collision.gameObject); // 衝突した弾を破壊
                }

                if (hitBulletStage >= bulletSizeStage)
                {
                    Destroy(this.gameObject); // 自身を破壊
                }
            }
        }
    }

    private void offScreen()
    {
        if (this.transform.position.x < -20.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // ダメージをセットするメソッド
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetCPUBulletStage(int newStage)
    {
        bulletSizeStage = newStage;
    }

    private void SetCPUBulletStageFromSize()
    {
        // 例: 大きさが1.0未満ならステージ0、1.0以上2.0未満ならステージ1、2.0以上ならステージ2とする
        float bulletSize = transform.localScale.x;
        bulletSizeStage = bulletSize < 1.1f ? 0 : (bulletSize < 3.6f ? 1 : 2);
    }

    // ステージを取得するメソッド
    public int GetBulletStage()
    {
        return bulletSizeStage;
    }


}
