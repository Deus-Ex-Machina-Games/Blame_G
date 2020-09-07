using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryHelper : MonoBehaviour {
    [SerializeField] public UIItemInventory itemInventory = null;
    [SerializeField] public Game.Inventory inventory = null;

    public void ChangeShow(UIItemInventory _itemInventory, bool activate) {
        if (_itemInventory && _itemInventory.inventory == Game.Player.equipment) return;

        itemInventory = _itemInventory;

        if (itemInventory) transform.position = itemInventory.gameObject.GetComponent<RectTransform>().transform.position;

        gameObject.SetActive(activate);
    }

    public void OnUseClick() { 
        Game.Player.Use(itemInventory.ID);
        ChangeShow(null, false);

        print(Game.Player.inventory.GetDebugString());
    }

    public void OnRemoveClick() {
        Game.Player.Remove(itemInventory.ID);
        ChangeShow(null, false);

        print(Game.Player.inventory.GetDebugString());
    }
}
