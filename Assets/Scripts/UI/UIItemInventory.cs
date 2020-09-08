using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItemInventory : MonoBehaviour {
    [SerializeField] public Text itemName = null;
    [SerializeField] public Text countText = null;
    [SerializeField] public Image itemImage = null;
    [SerializeField] public Button itemButton = null;
    [HideInInspector] public Game.Inventory inventory = null;
    [SerializeField] public bool isEmpty = false;
    [SerializeField] public int ID = 0;

    public void SetItem(string name, Sprite image, int count = 0) {
        itemName.text = name;
        itemImage.sprite = image;
        if (count > 1) countText.text = $"{count}";
        else if (count >= 999) countText.text = "999";
        else countText.text = "";
    }
}
