using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBullet : MonoBehaviour
{
    [SerializeField, Header("�e��")]
    public int bulletSpeed;

    // �_���[�W�̏����l
    private int damage = 1;

    private int bulletSizeStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        // �X�e�[�W��ݒ�
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
            // �Փ˂����I�u�W�F�N�g��"Player"�^�O�łȂ��ꍇ�A�_���[�W��^����
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
                // �Փ˂����e�̃X�e�[�W���擾
                int hitBulletStage = hitBullet.GetBulletStage();
                 bulletSizeStage = GetBulletStage();
    


                Debug.Log("CPUBullet Collision with PlayerBullet. CPUBullet Stage: " + bulletSizeStage + ", PlayerBullet Stage: " + hitBulletStage);

                // �X�e�[�W�������������������j�󂳂��
                if (hitBulletStage <= bulletSizeStage)
                {
                    Destroy(collision.gameObject); // �Փ˂����e��j��
                }

                if (hitBulletStage >= bulletSizeStage)
                {
                    Destroy(this.gameObject); // ���g��j��
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

    // �_���[�W���Z�b�g���郁�\�b�h
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
        // ��: �傫����1.0�����Ȃ�X�e�[�W0�A1.0�ȏ�2.0�����Ȃ�X�e�[�W1�A2.0�ȏ�Ȃ�X�e�[�W2�Ƃ���
        float bulletSize = transform.localScale.x;
        bulletSizeStage = bulletSize < 1.1f ? 0 : (bulletSize < 3.6f ? 1 : 2);
    }

    // �X�e�[�W���擾���郁�\�b�h
    public int GetBulletStage()
    {
        return bulletSizeStage;
    }


}