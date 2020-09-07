using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private GameObject _weapon = null;
    [SerializeField] private Transform _armBone = null;

    [HideInInspector] private static PlayerController _internal;
    public static PlayerController Internal {
        get { return _internal; }
    }

    private void Awake() {
        _internal = this;
    }

    void Start() {

    }

    void Update() {

    }

    public void SetNewWeapon(Weapon weapon) {
        if (_weapon) Destroy(_weapon);
        
       _weapon = Instantiate(weapon.prefab, _armBone);
    }
}
