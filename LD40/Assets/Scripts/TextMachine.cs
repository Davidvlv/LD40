using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMachine : MonoBehaviour {

    #region Singleton
    public static TextMachine instance;

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

    public Canvas worldCanvas;
    public TextMeshProUGUI textPrefab;

    public void CreateText(Vector3 position, string text, Color color)
    {
        TextMeshProUGUI newText = Instantiate(textPrefab, worldCanvas.transform, true);
        newText.transform.position = position;
        newText.GetComponent<FadeText>().Initialize(text, color);

    }
}
