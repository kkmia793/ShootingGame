using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotManager : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] private GameObject impactParticle;
    [SerializeField] private GameObject projectileParticle;
    [SerializeField] private GameObject muzzleParticle;
    [SerializeField] private GameObject[] trailParticles;

    [Header("Collider Settings")]
    [SerializeField] private float colliderRadius = 1f;
    [Range(0f, 1f)][SerializeField] private float collideOffset = 0.15f;

    [Header("Time Settings")]
    [SerializeField] private float destroyTimerThreshold = 5.0f;
    [SerializeField] private float trailDestroyTime = 3.0f;
    [SerializeField] private float impactParticleDestroyTime = 5.0f;

    private Rigidbody rb;
    private Transform myTransform;
    private SphereCollider sphereCollider;

    private float destroyTimer = 0f;
    private bool destroyed = false;

    void Start()
    {
        // �R���|�[�l���g�̎擾
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        sphereCollider = GetComponent<SphereCollider>();

        //�v���W�F�N�^�C���Ɣ��˃G�t�F�N�g�̐���
        projectileParticle = Instantiate(projectileParticle, myTransform.position, myTransform.rotation) as GameObject;
        projectileParticle.transform.parent = myTransform;

        if (muzzleParticle)
        {
           muzzleParticle = Instantiate(muzzleParticle, myTransform.position, myTransform.rotation) as GameObject;

             //���˃G�t�F�N�g�̎���
            Destroy(muzzleParticle, 1.5f);
        }
    }

    void FixedUpdate()
    {
        if (destroyed)
        {
            return;
        }

        float rad = sphereCollider ? sphereCollider.radius : colliderRadius;

        Vector3 dir = rb.velocity;
        float dist = dir.magnitude * Time.deltaTime;

        if (rb.useGravity)
        {
            // �d�͂��l�����ĕ����𐳊m�Ɍv�Z
            dir += Physics.gravity * Time.deltaTime;
            dist = dir.magnitude * Time.deltaTime;
        }

        RaycastHit hit;
        if (Physics.SphereCast(myTransform.position, rad, dir, out hit, dist))
        {
            // �Փˎ��̏���
            HandleCollision(hit);
        }
        else
        {
            // ���ɂ�������Ȃ������ꍇ�̏���
            HandleNoCollision();
        }

        // �v���W�F�N�^�C���̐i�s�����Ɍ����ĉ�]
        RotateTowardsDirection();
    }

    private void HandleCollision(RaycastHit hit)
    {
        myTransform.position = hit.point + (hit.normal * collideOffset);

        GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;

        if (hit.transform.CompareTag("Destructible"))
        {
            // "Destructible" �^�O�����I�u�W�F�N�g��j��
            Destroy(hit.transform.gameObject);
        }

        // �O�Ղ̃p�[�e�B�N���̏���
        foreach (GameObject trail in trailParticles)
        {
            GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, trailDestroyTime);  // �V���A���C�Y�h�t�B�[���h�Őݒ肵�����ԂŔj��
        }

        Destroy(projectileParticle, trailDestroyTime);  // �V���A���C�Y�h�t�B�[���h�Őݒ肵�����ԂŔj��
        Destroy(impactP, impactParticleDestroyTime);  // �V���A���C�Y�h�t�B�[���h�Őݒ肵�����ԂŔj��
        DestroyMissile();
    }

    private void HandleNoCollision()
    {
        // �Փ˂��Ȃ������ꍇ�̏���
        destroyTimer += Time.deltaTime;

        if (destroyTimer >= destroyTimerThreshold)
        {
            DestroyMissile();
        }
    }

    private void DestroyMissile()
    {
        destroyed = true;

        // �O�Ղ̃p�[�e�B�N���̏���
        foreach (GameObject trail in trailParticles)
        {
            GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, trailDestroyTime);  // �V���A���C�Y�h�t�B�[���h�Őݒ肵�����ԂŔj��
        }

        Destroy(projectileParticle, trailDestroyTime);  // �V���A���C�Y�h�t�B�[���h�Őݒ肵�����ԂŔj��
        Destroy(gameObject);

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
        // [0]�͐e�̃R���|�[�l���g�i��������΁j
        for (int i = 1; i < trails.Length; i++)
        {
            ParticleSystem trail = trails[i];
            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2f);
            }
        }
    }

    private void RotateTowardsDirection()
    {
        if (rb.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
            float angle = Vector3.Angle(myTransform.forward, rb.velocity.normalized);
            float lerpFactor = angle * Time.deltaTime;
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, lerpFactor);
        }
    }
}