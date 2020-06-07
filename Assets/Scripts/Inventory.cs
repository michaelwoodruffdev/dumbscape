using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public class Item
    {
        string imagepath;
        Image itemImage;

        public Item(string imagepath)
        {
            this.imagepath = imagepath;
        }
    }

    int panelHeight;
    int panelWidth;
    public Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        this.items = new Item[28];
        this.items[0] = new Item("./ironaxeimage.png");
        this.panelHeight = 300;
        this.panelWidth = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
