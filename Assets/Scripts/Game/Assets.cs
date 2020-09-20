using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Assets {
    public class Data {
        public static AssetsScriptableObject Assets = Resources.Load<AssetsScriptableObject>("Assets");

        public static CItem GetItemByName(string name) {
            for (int i = 0; i < Assets.items.Length; i++)
                if (Assets.items[i].name == name) return Assets.items[i];

            return null;
        }

        public static CWeapon GetWeaponByName(string name) {
            CItem item = GetItemByName(name);

            if (item.itemType == Items.ItemType.Weapon)
                return (CWeapon)item;

            return null;
        }

        public static CGun GetGunByName(string name) {
            CWeapon weapon = GetWeaponByName(name);
            if (weapon.weaponType == Items.WeaponType.Gun) return (CGun)weapon;
            return null;
        }

        public static CKnife GetKnifeByName(string name) {
            CWeapon weapon = GetWeaponByName(name);
            if (weapon.weaponType == Items.WeaponType.Knife) return (CKnife)weapon;
            return null;
        }

        public static CEntity GetEntityByName(string name) {
            for (int i = 0; i < Assets.enemies.Length; i++)
                if (Assets.enemies[i].name == name) return Assets.enemies[i];

            return null;
        }

        public static CEnemy GetEnemyByName(string name) {
            return (CEnemy)GetEntityByName(name);
        }
    }
}
