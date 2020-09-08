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

[System.Serializable]
public abstract class Item : ScriptableObject {
    public Game.Items.ItemType itemType = Game.Items.ItemType.Trash;
    public Game.Items.Equipment equipment = Game.Items.Equipment.Item;
    public Game.Items.StackType stackType = Game.Items.StackType.Stack;
    public new string name;
    public int chance = 10;
    public GameObject prefab;
    public Sprite sprite;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Items/Consumable", order = 10)]
public class Consumable : Item {
    public float toFood;
    public float toWater;
    public float toHealth;
}

[System.Serializable]
public abstract class Equipment : Item {
    public Game.Items.EquipType equipType = Game.Items.EquipType.Arm;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Items/Weapon", order = 6)]
public class Weapon : Equipment {
    public Game.Items.WeaponType weaponType = Game.Items.WeaponType.Knife;
    public float damage = 10.0f;
    public float range = 10.0f;
    public float speed = 10.0f;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Knife", menuName = "ScriptableObjects/Items/Knife", order = 7)]
public class Knife : Weapon {
    
}

[System.Serializable]
[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Items/Gun", order = 8)]
public class Gun : Weapon {
    public int ammoCount = 3;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObjects/Items/Armor", order = 9)]
public class Armor : Equipment {
    public float armorCount = 10;
}

