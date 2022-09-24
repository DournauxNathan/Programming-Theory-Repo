 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Enemy : MonoBehaviour, UIMainScene.IUIInfoContent
{
    // ENCAPSULATION
    [Header("Enemy's Settings")]
    [SerializeField] private float m_MaxHealth;
    public float maxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }

    private float m_Health;
    public float health { get { return m_Health; } set { m_Health = value; } }

    [SerializeField] private int m_AppearDistance;
    public int appearDistance { get { return m_AppearDistance; } set { m_AppearDistance = value; } }


    [Header("Attack Parameters")]
    [SerializeField] private List<Transform> m_FirePoints;
    public List<Transform> firePoints { get { return m_FirePoints; } set { m_FirePoints = value; } }

    [SerializeField] private GameObject m_ProjectilePrefab;
    public GameObject projectilePrefab { get { return m_ProjectilePrefab; } set { m_ProjectilePrefab = value; } }

    [SerializeField] private bool m_playerInRange;
    public bool playerInRange { get { return m_playerInRange; } set { m_playerInRange = value; } }

    private float m_LastAttackTime = 0f;
    public float lastAttackTime { get { return m_LastAttackTime; } set { m_LastAttackTime = value; } }

    [Tooltip("How many projectiles are fire/second")][SerializeField] private float m_FireRate;
    public float fireRate { get { return m_FireRate; } set { m_FireRate = value; } }

    [SerializeField] private float m_ShootSpeed;
    public float shootSpeed { get { return m_ShootSpeed; } set { m_ShootSpeed = value; } }

    [Header("SFX Parameters")]
    private GameObject playerRef;
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip shootSFX;

    [Header("VFX")]
    [SerializeField] private ParticleSystem m_alertEffect;
    public ParticleSystem alertEffect { get { return m_ExplosionEffect; } set { m_ExplosionEffect = value; } }
    [SerializeField] private ParticleSystem m_ExplosionEffect;
    public ParticleSystem explosionEffect { get { return m_ExplosionEffect; } set { m_ExplosionEffect = value; } }

    private void Awake()
    {
        playerRef = GameObject.Find("Player");
        m_AudioSource = GetComponent<AudioSource>();
    }

    public virtual void GoTo(Vector3 direction, float speed)
    {
        if (GetDistanceToPlayer() > appearDistance && !GameManager.Instance.isEnemyClose)
        {
            transform.Translate(direction * speed);
        }
        else
        {
            StartCoroutine(nameof(IsAlert));

            playerInRange = true;
            GameManager.Instance.isEnemyClose = true;

            foreach (Transform point in firePoints)
            {
                point.LookAt(playerRef.transform);
            }

            transform.LookAt(playerRef.transform);

            if (Time.time - lastAttackTime >= 1f / fireRate)
            {
                Fire();
                lastAttackTime = Time.time;
            }
        }
    }

    public virtual void Subscribe(GameObject currentEnemy, string name, float maxHealth)
    {
        health = maxHealth;
        UIMainScene.Instance.DisplayEnemyInfo(currentEnemy, name, maxHealth);
    }

    public virtual void SubscribeDamage(float damage)
    {
        UIMainScene.Instance.currentEnemy = this.gameObject;
        UIMainScene.Instance.UpdateEnemyHealth(damage);
        health -= damage;

        if (health <= (maxHealth / 2))
        {
            explosionEffect.Play();
        }
    }

    public float GetDistanceToPlayer()
    {
        float distance = Mathf.Abs(playerRef.transform.position.z - this.transform.position.z);
        return distance;
    }

    public virtual Quaternion RotateTowards(Transform _transform)
    {
        return Quaternion.LookRotation(_transform.position - transform.position, transform.up);
    }

    public virtual Transform ShootFromPositions(List<Transform> fromList)
    {
        return fromList[Random.Range(0, fromList.Count)];
    }

    public virtual void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, ShootFromPositions(firePoints).position, RotateTowards(playerRef.transform));
        //Get the Rigidbody of the projectile Prefab
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //Shoot the Bullet in the forward direction of the player
        projectileRb.velocity = transform.forward * shootSpeed;

        m_AudioSource.PlayOneShot(shootSFX, 1.0f);
    }

    IEnumerator IsAlert()
    {
        alertEffect.Play();
        yield return new WaitForSeconds(0f);
    }

    public virtual string GetName()
    {
        return "Enemy";
    }

    public virtual string GetData()
    {
        return "";
    }

    public virtual void GetContent()
    {
    }
}
