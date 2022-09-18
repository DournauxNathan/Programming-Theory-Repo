using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicule : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float dodgeMultiplier;

    private float horizontalInput;

    protected Rigidbody m_Rigidbody;
    public float moveForce;

    public List<Transform> anchors;
    public float distanceToGround;
    public LayerMask canFloatOn;

    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        Move();
    }

    RaycastHit[] hits = new RaycastHit[4];
    public virtual void Move()
    {
        m_Rigidbody.AddForce(Vector3.right * moveForce * horizontalInput);

        for (int i = 0; i < anchors.Count; i++)
        {
            // Each anchor sens a raycast down
            if (Physics.Raycast(anchors[i].position, Vector3.down, out hits[i], distanceToGround, canFloatOn))
            {
                //Draw Line if the ground is hit
                Debug.DrawRay(anchors[i].position, hits[i].transform.position, Color.red, .1f);

                Debug.Log(anchors[i].name + "; " + hits[i].transform.position);

                m_Rigidbody.AddForceAtPosition(Vector3.up * moveForce * distanceToGround, anchors[i].position, ForceMode.Force);
            }
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
