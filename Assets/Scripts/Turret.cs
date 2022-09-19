using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - Parent class
//Base class for all turrets. It will handle aiming and shooting methods
public abstract class Turret : MonoBehaviour
{
    // ENCAPSULATION
    [Header("References")]
    [SerializeField] private Transform turret;
    [SerializeField] private Transform turretPivotPoint;

    [Header("Settings")]
    [SerializeField] private float m_Range;
    public float range { get { return m_Range; } set { m_Range = value; } }

    [SerializeField] private float m_FireRate;
    public float fireRate { get { return m_FireRate; } set { m_FireRate = value; } }

    [SerializeField] private bool m_ReadyToShoot = true;
    public bool readyToShoot { get { return m_ReadyToShoot; } set { m_ReadyToShoot = value; } }

    [SerializeField] private GameObject m_ProjectilePrefab;

    private void Update()
    {
        if (turretPivotPoint != null)
        {
            turretPivotPoint.LookAt(AimToMousePosition());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot)
        {
            Shoot();
        }
    }

    // ABSTRACTION
    public virtual Vector3 AimToMousePosition()
    {
        Vector3 aim = Camera.main.ScreenToWorldPoint(UiManager.GetMousePosition());

        return aim;
    }

    public virtual void Shoot()
    {
        readyToShoot = false;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(UiManager.GetMousePosition());

        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 difference = (hit.transform.position - turret.position).normalized;
            float distance = difference.magnitude;
            Vector3 direction = (difference / distance).normalized;
            
            FireBullet(direction);
        }

        Invoke(nameof(ResetShoot), fireRate);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    public virtual void FireBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(m_ProjectilePrefab, turret.transform.position, Quaternion.identity);

        Rigidbody b = bullet.GetComponent<Rigidbody>();

        b.AddRelativeForce(direction * 1000f);
    }
}
