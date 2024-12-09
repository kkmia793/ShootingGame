using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public GameObject[] projectiles;

    [Header("Projectile Settings")]
    public Transform Bullet_spawnPosition;
    public Transform Cleave_spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float speed = 1000;
    public float spawnOffset = 0.3f; // Customizable setting for offset

    [Header("Firing Settings")]
    public float fireRate = 0.13f;
    public bool isFullAuto = true;


    [Header("Gun Settings")]
    public GameObject gunPrefab; // Reference to the gun prefab
    public GameObject cleavePrefab;
    public float gunOffset = 0.5f; // Customizable setting for the gun's offset from the player

    private bool canShoot = true;
    private bool canShootCleave = true;
    private bool isJump = false;

    private GameObject instantiatedGun; // Reference to the instantiated gun
    private GameObject instantiatedCleave;
    private Animator anim;

    PlayerManager playerManager;

    private void Start()
    {
        if (gunPrefab != null)
        {
            instantiatedGun = Instantiate(gunPrefab, Vector3.zero, Quaternion.identity);
            instantiatedGun.transform.SetParent(transform); // Set the gun's parent to the player
            instantiatedGun.transform.localPosition = Vector3.zero; // Reset the local position relative to the player
        }

        if(cleavePrefab !=null)
        {
            instantiatedCleave = Instantiate(cleavePrefab, Vector3.zero, Quaternion.identity);
            instantiatedCleave.transform.SetParent(transform); // Set the gun's parent to the player
            instantiatedCleave.transform.localPosition = Vector3.zero; // Reset the local position relative to the playe
        }

        playerManager = FindObjectOfType<PlayerManager>();
        anim = GetComponent<Animator>();
        canShootCleave = playerManager.canShootCleave;

    }

    private void Update()
    {
        Get_canShootCleaveState();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
                SwitchProjectile();            
        }

        if (isFullAuto)
        {
            if (canShoot && Input.GetKey(KeyCode.Mouse0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(Shoot());
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    ShootProjectile();
                    
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && canShootCleave)
        {
            ShootCleave();
            anim.SetTrigger("attack");
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        ShootProjectile();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void ShootProjectile()
    {
        anim.SetTrigger("attack");
        // ÉvÉåÉCÉÑÅ[ÇÃâEï˚å¸Ç…íeÇî≠éÀÇ∑ÇÈ
        Vector3 direction = Bullet_spawnPosition.right;

        // íeÇÃê∂ê¨à íuÇåvéZ
        Vector3 spawnPositionWithOffset = Bullet_spawnPosition.position + direction * spawnOffset;

        // íeÇÃê∂ê¨Ç∆èâä˙ê›íË
        GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPositionWithOffset, Quaternion.identity);
        Projectile bullet = projectile.GetComponent<Projectile>();
        projectile.transform.LookAt(spawnPositionWithOffset + direction * 10f);
         if (projectiles[currentProjectile].name == "ArcaneTinyObj")
            {
                projectile.GetComponent<Rigidbody>().AddForce(direction * speed * 2); // ë¨ìxÇ2î{Ç…ê›íË
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
            }
       
    }

    private void ShootCleave()
    {
        Vector3 direction = Cleave_spawnPosition.right;
        // íeÇÃê∂ê¨à íuÇåvéZ
        Vector3 spawnPositionWithOffset = Cleave_spawnPosition.position + direction * spawnOffset;

        GameObject cleave = Instantiate(cleavePrefab, spawnPositionWithOffset, Quaternion.identity);
        Cleave cleave_01 = cleave.GetComponent<Cleave>();
        cleave.transform.LookAt(spawnPositionWithOffset + direction * 10f);
        cleave.GetComponent<Rigidbody>().AddForce(direction * speed);
        anim.SetTrigger("attack");
    }

    private void SwitchProjectile()
    {
        currentProjectile = (currentProjectile + 1) % projectiles.Length;
    }

    private bool Get_canShootCleaveState()
    {
        canShootCleave = playerManager.canShootCleave;

        return canShootCleave;
    }


}