using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItemInventory : MonoBehaviour {
    [SerializeField] public Text itemName = null;
    [SerializeField] public Image itemImage = null;
    [SerializeField] public Button itemButton = null;
    [SerializeField] public bool isEmpty = false;
    [SerializeField] public int ID = 0;

    public void SetItem(string name, Sprite image) {
        itemName.text = name;
        itemImage.sprite = image;
    }
}
