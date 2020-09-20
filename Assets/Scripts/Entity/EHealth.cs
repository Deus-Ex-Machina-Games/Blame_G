using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHealth : MonoBehaviour {
    [SerializeField] public float maxHealth = 10.0f;
    [SerializeField] public float health = 10.0f;

    public void Damage(float value) {
        health = Mathf.Clamp(health - value, 0, maxHealth);
        if (health == 0) GetComponent<EntityController>().Dead();
    }
}
