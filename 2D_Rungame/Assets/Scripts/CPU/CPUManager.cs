using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CPUManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField, Header("�̗�")]
    private int HP;
    [SerializeField, Header("�ő�̗�")]
    private int MaxHP=100;
    [SerializeField, Header("�W�����v��")]
    public float jumpPower = 15f;

    [SerializeField,Header("�e�̍ő�T�C�Y")]
    private float maxBulletSize;
    [SerializeField,Header("�e�̑傫����ς��邽�߂ɕK�v�ȃN���b�N����")] 
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

        // �Փ˂����I�u�W�F�N�g�����ł��邱�Ƃ��m�F
        if (collision.collider.CompareTag("Floor"))
        {
            // "isJump" �p�����[�^�� false �ɐݒ�
            anim.SetBool("isJump", false);

            // �W�����v�񐔂����Z�b�g
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



    // �V�����ǉ����ꂽ���\�b�h
    public void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die(); // HP��0�ȉ��ɂȂ����玀�S
            return ;
        }
    }

    // �V�����ǉ����ꂽ���\�b�h
    public int GetHP()
    {
        return HP;
    }

    // �V�����ǉ����ꂽ���\�b�h
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
            int sizeStage = Random.Range(0, 3); // 0, 1, 2 �̂����ꂩ�̃X�e�[�W�������_���ɑI��

            ShootBullet(sizeStage);

            float randomInterval = Random.Range(1.0f, 1.5f); // ���̃����_���Ȓe�܂ł̑ҋ@����
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private IEnumerator RandomJump()
    {
        while (true)
        {
            Jump();

            float randomInterval = Random.Range(1.0f, 3.0f); // ���̃����_���ȃW�����v�܂ł̑ҋ@����
            yield return new WaitForSeconds(randomInterval);
        }
    }

}