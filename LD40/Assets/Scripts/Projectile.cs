using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

    Transform target;
    Enemy targetScript;

    public SphereCollider splashCollider;

    private float damage, accuracy, splash;
    private float speed = 10;

    private List<GameObject> splashCollisions = new List<GameObject>();

    private TextMachine textMachine;

    public void Initialize(Transform target, float damage, float accuracy, float splash)
    {
        this.target = target;
        targetScript = target.gameObject.GetComponent<Enemy>();

        this.damage = damage;
        this.accuracy = accuracy;
        this.splash = splash;
        if (splash > 0)
        {
            splashCollider.radius = splash * 0.01f;
        }
        else
        {
            splashCollider.enabled = false;
        }
    }
    
	void Start ()
    {
        textMachine = TextMachine.instance;
    }
	
	void Update ()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // move towards target
        transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);

        // if close enough
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            // accuracy
            if (Random.Range(0,100) <= accuracy)
            {
                if (splash == 0)
                {
                    // single damage
                    targetScript.TakeDamage(damage);
                }
                else
                {
                    // splash damage
                    foreach (GameObject splashed in splashCollisions)
                    {
                        splashed.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
            else
            {
                textMachine.CreateText(transform.position + Vector3.up * 0.2f, "Miss", Color.black);
            }
            Destroy(gameObject);
        }
	}

    // splash hit
    private void OnTriggerEnter(Collider other)
    {
        splashCollisions.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        splashCollisions.Remove(other.gameObject);
    }
}
