using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Spawn
{
    public GameObject enemy;
    public int threshold;
}

public class Spawner : MonoBehaviour {

    #region Singleton
    public static Spawner instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple instances of GoldPile. There should only be one instance.");
            return;
        }
        instance = this;
        //Debug.Log(instance);
    }

    #endregion

    [SerializeField]
    private Spawn[] spawns;
    private GoldPile goldPile;

    float timer;
    float timerDuration = 0.5f;

    public List<Transform> enemies = new List<Transform>();

	// Use this for initialization
	void Start () {
        goldPile = GoldPile.instance;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > timerDuration)
        {
            timer -= timerDuration;
            Spawn();
        }
	}

    private void Spawn()
    {
        int gold = goldPile.GetGold();
        foreach (Spawn spawn in spawns)
        {
            //Debug.Log(spawn.enemy.name + ", " + spawn.threshold);
            if (gold >= spawn.threshold)
            {
                // more chance for higher gold
                if (Random.Range((float)gold - (float)spawn.threshold, (float)gold + 100) > spawn.threshold + 100)
                {
                    // spawn the ene y
                    Vector3 spawnPos = transform.position + Vector3.up * Random.Range(-1f, 1f) + Vector3.right * Random.Range(0f, 1f);
                    enemies.Add(Instantiate(spawn.enemy, spawnPos, Quaternion.identity).transform);
                }
            }
        }
    }
}
