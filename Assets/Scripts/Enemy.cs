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

    public virtual void Subscribe(string name, float maxHealth)
    {
        UIMainScene.Instance.DisplayEnemyInfo(this.gameObject ,name, maxHealth);
    }

    public virtual void SubscribeDamage(float damage)
    {
        UIMainScene.Instance.currentEnemy = this.gameObject;
        UIMainScene.Instance.UpdateEnemyHealth(damage);
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
