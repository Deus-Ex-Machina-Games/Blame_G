using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemies/Enemy")]
public class CEnemy : CEntity {
    public string name;
    public GameObject prefab;
    public int chance;

    public float heatlh;    

    public float damageRate;
    public float damage;
    public float speed;
}
