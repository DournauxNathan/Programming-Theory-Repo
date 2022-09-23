using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

//INHERITANCE - Parent class
//Base class for all vehicule. It will handle movement and physics of vehicule
[RequireComponent(typeof(Rigidbody))]
public abstract class Vehicule : MonoBehaviour, UIMainScene.IUIInfoContent
{
    // ENCAPSULATION
    [Header("Settings")]
    [SerializeField] private float m_MaxHealth;
    public float maxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }

    public float speed;
    public float dodgeSpeed;
    public float dodgeForce;
    public float dodgeMultiplier;
    public float dodgeCoolDown;

    public float multiplier;

    private float m_Health;
    public float health { get { return m_Health; } set { m_Health = value; } }

    private float m_DodgeCharge;
    public float dodgeCharge { get { return m_DodgeCharge; } set { m_DodgeCharge = value; } }

    private bool m_ReadyToDodge = true;
    public bool readyToDodge { get { return m_ReadyToDodge; } set { m_ReadyToDodge = value; } }

    [SerializeField] private List<Transform> anchors;
    [HideInInspector] public List<Transform> groundAnchors;
    RaycastHit[] hits = new RaycastHit[4];

    private int xRange = 35;

    private Animator m_Animator;
    public Animator animator { get { return m_Animator; } set { m_Animator = value; } }

    [Header("Animation Parameters")]
    protected string dodgeLAnimParameter = "Dodge_L";
    protected string dodgeRAnimParameter = "Dodge_R";

    [Header("SFX")]
    [SerializeField] private AudioClip dodgeEffect;
    private AudioSource m_Audiosource;
    public AudioSource audioSource { get { return m_Audiosource; } set { m_Audiosource = value; } }


    private Rigidbody m_Rigidbody;
    private float horizontalInput;

    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_Audiosource = GetComponent<AudioSource>();

        foreach (Transform anchor in anchors)
        {
            groundAnchors.Add(anchor.GetChild(0).transform);
        }
    }

    private void Start()
    {
        UIMainScene.Instance.SetSliderTo(UIMainScene.Instance.playerHealthBar, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyToDodge)
            Dodge();        
    }

    void FixedUpdate()
    {
        Move();
    }

    public float tilt;
    public Boundary boundary;

    // ABSTRACTION
    public virtual void Move()
    {
        if (!GameManager.Instance.GetGameOver())
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
        m_Audiosource.PlayOneShot(dodgeEffect, 1.0f);
        
        dodgeCharge = 0f;
        UIMainScene.Instance.dodgeCharge.fillAmount = dodgeCharge;

        StartCoroutine(nameof(IncreaseDodgeCharge));

        //Dogde will be available in 'dodgeCoolDown" time
        Invoke(nameof(ResetDodgeCoolDown), dodgeCoolDown);
    }

    IEnumerator IncreaseDodgeCharge()
    {
        while (true)
        {
            yield return new WaitForSeconds(0);
            UIMainScene.Instance.dodgeCharge.fillAmount += Time.deltaTime / dodgeCoolDown;
        }
    }

    private void ResetDodgeCoolDown()
    {        
        StopCoroutine(nameof(IncreaseDodgeCharge));
        m_ReadyToDodge = true;
        dodgeCharge = 1;
        UIMainScene.Instance.dodgeCharge.fillAmount = dodgeCharge;
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

    public virtual void SubscribeDamage(float damage)
    {
        UIMainScene.Instance.UpdateSlider(UIMainScene.Instance.playerHealthBar, damage);

        if (health <= 0)
        {
            GameManager.Instance.SetGameOver(true);
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
