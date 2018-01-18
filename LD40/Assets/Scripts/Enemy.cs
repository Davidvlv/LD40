using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    private Sprite sprite;
    [SerializeField]
    private GameObject goldSprite;

    [SerializeField]
    private float maxHealth;
    private float health;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int gold;

    [SerializeField]
    private int greed;

    private bool gotGold = false;

    TextMachine textMachine;

    public Color goldColor;
    public Color redColor;

    public void Start()
    {
        textMachine = TextMachine.instance;
        health = maxHealth;
    }

    /// <summary>
    /// Takes the specified amount of damage.
    /// </summary>
    /// <param name="amount">The amount of damage.</param>
    public void TakeDamage(float amount)
    {
        health -= amount;
        textMachine.CreateText(transform.position + Vector3.up * 0.2f, "-" + amount, redColor);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die(bool keepGold = true)
    {
        Spawner.instance.enemies.Remove(transform);
        if (keepGold)
        {
            GoldPile.instance.Add(gold);
            textMachine.CreateText(transform.position + Vector3.up * 0.2f, "+" + gold, goldColor);
        }
        Destroy(gameObject);
    }

    public void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed * (gotGold? -1 : 1));

        if (!gotGold)
        {
            if (transform.position.x < -6.5f)
            {
                GetGold();
            }
        }
        else
        {
            if (transform.position.x > 24)
            {
                Die(false);
            }
        }
    }

    public void GetGold()
    {
        gotGold = true;
        int stolenGold = GoldPile.instance.Remove(greed);
        gold += stolenGold;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        if (stolenGold > 0)
        {
            goldSprite.SetActive(true);
        }
    }
}
