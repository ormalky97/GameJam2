using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePopup : MonoBehaviour
{
    public Text txt;
    public Image icon;

    public int type;
    public int amount;
    public float decay;
    public List<Sprite> sprites;

    float alpha = 1;

    private void Awake()
    {
        sprites = new List<Sprite>();
    }

    public void Set(int type, int amount)
    {
        icon.sprite = sprites[type];
        txt.text = "" + amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha <= 0)
            Destroy(gameObject);
        else
        {
            icon.color = new Color(1, 1, 1, alpha);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
        }

        alpha -= decay * Time.deltaTime;
        transform.position += Vector3.up * decay * Time.deltaTime;
    }
}
