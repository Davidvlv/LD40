using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour {

    #region Singleton
    public static TowerGrid instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple instances of TowerGrid. There should only be one instance.");
            return;
        }
        instance = this;
    }

    #endregion
    
    // UI stuff
    public GameObject newTowerPanel;
    public GameObject upgradeTowerPanel;
    public GameObject UITowerPrefab;

    public GameObject towerPrefab;
    public List<TowerData> baseTowers = new List<TowerData>();

    public Color textColor;

    private Dictionary<Vector3Int, Tower> grid = new Dictionary<Vector3Int, Tower>();
    
    Vector3Int currentPosition;

    TextMachine textMachine;
    GoldPile goldPile;

    // Use this for initialization
    void Start () {

        //get the singletons
        textMachine = TextMachine.instance;
        goldPile = GoldPile.instance;
        // set the UI for the first towers
		foreach(TowerData data in baseTowers)
        {
            SetTowerUI(newTowerPanel, data);
        }

    }
	
	void Update () {

        // Esc => close all UI
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseUI();
        }
	}

    public void Interact(Vector3Int pos)
    {
        currentPosition = pos;

        // close any previously opened UI
        CloseUI();

        // get the tower at the position
        Tower tower = GetTower(pos);
        if (tower == null)
        {
            // no tower, open the build new tower menu
            newTowerPanel.SetActive(true);
        }
        else
        {
            // open the upgrade menu of the tower at pos
            upgradeTowerPanel.SetActive(true);
            SetUpgradePanel(tower.data);
        }
    }

    public void CloseUI()
    {
        newTowerPanel.SetActive(false);
        upgradeTowerPanel.SetActive(false);

        // remove all UI from upgrade panel
        foreach(Transform child in upgradeTowerPanel.transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
    }

    private void SetUpgradePanel(TowerData tower)
    {
        foreach(TowerData upgrade in tower.upgrades)
        {
            SetTowerUI(upgradeTowerPanel, upgrade);
        }
    }

    private void SetTowerUI(GameObject parent, TowerData data)
    {
        GameObject newTower = Instantiate(UITowerPrefab, parent.transform.GetChild(0), false);
        newTower.GetComponent<TowerUI>().Initialize(data);
    }

    /// <summary>
    /// Places a tower at the speicified position if no tower already exists at the position.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="tower">The tower.</param>
    public void PlaceTower(Vector3Int pos, TowerData towerData)
    {
        if (!grid.ContainsKey(pos) && goldPile.GetGold() >= towerData.price)
        {
            GameObject obj = Instantiate(towerPrefab, currentPosition, Quaternion.identity);
            Tower tower = obj.GetComponent<Tower>();
            tower.data = towerData;
            grid.Add(pos, tower);

            goldPile.Remove(towerData.price);
            textMachine.CreateText(pos, "-" + towerData.price, textColor);
        }
    }

    public void PlaceTower(TowerData towerData)
    {
        PlaceTower(currentPosition, towerData);
        CloseUI();
    }

    /// <summary>
    /// Removes the tower at the specified position.
    /// </summary>
    /// <param name="pos">The position.</param>
    public void RemoveTower(Vector3Int pos)
    {
        if (GetTower(pos) != null)
        {
            grid.Remove(pos);
        }
    }

    /// <summary>
    /// Gets the tower at the specified position.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <returns>The tower or null if no tower at the position</returns>
    public Tower GetTower(Vector3Int pos)
    {
        Tower tower;
        grid.TryGetValue(pos, out tower);
        return tower;
    }
}
