using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this script on the camera
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float speed;
    public float zoomSpeed;

    public float borderLeft;
    public float borderRight;
    public float borderTop;
    public float borderBottom;

    public float maxZoom;
    public float minZoom;
    private float zoom = 3;

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        // Movement
        Vector2 motion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        motion *= Time.deltaTime * zoom * speed;

        gameObject.transform.Translate(motion);

        // Zoom
        float deltaZoom = Input.GetAxis("Mouse ScrollWheel");
        deltaZoom *= Time.deltaTime * zoomSpeed;

        zoom -= deltaZoom;

        if (zoom > maxZoom)
        {
            zoom = maxZoom;
        }
        if (zoom < minZoom)
        {
            zoom = minZoom;
        }

        cam.orthographicSize = zoom;

        CenterCamera();
	}

    private void CenterCamera()
    {
        Vector3 leftBottomEdge = cam.ScreenToWorldPoint(new Vector3(0,0,0));
        Vector3 rightTopEdge = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0));
        //Vector3 center = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 0.5f, cam.pixelHeight * 0.5f, 0));

        float left = leftBottomEdge.x;
        float right = rightTopEdge.x;
        float top = rightTopEdge.y;
        float bottom = leftBottomEdge.y;

        // if moved out of border range, move to border
        if (left < borderLeft)
        {
            cam.transform.Translate(new Vector3(borderLeft - left, 0, 0));
        }
        if (right > borderRight)
        {
            cam.transform.Translate(new Vector3(borderRight - right, 0, 0));
        }
        if (bottom < borderBottom)
        {
            cam.transform.Translate(new Vector3(0, borderBottom - bottom, 0));
        }
        if (top > borderTop)
        {
            cam.transform.Translate(new Vector3(0, borderTop - top, 0));
        }
        //Debug.Log("left : " + left + " | right : " + right + " | top : " + top + " | bottom : " + bottom);
    }
}
