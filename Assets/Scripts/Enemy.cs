 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Enemy : MonoBehaviour, UIMainScene.IUIInfoContent
{
    private float m_Health;
    public float health { get { return m_Health;  } set { m_Health = value; } }

    [SerializeField] private float m_MaxHealth;
    public float maxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }

    private void FixedUpdate()
    {
        if (GetDistanceToPlayer() > 150f && !GameManager.Instance.isEnemyClose)
        {
            transform.Translate(-Vector3.forward * .5f);
        }
        else
        {
            GameManager.Instance.isEnemyClose = true;
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
    }

    public float GetDistanceToPlayer()
    {
        float distance = Mathf.Abs(GameObject.Find("Player").transform.position.z - this.transform.position.z);
        return distance;
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
