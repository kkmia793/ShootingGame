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
        // コンポーネントの取得
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        sphereCollider = GetComponent<SphereCollider>();

        //プロジェクタイルと発射エフェクトの生成
        projectileParticle = Instantiate(projectileParticle, myTransform.position, myTransform.rotation) as GameObject;
        projectileParticle.transform.parent = myTransform;

        if (muzzleParticle)
        {
           muzzleParticle = Instantiate(muzzleParticle, myTransform.position, myTransform.rotation) as GameObject;

             //発射エフェクトの寿命
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
            // 重力を考慮して方向を正確に計算
            dir += Physics.gravity * Time.deltaTime;
            dist = dir.magnitude * Time.deltaTime;
        }

        RaycastHit hit;
        if (Physics.SphereCast(myTransform.position, rad, dir, out hit, dist))
        {
            // 衝突時の処理
            HandleCollision(hit);
        }
        else
        {
            // 何にも当たらなかった場合の処理
            HandleNoCollision();
        }

        // プロジェクタイルの進行方向に向けて回転
        RotateTowardsDirection();
    }

    private void HandleCollision(RaycastHit hit)
    {
        myTransform.position = hit.point + (hit.normal * collideOffset);

        GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;

        if (hit.transform.CompareTag("Destructible"))
        {
            // "Destructible" タグを持つオブジェクトを破壊
            Destroy(hit.transform.gameObject);
        }

        // 軌跡のパーティクルの処理
        foreach (GameObject trail in trailParticles)
        {
            GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, trailDestroyTime);  // シリアライズドフィールドで設定した時間で破棄
        }

        Destroy(projectileParticle, trailDestroyTime);  // シリアライズドフィールドで設定した時間で破棄
        Destroy(impactP, impactParticleDestroyTime);  // シリアライズドフィールドで設定した時間で破棄
        DestroyMissile();
    }

    private void HandleNoCollision()
    {
        // 衝突しなかった場合の処理
        destroyTimer += Time.deltaTime;

        if (destroyTimer >= destroyTimerThreshold)
        {
            DestroyMissile();
        }
    }

    private void DestroyMissile()
    {
        destroyed = true;

        // 軌跡のパーティクルの処理
        foreach (GameObject trail in trailParticles)
        {
            GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, trailDestroyTime);  // シリアライズドフィールドで設定した時間で破棄
        }

        Destroy(projectileParticle, trailDestroyTime);  // シリアライズドフィールドで設定した時間で破棄
        Destroy(gameObject);

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
        // [0]は親のコンポーネント（もしあれば）
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
