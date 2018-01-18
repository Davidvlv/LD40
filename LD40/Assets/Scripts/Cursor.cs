using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour {

    public Color green;
    public Color red;

    public bool buildable = false;
    
    Camera cam;
    SpriteRenderer spriteRenderer;
    public Tilemap tilemap;
    TowerGrid towerGrid;

    public List<TileBase> buildableTiles = new List<TileBase>();

    Vector3Int position;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        towerGrid = TowerGrid.instance;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int roundedMousePos = new Vector3Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y), 0);

        if (roundedMousePos != position)
        {
            position = roundedMousePos;
            gameObject.transform.position = new Vector3(roundedMousePos.x, roundedMousePos.y, -1);
            TileBase tile = tilemap.GetTile(position);

            if (buildableTiles.Contains(tile))
            {
                spriteRenderer.color = green;
                buildable = true;
            }
            else
            {
                spriteRenderer.color = red;
                buildable = false;
            }
        }
        
        // Mouse click but not click on UI
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Click" + position);
            if (buildable)
            {
                towerGrid.Interact(position);
            }
            else
            {
                towerGrid.CloseUI();
            }
        }
	}
}
