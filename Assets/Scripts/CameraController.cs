using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float maxZoom;
    public float minZoom;

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
        
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + movement * moveSpeed;

        cam.orthographicSize += zoom * -1;
        if (cam.orthographicSize > maxZoom)
            cam.orthographicSize = maxZoom;
        else if (cam.orthographicSize < minZoom)
            cam.orthographicSize = minZoom;
    }
}
