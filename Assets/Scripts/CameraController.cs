using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float maxZoom;
    public float minZoom;

    [Header("Background")]
    public GameObject background;
    public RawImage rawImage;

    //Refs & Vars
    Camera cam;
    Vector3 movement;
    float zoom;
    

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        zoom = Input.mouseScrollDelta.y;

        background.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + movement * moveSpeed;
        rawImage.uvRect = new Rect(rawImage.uvRect.x + movement.x * 0.05f, rawImage.uvRect.y + movement.y * 0.088f, rawImage.uvRect.width, rawImage.uvRect.height);

        cam.orthographicSize += zoom * -1;
        if (cam.orthographicSize > maxZoom)
            cam.orthographicSize = maxZoom;
        else if (cam.orthographicSize < minZoom)
            cam.orthographicSize = minZoom;
    }
}
