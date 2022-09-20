using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - Parent class
//Base class for all turrets. It will handle aiming and shooting methods
public abstract class Turret : MonoBehaviour, UIMainScene.IUIInfoContent
{
    // ENCAPSULATION
    [Header("References")]
    [SerializeField] private Transform turret;
    [SerializeField] private Transform turretPivotPoint;

    [Header("Settings")]
    [SerializeField] private float m_Force;
    public float force { get { return m_Force; } set { m_Force = value; } }

    [SerializeField] private float m_Range;
    public float range { get { return m_Range; } set { m_Range = value; } }

    [SerializeField] private float m_FireRate;
    public float fireRate { get { return m_FireRate; } set { m_FireRate = value; } }

    [SerializeField] private bool m_ReadyToShoot = true;
    public bool readyToShoot { get { return m_ReadyToShoot; } set { m_ReadyToShoot = value; } }

    [SerializeField] private GameObject m_ProjectilePrefab;

    [Header("SFX")]
    [SerializeField] private AudioClip shot;
    private AudioSource m_Audiosource;
    public AudioSource audioSource { get { return m_Audiosource; } set { m_Audiosource = value; } }

    private void Awake()
    {
        m_Audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (turretPivotPoint != null)
        {
            turretPivotPoint.LookAt(AimToMousePosition());
        }
    }

    // ABSTRACTION
    public virtual Vector3 AimToMousePosition()
    {
        Vector3 aim = Camera.main.ScreenToWorldPoint(UIMainScene.GetMousePosition());

        return aim;
    }

    public virtual void Shoot(float multiplier)
    {
        readyToShoot = false;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(UIMainScene.GetMousePosition());

        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 difference = (hit.transform.position - turret.position).normalized;
            float distance = difference.magnitude;
            Vector3 direction = (difference / distance).normalized;

            FireBullet(direction, force * multiplier);
        }

        Invoke(nameof(ResetShoot), fireRate);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    public virtual void FireBullet(Vector3 direction, float force)
    {
        m_Audiosource.PlayOneShot(shot, 1.0f);

        GameObject projectile = Instantiate(m_ProjectilePrefab, turret.transform.position, Quaternion.identity);

        projectile.transform.rotation = Quaternion.LookRotation(direction);

        Rigidbody projectilRb = projectile.GetComponent<Rigidbody>();

        projectilRb.AddRelativeForce(direction * force);
    }

    public virtual string GetName()
    {
        return "Turret";
    }

    public virtual string GetData()
    {
        return "";
    }

    public virtual void GetContent()
    {
    }
}
