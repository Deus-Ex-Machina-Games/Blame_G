using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Items/Consumable", order = 10)]
public class CConsumable : CItem {
    public float toFood;
    public float toWater;
    public float toHealth;
}
