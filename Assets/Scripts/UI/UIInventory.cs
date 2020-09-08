using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
    [SerializeField] public List<UIItemInventory> uiInventory = new List<UIItemInventory> { };
    [SerializeField] private GameObject _uiItemPrefab = null;
    [SerializeField] private GameObject _uiContent = null;
    [SerializeField] private Sprite _emptyItem = null;

    public void GenerateInventoryByInventory(Game.Inventory inventory) {
        if (inventory.inventorySize == uiInventory.Count) UpdateInventory(inventory);
        else NewInventory(inventory);
    }

    private void ClearInventory() {
        for (int i = 0; i < uiInventory.Count; i++)
            Destroy(uiInventory[i].gameObject);

        uiInventory = new List<UIItemInventory> { };
    }

    private void UpdateInventory(Game.Inventory inventory) {
        for (int i = 0; i < inventory.inventorySize; i++) {
            UIItemInventory itemInventory = uiInventory[i];

            Item item = Game.Assets.Data.GetItemByName(inventory.items[i].name);
            if (item) itemInventory.SetItem(inventory.items[i].name, item.sprite, inventory.items[i].count);
            else {
                itemInventory.SetItem("", _emptyItem);
                itemInventory.isEmpty = true;
            }

            itemInventory.inventory = inventory;
            itemInventory.itemButton.onClick.AddListener(delegate { UIGame.Internal.OnClickInventoryItem(itemInventory); });
        }
    }

    private void NewInventory(Game.Inventory inventory) {
        for (int i = 0; i < inventory.inventorySize; i++) {
            UIItemInventory itemInventory = Instantiate(_uiItemPrefab, _uiContent.transform).GetComponent<UIItemInventory>();

            itemInventory.ID = i;

            if (i < inventory.items.Count) {
                Item item = Game.Assets.Data.GetItemByName(inventory.items[i].name);
                if (item) itemInventory.SetItem(inventory.items[i].name, item.sprite, inventory.items[i].count);
                else {
                    itemInventory.SetItem("", _emptyItem);
                    itemInventory.isEmpty = true;
                }

                itemInventory.inventory = inventory;
                itemInventory.itemButton.onClick.AddListener(delegate { UIGame.Internal.OnClickInventoryItem(itemInventory); });
            }

            uiInventory.Add(itemInventory);
        }
    }
}
