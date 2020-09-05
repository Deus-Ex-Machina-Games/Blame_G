using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryHelper : MonoBehaviour {
    [SerializeField] public UIItemInventory itemInventory = null;

    public void ChangeShow(UIItemInventory _itemInventory, bool activate) {
        gameObject.SetActive(activate);
        itemInventory = _itemInventory;
    }

    public void OnUseClick() {
        Game.Player.Use(itemInventory.ID);
        ChangeShow(null, false);

        print(Game.Player.inventory.GetDebugString());
    }
}
