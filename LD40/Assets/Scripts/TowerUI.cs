using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour {

    public Image towerImage;
    public Image bulletImage;
    new public TextMeshProUGUI name;
    public TextMeshProUGUI price;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI accuracy;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI range;
    public TextMeshProUGUI splash;

    private TowerData data;

    public void Initialize(Sprite towerSprite, Sprite bulletSprite, string name, int price, float damage, float accuracy, float speed, float range, float splash)
    {
        towerImage.sprite = towerSprite;
        bulletImage.sprite = bulletSprite;
        this.name.text = name;
        this.price.text = price + "G";
        this.damage.text = damage.ToString();
        this.accuracy.text = accuracy.ToString() + "%";
        this.speed.text = speed.ToString();
        this.range.text = range.ToString();
        this.splash.text = splash.ToString();
    }

    public void Initialize(TowerData data)
    {
        this.data = data;
        Initialize(data.towerSprite, data.bulletSprite, data.name, data.price, data.damage, data.accuracy, data.speed, data.range, data.splash);
    }

    public void Select()
    {
        TowerGrid.instance.PlaceTower(data);
    }
}
