using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : ScriptableObject {

    public Sprite towerSprite;
    public Sprite bulletSprite;

    new public string name;
    public int price;
    public int sellPrice;
    public float damage;
    [Range(0, 100)]
    public float accuracy;
    public float speed;
    public float range;
    public float splash;

    public TowerData[] upgrades;
}
