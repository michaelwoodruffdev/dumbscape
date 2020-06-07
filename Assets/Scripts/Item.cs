using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemPath;
    public Sprite itemSprite;
    public Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        this.itemPath = "Items/ironaxeimage.png";
        this.itemImage = this.GetComponent<Image>();
        this.itemImage.sprite = Resources.Load<Sprite>(this.itemPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
