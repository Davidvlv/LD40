using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour {

    private Spawner spawner;

    public TowerData data;

    public SpriteRenderer spriteRenderer;

    private Transform target;

    public GameObject projectilePrefab;

    float timer;

    private float delay;
    private float trueRange;

    public void Start()
    {
        spriteRenderer.sprite = data.towerSprite;
        spawner = Spawner.instance;
        delay = 10 / data.speed;
        trueRange = data.range * 0.01f;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            timer -= delay;
            Fire();
        }
    }

    private void Fire()
    {
        if (target == null)
        {
            target = GetClosestTransform(spawner.enemies);
            // no enemies
            if (target == null) { return; }
        }

        float distance = Vector3.Distance(transform.position, target.position);
        // enemy is in range
        //Debug.Log(Vector3.Distance(transform.position, target.position));
        //Debug.Log(data.range * 0.01f);
        if (distance <= trueRange)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Initialize(target, data.damage, data.accuracy, data.splash);
            projectileScript.spriteRenderer.sprite = data.bulletSprite;
        }
        else
        {
            target = null;
        }
    }

    Transform GetClosestTransform(Transform[] transforms)
    {
        if (transforms.Length == 0)
        {
            return null;
        }
        //get closest transform
        Transform nClosest = transforms.OrderBy(t => (t.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        return nClosest;
    }

    Transform GetClosestTransform(List<Transform> transforms)
    {
        return GetClosestTransform(transforms.ToArray());
    }

}
