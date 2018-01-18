using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour {

    private float timer;
    public float fadeTime;

    public TextMeshProUGUI text;

    public void Initialize(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        Color newColor = text.color;
        newColor.a = Mathf.Lerp(1, 0, timer / fadeTime);
        text.color = newColor;


        transform.Translate(Vector3.up * Time.deltaTime * 0.2f);

        if (timer > fadeTime)
        {
            Destroy(gameObject);
        }
	}
}
