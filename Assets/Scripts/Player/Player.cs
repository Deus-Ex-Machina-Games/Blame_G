using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class Player {
        public static Character[] CHARACTERS = Assets.Data.Assets.characters;

        public static int currentID = -1;
        public static int currentY = 2;

        public static int currentCharacterID = 0;

        public static float Health;
        public static float Hunger;
        public static float Water;

        public static Inventory inventory;
        public static Inventory equipment;

        public static void SelectPlayer(int characterID) {
            if (characterID < CHARACTERS.Length)
                currentCharacterID = characterID;
            else return;

            Character character = GetCurrentCharacter();

            Health = character.maxHealth;
            Hunger = character.maxHunger;
            Water = character.maxWater;

            inventory = new Inventory(character.inventoryWidth);
            equipment = new Inventory(4); // 0 - Head; 1 - Arms (For weapons); 2 - Body; 3 - Legs; 
        }

        public static void Use(int index) {
            Item item = Assets.Data.GetItemByName(inventory.items[index]);

            if (!item) return;

            if (item.equipment == Items.Equipment.Equip) {
                Equipment equipment = (Equipment)item;
                Equip(equipment.name, index, equipment.equipType);
            }
        }

        public static void Equip(string name, int index, Game.Items.EquipType equipType) {
            string t_lastItem = equipment.items[(int)equipType];

            equipment.ChangeItemByIndex((int)equipType, name);
            inventory.ChangeItemByIndex(index, t_lastItem);

            if (equipType == Game.Items.EquipType.Arm)
                PlayerController.Internal.SetNewWeapon(Assets.Data.GetWeaponByName(name));

        }

        public static void Remove(int index) {
            inventory.RemoveItemByIndex(index);
        }

        public static Character GetCurrentCharacter() {
            return CHARACTERS[currentCharacterID];
        }

        public static Character GetCharacterByIndex(int index) {
            return CHARACTERS[index];
        }
    }

    [System.Serializable]
    public class Inventory {
        public int inventorySize = 1;
        public List<string> items = new List<string> { };
        public string itemEmpty = "Empty";
        public bool isChanged = false;

        public Inventory(int _inventorySize) {
            inventorySize = _inventorySize;

            for (int i = 0; i < inventorySize; i++)
                items.Add(itemEmpty);

            isChanged = true;
        }

        public int GetFirstEmptyCell() {
            for (int i = 0; i < items.Count; i++)
                if (items[i] == itemEmpty) return i;

            return -1;
        }

        public void AppendItem(string name) {
            int cell = GetFirstEmptyCell();
            if (cell != -1) items[cell] = name;
            isChanged = true;
        }

        public void RemoveItemByIndex(int index) {
            items[index] = itemEmpty;
            isChanged = true;
        }

        public void ChangeItemByIndex(int index, string value) {
            items[index] = value;
            isChanged = true;
        }

        public string GetDebugString() {
            string t_string = "";

            for (int i = 0; i < items.Count; i++)
                t_string += $"{{ {i}: {items[i]} }},\n";

            return t_string;
        }
    }
}
