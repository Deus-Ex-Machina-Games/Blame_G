using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {
    [SerializeField] private UIBar _healthBar = null;
    [SerializeField] private UIBar _waterBar = null;
    [SerializeField] private UIBar _foodBar = null;
    [SerializeField] private GameObject _inventoryPanel = null;
    [SerializeField] private UIInventory _playerInventory = null;
    [SerializeField] private UIInventory _playerEquipment = null;
    [SerializeField] private UIInventoryHelper _inventoryHelper = null;

    [Header("BOOLS")]
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

        if (CnControls.CnInputManager.GetButton("Shoot")) PlayerController.Internal.Attack(true);
        else PlayerController.Internal.Attack(false);

        if (Input.GetKeyDown(KeyCode.Minus)) Game.Player.Health--;
        _healthBar.SetVariable(0, Game.Player.GetCurrentCharacter().maxHealth, Game.Player.Health);
        _waterBar.SetVariable(0, Game.Player.GetCurrentCharacter().maxWater, Game.Player.Water);
        _foodBar.SetVariable(0, Game.Player.GetCurrentCharacter().maxHunger, Game.Player.Hunger);
    }

    public void OnClickInventoryItem(UIItemInventory itemInventory) {
        if (!itemInventory.isEmpty)
            _inventoryHelper.ChangeShow(itemInventory, true);
    }

    public void OnClickInventoryChangeState() {
        _inventoryPanel.GetComponent<Animator>().SetTrigger("open_t");
    }

    public void OnClickShoot() {
        print("SHOT!");
        // PlayerController.Internal.Attack();
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
