using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUShotManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxBulletSize;
    [SerializeField] private float resizeSpeed = 1.0f;

    private Animator anim;
    private int bulletSizeStage = 0; // �����o�ϐ��Ƃ��Đ錾


    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ShootRandomBullet());
    }

    private void Update()
    {
       
        // �}�E�X�N���b�N�ł̏����͕s�v�Ȃ̂ō폜
    }

    private void ShootBullet(int sizeStage)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float damageMultiplier = CalculateDamageMultiplier(sizeStage);
        int calculatedDamage = Mathf.RoundToInt(1.0f + sizeStage * 2.0f);

        Debug.Log("CPU Bullet Stage: " + sizeStage); // �ǉ�
        bulletSizeStage = sizeStage;

        bullet.GetComponent<CPUBullet>().SetDamage(calculatedDamage);
        bullet.GetComponent<CPUBullet>().SetCPUBulletStage(bulletSizeStage);
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
            Debug.Log("CPU Bullet Stage: " + sizeStage); // �ǉ�

            float randomInterval = Random.Range(0.2f, 0.5f); // ���̃����_���Ȓe�܂ł̑ҋ@����
            yield return new WaitForSeconds(randomInterval);
        }
    }

}