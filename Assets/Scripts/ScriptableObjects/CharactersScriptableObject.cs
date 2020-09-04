using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 3)]
public class Character : ScriptableObject {
    public string name;

    public float maxHealth = 20.0f;
    public float maxHunger = 10.0f;
    public float maxWater = 10.0f;

    public int inventoryWidth = 12;

    public float height = 2.0f;

    public GameObject prefab;
}


[CreateAssetMenu(fileName = "Characters", menuName = "ScriptableObjects/Characters", order = 4)]
public class Characters : ScriptableObject {
    public Character[] CHARACTERS = { };
}
