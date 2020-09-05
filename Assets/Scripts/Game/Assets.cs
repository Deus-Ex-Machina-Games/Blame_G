using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Assets {
    public class Data {
        public static AssetsScriptableObject Assets = Resources.Load<AssetsScriptableObject>("Assets");

        public static Item GetItemByName(string name) {
            for (int i = 0; i < Assets.items.Length; i++)
                if (Assets.items[i].name == name) return Assets.items[i];

            return null;
        }

        public static Weapon GetWeaponByName(string name) {
            Item item = GetItemByName(name);

            if (item.itemType == Items.ItemType.Weapon)
                return (Weapon)item;

            return null;
        }

        public static Gun GetGunByName(string name) {
            Weapon weapon = GetWeaponByName(name);
            if (weapon.weaponType == Items.WeaponType.Gun) return (Gun)weapon;
            return null;
        }

        public static Knife GetKnifeByName(string name) {
            Weapon weapon = GetWeaponByName(name);
            if (weapon.weaponType == Items.WeaponType.Knife) return (Knife)weapon;
            return null;
        }

        public static T GetItemByNameWithCast<T>(string name) {
            Item item = GetItemByName(name);

            if (item.itemType == Items.ItemType.Weapon) {
                Weapon weapon = (Weapon)item;
                if (weapon.weaponType == Items.WeaponType.Knife) return (T)Convert.ChangeType((Knife)item, typeof(T));
                else if (weapon.weaponType == Items.WeaponType.Gun) return (T)Convert.ChangeType((Gun)item, typeof(T));
            }

            return (T)Convert.ChangeType(item, typeof(T));
        }
    }
}
