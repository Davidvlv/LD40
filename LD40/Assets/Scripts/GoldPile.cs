using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldPile : MonoBehaviour {

    #region Singleton
    public static GoldPile instance;

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

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI warningText;
    TextMachine textMachine;
    public Color textColor;

    // serializefield = can view/edit it in Unity Editor
    [SerializeField]
    private int gold = 0;

    // sprites of the gold pile
    [SerializeField]
    private GameObject[] images;

    private void Start()
    {
        // gets the Text Machine singleton
        textMachine = TextMachine.instance;
        UpdateImage();
    }

    /// <summary>
    /// Adds the specified amount of gold.
    /// </summary>
    /// <param name="amount">The amount of gold.</param>
    public void Add(int amount)
    {
        gold += amount;
        UpdateImage();
    }

    /// <summary>
    /// Removes the specified amount of gold that is able
    /// </summary>
    /// <param name="amount">The amount of gold.</param>
    /// <param name="showText">Whether to show the removed amount of gold.</param>
    /// <returns>Returns the amount that was able to be removed.</returns>
    public int Remove(int amount, bool showText = true)
    {
        // if you don't have enough gold
        if (gold - amount < 0)
        {
            //shows you text of gold lost
            if (showText)
            {
                textMachine.CreateText(new Vector3(-7, -0.5f, 0), "-" + gold, textColor);
            }
            gold = 0;
            UpdateImage();
            return gold;
        }
        gold -= amount;
        //shows you text of gold lost
        if (showText)
        {
            textMachine.CreateText(new Vector3(-7, -0.5f, 0), "-" + amount, textColor);
        }
        UpdateImage();
        return amount;
    }

    public int GetGold()
    {
        return gold;
    }

    /// <summary>
    /// Updates the sprites to show how much gold you have
    /// </summary>
    private void UpdateImage()
    {
        goldText.text = gold + "G";

        images[0].SetActive(gold > 0 && gold <= 100);
        images[1].SetActive(gold > 100 && gold <= 250);
        images[2].SetActive(gold > 250 && gold <= 500);
        images[3].SetActive(gold > 500 && gold <= 1000);
        images[3].SetActive(gold > 1000 && gold <= 1500);
        images[5].SetActive(gold > 1500);
        images[6].SetActive(gold > 2000);
        images[7].SetActive(gold > 2000);
        images[8].SetActive(gold > 3000);
        images[9].SetActive(gold > 4000);

        // tells you gold is low
        warningText.enabled = (gold <= 20);

    }
}
