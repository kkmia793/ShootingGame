using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CPUManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField, Header("体力")]
    private int HP;
    [SerializeField, Header("最大体力")]
    private int MaxHP=100;
    [SerializeField, Header("ジャンプ力")]
    public float jumpPower = 15f;

    [SerializeField,Header("弾の最大サイズ")]
    private float maxBulletSize;
    [SerializeField,Header("弾の大きさを変えるために必要なクリック時間")] 
    private float resizeSpeed = 1.0f;

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private Coroutine shootCoroutine;
    private Coroutine jumpCoroutine;

    private int direction = 1;
    private int __jumpCount;

    bool isJumping = false;
    private bool alive = true;

    CPUShotManager CpuShot;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        HP = MaxHP;

        __jumpCount = 0;
        shootCoroutine = StartCoroutine(ShootRandomBullet());
        jumpCoroutine = StartCoroutine(RandomJump());

    }

    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // 衝突したオブジェクトが床であることを確認
        if (collision.collider.CompareTag("Floor"))
        {
            // "isJump" パラメータを false に設定
            anim.SetBool("isJump", false);

            // ジャンプ回数をリセット
            __jumpCount = 0;
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Hurt();
        }

    }


    void Jump()
    {
        if (__jumpCount < 2)
        {
            isJumping = true;
            anim.SetBool("isJump", true);
            __jumpCount++;
            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
        if (!isJumping)
        {
            return;
        }

        isJumping = false;
    }

    void Hurt()
    {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);     
    }

    void Die()
    {
            anim.SetTrigger("die");     
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
        }

        enabled = false;

    }



    // 新しく追加されたメソッド
    public void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die(); // HPが0以下になったら死亡
            return ;
        }
    }

    // 新しく追加されたメソッド
    public int GetHP()
    {
        return HP;
    }

    // 新しく追加されたメソッド
    public int GetMaxHP()
    {
        return MaxHP;
    }

    private void ShootBullet(int sizeStage)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float damageMultiplier = CalculateDamageMultiplier(sizeStage);
        int calculatedDamage = Mathf.RoundToInt(1.0f + sizeStage * 2.0f);
        bullet.GetComponent<CPUBullet>().SetDamage(calculatedDamage);
        ResizeBullet(sizeStage, bullet);
        anim.SetTrigger("attack");
    }

    private void ResizeBullet(int stage, GameObject bullet = null)
    {
        float newSize = Mathf.Lerp(1.0f, maxBulletSize, stage / 2.0f);
        if (bullet != null)
        {
            bullet.transform.localScale = new Vector3(newSize, newSize, newSize);
        }
    }

    private float CalculateDamageMultiplier(int stage)
    {
        return 1.0f + stage * 2.0f;
    }

    private IEnumerator ShootRandomBullet()
    {
        while (true)
        {
            int sizeStage = Random.Range(0, 3); // 0, 1, 2 のいずれかのステージをランダムに選択

            ShootBullet(sizeStage);

            float randomInterval = Random.Range(1.0f, 1.5f); // 次のランダムな弾までの待機時間
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private IEnumerator RandomJump()
    {
        while (true)
        {
            Jump();

            float randomInterval = Random.Range(1.0f, 3.0f); // 次のランダムなジャンプまでの待機時間
            yield return new WaitForSeconds(randomInterval);
        }
    }

}
