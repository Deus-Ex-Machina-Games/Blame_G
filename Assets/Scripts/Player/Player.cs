using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game {
    public class Player {
        public static CCharacter[] CHARACTERS = Assets.Data.Assets.characters;

        public static int currentID = -1;
        public static int currentY = 2;

        public static int currentCharacterID = 0;

        public static float speed = 10.0f;

        public static float Health;
        public static float Hunger;
        public static float Water;

        public static Inventory inventory;
        public static Inventory equipment;

        public static void SelectPlayer(int characterID) {
            if (characterID < CHARACTERS.Length)
                currentCharacterID = characterID;
            else return;

            CCharacter character = GetCurrentCharacter();

            Health = character.maxHealth;
            Hunger = character.maxHunger;
            Water = character.maxWater;

            speed = character.maxSpeed;

            inventory = new Inventory(character.inventoryWidth);
            equipment = new Inventory(4); // 0 - Head; 1 - Arms (For weapons); 2 - Body; 3 - Legs; 
        }

        public static void Use(int index) {
            CItem item = Assets.Data.GetItemByName(inventory.items[index].name);

            if (!item) return;

            if (item.equipment == Items.Equipment.Equip) {
                CEquipment equipment = (CEquipment)item;
                Equip(equipment.name, index, equipment.equipType);
            }

            if (item.itemType == Items.ItemType.Consumable) {
                CConsumable consumable = (CConsumable)item;
                CCharacter character = GetCurrentCharacter();

                Health = Mathf.Clamp(Health + consumable.toHealth, 0, character.maxHealth);
                Hunger = Mathf.Clamp(Hunger + consumable.toFood, 0, character.maxHunger);
                Water = Mathf.Clamp(Water + consumable.toWater, 0, character.maxWater);

                inventory.RemoveItemByIndex(index, 1);
            }
        }

        public static void Equip(string name, int index, Game.Items.EquipType equipType) {
            string t_lastItem = equipment.items[(int)equipType].name;

            equipment.ChangeItemByIndex((int)equipType, name);
            inventory.ChangeItemByIndex(index, t_lastItem);

            if (equipType == Game.Items.EquipType.Arm)
                PlayerController.Internal.SetNewWeapon(Assets.Data.GetWeaponByName(name));

        }

        public static void Remove(int index) {
            inventory.RemoveItemByIndex(index, -1);
        }

        public static CCharacter GetCurrentCharacter() {
            return CHARACTERS[currentCharacterID];
        }

        public static CCharacter GetCharacterByIndex(int index) {
            return CHARACTERS[index];
        }
    }

    [System.Serializable]
    public class ItemInventory {
        public string name;
        public int count;

        public ItemInventory(string _name, int _count) {
            name = _name;
            count = _count;
        }
    }

    [System.Serializable]
    public class Inventory {
        public int inventorySize = 1;
        public List<ItemInventory> items = new List<ItemInventory> { };
        public ItemInventory itemEmpty = new ItemInventory("Empty", 0);
        public bool isChanged = false;

        public Inventory(int _inventorySize) {
            inventorySize = _inventorySize;

            for (int i = 0; i < inventorySize; i++)
                items.Add(new ItemInventory(itemEmpty.name, itemEmpty.count));

            isChanged = true;
        }

        public int GetFirstEmptyCell() {
            for (int i = 0; i < items.Count; i++)
                if (items[i].name == itemEmpty.name && items[i].count == itemEmpty.count) return i;

            return -1;
        }

        public int GetStackIdCell(string name) {
            CItem item = Assets.Data.GetItemByName(name);
            if (item.stackType == Items.StackType.Stack)
                for (int i = 0; i < items.Count; i++)
                    if (items[i].name == name) return i;

            return -1;
        }

        public void AppendItem(string name, int _count = 1) {
            int stackCell = GetStackIdCell(name);
            if (stackCell != -1) {
                items[stackCell].count += _count;
            } else {
                int cell = GetFirstEmptyCell();
                if (cell != -1) {
                    items[cell].name = name;
                    items[cell].count = _count;
                }
            }
            isChanged = true;
        }

        public void RemoveItemByIndex(int index, int _count = 1) {
            if (_count == -1 || items[index].count - _count <= 0) items[index] = new ItemInventory(itemEmpty.name, itemEmpty.count);
            else items[index].count -= _count;
            isChanged = true;
        }

        public void ChangeItemByIndex(int index, string value, int count = 1) {
            items[index].name = value;
            items[index].count = count;
            isChanged = true;
        }

        public string GetDebugString() {
            string t_string = "";

            for (int i = 0; i < items.Count; i++)
                t_string += $"{{ {i}: {{ {items[i].name} : {items[i].count} }} }},\n";

            return t_string;
        }
    }
}
