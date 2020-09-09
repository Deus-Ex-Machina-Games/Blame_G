using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponBehaviour : MonoBehaviour {
    public float damage = 10.0f;
    public float range = 10.0f;
    public float speed = 10.0f;
    public float ammoCount = 10.0f;
    public float defaultTime = 60.0f;

    public float bulletLifeTime = 2.0f;
    public GameObject bulletPrefab = null;
    public string bulletName = null;

    public virtual void Awake() {

    }

    public virtual void Start() {

    }

    public virtual void Update() {

    }

    public virtual void Attack(bool value) {

    }
}

public abstract class AGunBehaviour : AWeaponBehaviour {
    
}

public abstract class AKnifeBehaviour : AWeaponBehaviour {

}
