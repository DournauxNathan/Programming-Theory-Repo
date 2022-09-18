using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicule : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float dodgeMultiplier;

    protected Rigidbody m_Rigidbody;
    public float multiplier;

    public Transform[] anchors;
    public Transform[] groundAnchors;
    RaycastHit[] hits = new RaycastHit[4];

    private float horizontalInput;

    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        //Get Input from player
        horizontalInput = Input.GetAxis("Horizontal");

        for (int i = 0; i < anchors.Length; i++)
        {
            ApplyForce(anchors[i], groundAnchors[i], hits[i]);
        }

        Vector3 direction = new Vector3(horizontalInput * speed, 0f, 0f);
        m_Rigidbody.AddForce(direction, ForceMode.Acceleration);
    }

    private void ApplyForce(Transform anchor, Transform groundedAnchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));

            Debug.DrawLine(anchor.position, groundedAnchor.position, Color.red);

            groundedAnchor.position = new Vector3(anchor.position.x, hit.point.y, anchor.position.z);

            m_Rigidbody.AddForceAtPosition(transform.up * force * multiplier,  anchor.position, ForceMode.Acceleration);
        }
    }

    public virtual void Dodge()
    {

    }

    public virtual void Shoot()
    {

    }

    public virtual void DisplaySpeed()
    {

    }
}
