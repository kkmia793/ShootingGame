using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ShotManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxBulletSize;
    [SerializeField] private float resizeSpeed = 1.0f;
    [SerializeField] private float maxClickTime; // Maximum click duration for resizing

    private bool isClicking = false;
    private float clickStartTime = 0f;
    private int bulletSizeStage = 0; // 0: Small, 1: Medium, 2: Large
    private Animator anim;

    PlayerManager playerManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            isClicking = true;
            clickStartTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicking = false;
            ShootBullet();
        }

        if (isClicking)
        {
            float clickDuration = Time.time - clickStartTime;
            float normalizedDuration = Mathf.Clamp01(clickDuration / maxClickTime);

            bulletSizeStage = Mathf.FloorToInt(normalizedDuration * 2); // Three stages of size change

            ResizeBullet(bulletSizeStage);
        }
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float damageMultiplier = CalculateDamageMultiplier(bulletSizeStage);
        int calculatedDamage = Mathf.RoundToInt(1.0f + bulletSizeStage * 2.0f); // �X�e�[�W���Ƃ̃_���[�W���v�Z
        int calculatedEnergy = Mathf.RoundToInt(5.0f + bulletSizeStage * 5.0f);
        bullet.GetComponent<PlayerBullet>().SetDamage(calculatedDamage);
        bullet.GetComponent<PlayerBullet>().SetBulletStage(bulletSizeStage);
        bullet.GetComponent<PlayerBullet>().SetEnergy(calculatedEnergy);
        ResizeBullet(bulletSizeStage, bullet); // Apply size change to the bullet
        anim.SetTrigger("attack");
    }

    private void ResizeBullet(int stage, GameObject bullet = null)
    {
        float newSize = Mathf.Lerp(1.0f, maxBulletSize, stage / 2.0f); // Stage 0: 1.0, Stage 1: maxBulletSize/2, Stage 2: maxBulletSize
        if (bullet != null)
        {
            bullet.transform.localScale = new Vector3(newSize, newSize, newSize); // X, Y, Z�����̃T�C�Y�𓯎��ɕύX
        }
    }

    private float CalculateDamageMultiplier(int stage)
    {
        // �����Ń_���[�W�̔{���𒲐����郍�W�b�N��ǉ�
        // stage �� 0 �̂Ƃ��A�_���[�W��1
        // stage �� 1 �̂Ƃ��A�_���[�W��3
        // stage �� 2 �̂Ƃ��A�_���[�W��5
        return 1.0f + stage * 2.0f;
    }

}