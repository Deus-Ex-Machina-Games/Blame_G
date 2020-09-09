using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Foundation", menuName = "ScriptableObjects/Foundation", order = 1)]
public class CFoundation : ScriptableObject {
    public string name, symbol;
    public int chance, brokenChance, minEnemies, maxEnemies;
    public GameObject prefab;
}
