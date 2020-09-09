using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items {
    public enum ItemType {
        Consumable,
        Weapon,
        Armor,
        Trash,
    };

    public enum StackType {
        Stack,
        Note,
    };

    public enum Equipment {
        Equip,
        Item,
    };

    public enum EquipType {
        Head,
        Arm,
        Body,
        Legs,
    };

    public enum WeaponType {
        Knife,
        Gun,
    };
}

public abstract class CItem : ScriptableObject {
    public Game.Items.ItemType itemType = Game.Items.ItemType.Trash;
    public Game.Items.Equipment equipment = Game.Items.Equipment.Item;
    public Game.Items.StackType stackType = Game.Items.StackType.Stack;
    public new string name;
    public int chance = 10;
    public GameObject prefab;
    public Sprite sprite;
}

