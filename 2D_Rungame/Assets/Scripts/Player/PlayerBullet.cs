using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Header("�e��")]
    public float bulletSpeed;

    // �_���[�W�̏����l
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
            // �Փ˂����I�u�W�F�N�g��"Player"�^�O�łȂ��ꍇ�A�_���[�W��^����
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

    // �_���[�W���Z�b�g���郁�\�b�h
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetEnergy(int newEnergy)
    {
        energy = newEnergy;
        Debug.Log("Energy: " + energy); // �f�o�b�O���O��ǉ�
    }

    public void SetBulletStage(int newStage)
    {
        bulletSizeStage = newStage;
    }

    // �X�e�[�W���擾���郁�\�b�h
    public int GetBulletStage()
    {
        return bulletSizeStage;
    }
}