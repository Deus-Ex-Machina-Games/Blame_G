using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Items/Weapon", order = 6)]
public class CWeapon : CEquipment {
    public Game.Items.WeaponType weaponType = Game.Items.WeaponType.Knife;
    public float damage = 10.0f;
    public float range = 10.0f;
    public float speed = 10.0f;
}

