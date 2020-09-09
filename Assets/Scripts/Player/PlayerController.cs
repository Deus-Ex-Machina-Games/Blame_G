using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private GameObject _weapon = null;
    [SerializeField] public AWeaponBehaviour _weaponBeahviour = null;
    [SerializeField] private Transform _armBone = null;
    [SerializeField] private Animator _animator = null;

    [Header("Variables")]
    [SerializeField] public bool isCanMove = true;
    [HideInInspector] private Vector3 _defaultEulers;

    [Header("AnimatorNames")]
    [SerializeField] private string _moveBoolName = "walk";

    [HideInInspector] private static PlayerController _internal;
    public static PlayerController Internal {
        get { return _internal; }
    }

    private void Awake() {
        _internal = this;
        _defaultEulers = transform.eulerAngles;
        _animator = GetComponent<Animator>();
    }

    void Start() {

    }

    void Update() {
        if (isCanMove) Move();
    }

    public void Attack(bool value) {
        if (_weaponBeahviour) _weaponBeahviour.Attack(value);
    }

    public void Move() {
        float _horizontal = CnControls.CnInputManager.GetAxis("Horizontal");

        _animator.SetBool(_moveBoolName, (_horizontal != 0));
        if (_horizontal == 0) return;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, ((_horizontal > 0) ? 0 : 180), transform.eulerAngles.z);
        transform.position += transform.right * ((_horizontal > 0) ? 1 : -1) * Game.Player.speed * _horizontal * Time.deltaTime;
    }

    public void SetNewWeapon(CWeapon weapon) {
        if (_weapon) {
            Destroy(_weapon);
            _weaponBeahviour = null;
        }
        
        _weapon = Instantiate(weapon.prefab, _armBone);
        _weaponBeahviour = _weapon.GetComponent<AWeaponBehaviour>();

        _weaponBeahviour.damage = weapon.damage;
        _weaponBeahviour.range = weapon.range;
        _weaponBeahviour.speed = weapon.speed;

        if (weapon.weaponType == Game.Items.WeaponType.Gun)
            _weaponBeahviour.ammoCount = ((CGun)weapon).ammoCount;

        if (weapon.weaponType == Game.Items.WeaponType.Gun && ((CGun)weapon).nameBullet != "") {
            CBullet bullet = (CBullet)Game.Assets.Data.GetItemByName(((CGun)weapon).nameBullet);

            _weaponBeahviour.bulletPrefab = bullet.prefab;
            _weaponBeahviour.bulletName = bullet.name;
            _weaponBeahviour.bulletLifeTime = bullet.lifeTime;
        }
    }
}
