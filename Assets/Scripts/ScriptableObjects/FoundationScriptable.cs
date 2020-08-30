using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Foundation", menuName = "ScriptableObjects/Foundation", order = 1)]
public class Foundation : ScriptableObject {
    public string name, symbol;
    public int chance, brokenChance, minEnemies, maxEnemies;
    public GameObject prefab;

    [Header("Sprites")]
    public Sprite end_none;
    public Sprite end_left;
    public Sprite end_right;
}


[CreateAssetMenu(fileName = "Foundations", menuName = "ScriptableObjects/Foundations", order = 2)]
public class FoundationsScriptable : ScriptableObject {
    public Foundation[] FOUNDATIONS = { };
}
