using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Header("体力")]
    private int HP;
    [SerializeField, Header("最大体力")]
    private int MaxHP = 100;
    [SerializeField, Header("魔力")]
    private float MP;
    [SerializeField, Header("最大魔力")]
    private float MaxMP = 100;
    [SerializeField, Header("魔力回復速度")]
    public float MP_RecoverySpeed = 5;
    [SerializeField, Header("消費魔力")]
    public int Energy = 5;
    [SerializeField, Header("移動速度")]
    public float movePower = 10f;
    [SerializeField, Header("ジャンプ力")]
    public float jumpPower = 15f;

    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    private int __jumpCount;
    private bool isJumping = false;
    private bool alive = true;
    public bool canShootCleave = true;
    public bool canShoot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        HP = MaxHP;
        MP = MaxMP;
        __jumpCount = 0;
    }

    private void Update()
    {
        Restart();
        Jump();
        RecoverMP();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            anim.SetBool("isJump", false);
            canShootCleave = true;
            __jumpCount = 0;
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Hurt();
        }
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && __jumpCount < 2)
        {
            isJumping = true;
            canShootCleave = false;
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
        rb.AddForce((direction == 1) ? new Vector2(-5f, 1f) : new Vector2(5f, 1f), ForceMode2D.Impulse);
    }

    void Die()
    {
        anim.SetTrigger("die");
        alive = false;
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }

    public void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Consume(int energy)
    {
        MP -= energy;
        Debug.Log("MP: " + MP); // デバッグログを追加
        if (MP <= 0)
        {
            canShoot = false;
        }
    }

    private void RecoverMP()
    {
        if (MP < MaxMP)
        {
            MP += MP_RecoverySpeed * Time.deltaTime;
            MP = Mathf.Clamp(MP, 0f, MaxMP);
        }
    }

    public int GetHP()
    {
        return HP;
    }

    public int GetMaxHP()
    {
        return MaxHP;
    }

    public float GetMP()
    {
        return MP;
    }

    public float GetMaxMP()
    {
        return MaxMP;
    }

    public bool GetisJump()
    {
        return isJumping;
    }
}
