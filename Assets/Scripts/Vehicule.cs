using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE - Parent class
//Base class for all vehicule. It will handle movement and physics of vehicule
public abstract class Vehicule : MonoBehaviour, UIMainScene.IUIInfoContent
{
    // ENCAPSULATION
    private Rigidbody m_Rigidbody;
    public Rigidbody rigibody { get { return m_Rigidbody; } set { m_Rigidbody = value; } }

    public float speed;
    public float dodgeForce;
    public float dodgeMultiplier;
    public float dodgeCoolDown;

    public float multiplier;

    [SerializeField] private List<Transform> anchors;
    [HideInInspector] public List<Transform> groundAnchors;
    RaycastHit[] hits = new RaycastHit[4];

    private int xRange = 35;


    private bool m_ReadyToDodge = true;
    public bool readyToDodge { get { return m_ReadyToDodge; } set { m_ReadyToDodge = value; } }

    private Animator m_Animator;
    public Animator animator { get { return m_Animator; } set { m_Animator = value; } }

    [Header("Animation Parameters")]
    protected string dodgeAnimParameter = "Dodge";

    private float horizontalInput;

    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();

        foreach (Transform anchor in anchors)
        {
            groundAnchors.Add(anchor.GetChild(0).transform);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyToDodge)
        {
            Dodge();
        }

        Move();
    }

    // ABSTRACTION
    public virtual void Move()
    {
        //Get Input from player
        horizontalInput = Input.GetAxis("Horizontal");

        for (int i = 0; i < anchors.Count; i++)
        {
            ApplyForce(anchors[i], groundAnchors[i], hits[i]);
        }

        Vector3 direction = new Vector3(horizontalInput * speed, 0f, 0f);
        m_Rigidbody.AddForce(direction, ForceMode.Acceleration);

        KeepVehiculeInBound(xRange);
    }

    private void ApplyForce(Transform anchor, Transform groundedAnchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit, Mathf.Infinity))
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
        //Force in the direction of the horizontal input
        Vector3 forceToadd = Vector3.right * dodgeForce * dodgeMultiplier * horizontalInput;

        //Add Force
        m_Rigidbody.AddForce(forceToadd, ForceMode.Impulse);

        m_ReadyToDodge = false;

        //Dogde will be available in 'dodgeCoolDown" time
        Invoke(nameof(ResetDodgeCoolDown), dodgeCoolDown);
    }

    private void ResetDodgeCoolDown()
    {
        m_ReadyToDodge = true;
    }

    private void KeepVehiculeInBound(float rangeOfBound)
    {
        if (transform.position.x < -rangeOfBound)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rangeOfBound)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    public virtual void CalculSpeed()
    {
        speed = Mathf.RoundToInt(m_Rigidbody.velocity.magnitude * 2.237f);
    }
        public virtual string GetName()
    {
        return "Vehicule";
    }

    public virtual string GetData()
    {
        return "";
    }

    public virtual void GetContent()
    {
    }
}
