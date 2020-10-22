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
    public float resetSpeed;

    [Header("Background")]
    public GameObject background;
    public RawImage rawImage;
    public float movementFactorX;
    public float movementFactorY;

    //Refs & Vars
    Camera cam;
    Rigidbody2D rb;
    Vector3 movement;
    float zoom;
    Vector3 startPos = new Vector3(0, 0, -10);
    bool isResetting = false;
    Vector3 dragStart;
    

    private void Awake()
    {
        cam = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        zoom = Input.mouseScrollDelta.y;

        if (Input.GetKeyDown(KeyCode.Space))
            isResetting = true;

        if (Input.GetMouseButtonDown(1))
            dragStart = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(1))
            movement = -1 * (cam.ScreenToWorldPoint(Input.mousePosition) - dragStart);

        if (movement != Vector3.zero || transform.position == startPos)
            isResetting = false;

        if(isResetting)
            movement = startPos - transform.position;
            
        background.transform.position = new Vector2(transform.position.x, transform.position.y);
    }


    private void FixedUpdate()
    {
        transform.position = transform.position + movement * moveSpeed;
        rawImage.uvRect = new Rect(rawImage.uvRect.x + movement.x * movementFactorX, rawImage.uvRect.y + movement.y * movementFactorY, rawImage.uvRect.width, rawImage.uvRect.height);

        cam.orthographicSize += zoom * -1;
        if (cam.orthographicSize > maxZoom)
            cam.orthographicSize = maxZoom;
        else if (cam.orthographicSize < minZoom)
            cam.orthographicSize = minZoom;
    }
}
