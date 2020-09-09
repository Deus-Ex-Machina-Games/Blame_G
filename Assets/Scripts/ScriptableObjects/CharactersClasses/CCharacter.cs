using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 3)]
public class CCharacter : ScriptableObject {
    public string name;

    public float maxHealth = 20.0f;
    public float maxHunger = 10.0f;
    public float maxWater = 10.0f;

    public float maxSpeed = 0.5f;

    public int inventoryWidth = 12;

    public float height = 2.0f;

    public GameObject prefab;
}
