using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController {
    [SerializeField] public EHealth health = null;

    private void Awake() {

    }

    public override void Dead() {
        Destroy(gameObject);
    }

    public override void Damage(float value) {
        if (!health) health = GetComponent<EHealth>();

        health.Damage(value);
    }
}

