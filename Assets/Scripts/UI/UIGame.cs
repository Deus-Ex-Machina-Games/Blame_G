using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {
    [SerializeField] private UIBar _healthBar = null;
    [SerializeField] private UIInventory _playerInventory = null;
    [SerializeField] private UIInventory _playerEquipment = null;
    [SerializeField] private UIInventoryHelper _inventoryHelper = null;
    private static UIGame _internal;

    public static UIGame Internal {
        get { return _internal; }
    }

    void Awake() {
        _internal = this;

        _inventoryHelper.gameObject.SetActive(false);
    }

    void Start() {
        _playerInventory.GenerateInventoryByInventory(Game.Player.inventory);
        _playerEquipment.GenerateInventoryByInventory(Game.Player.equipment);
    }

    void Update() {
        UpdateInventory();

        if (Input.GetKeyDown(KeyCode.Minus)) Game.Player.Health--;
        _healthBar.SetVariable(0, Game.Player.GetCurrentCharacter().maxHealth, Game.Player.Health);
    }

    public void OnClickInventoryItem(UIItemInventory itemInventory) {
        if (!itemInventory.isEmpty)
            _inventoryHelper.ChangeShow(itemInventory, true);
    }

    public void UpdateInventory() {
        if (Game.Player.inventory.isChanged) {
            _playerInventory.GenerateInventoryByInventory(Game.Player.inventory);
            Game.Player.inventory.isChanged = false;
        }

        if (Game.Player.equipment.isChanged) {
            _playerEquipment.GenerateInventoryByInventory(Game.Player.equipment);
            Game.Player.equipment.isChanged = true;
        }
    }
}
