using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : EntityController {
    [Header("Objects")]
    [SerializeField] private GameObject _weapon = null;
    [SerializeField] public AWeaponBehaviour _weaponBeahviour = null;
    [SerializeField] private Transform _armBone = null;
    [SerializeField] private Transform _spriteBone = null;
    [SerializeField] public Animator animator = null;
    [SerializeField] public Rigidbody2D rigidbody2D = null;

    [Header("Variables")]
    [SerializeField] public bool isCanMove = true;
    [HideInInspector] private Vector3 _defaultEulers;

    [Header("AnimatorNames")]
    [SerializeField] private string _moveBoolName = "walk";

    [Header("Abilities")]
    [SerializeField] private Game.Abilities.AAbility _activeAbility = null;
    [SerializeField] private Game.Abilities.AAbility _passiveAbility = null;

    [Header("LookAt")]
    [SerializeField] private bool canClick = true;
    [SerializeField] private Entity _entityLook = null;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Ray ray;
    [SerializeField] private RaycastHit2D hit;

    [HideInInspector] private static PlayerController _internal;
    

    public static PlayerController Internal {
        get { return _internal; }
    }

    private void Awake() {
        _internal = this;
        _defaultEulers = transform.eulerAngles;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        _activeAbility = Game.Abilities.AbilityManager.GetAbilityByName("Dash");
    }

    void Start() {

    }

    void Update() {
        if (isCanMove) Move();
        if (Input.GetKeyDown(KeyCode.R)) _activeAbility.Use();
        UpdateCheckOverlap();
    }

    public void UpdateCheckOverlap() {
#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
                Entity entity = CheckOverlapEntity(Input.GetTouch(i).position);
                if (entity) _entityLook = entity;
            }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0)) {
            Entity entity = CheckOverlapEntity(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (entity) _entityLook = entity;
        }
#endif
        OnEnemyLookAt();
    }

    private Entity CheckOverlapEntity(Vector2 position) {
        Entity entity = null;
        Collider2D collider = Physics2D.OverlapPoint(position, layerMask);

        if (!collider) return null;

        if (EventSystem.current.IsPointerOverGameObject()) return null;

        entity = collider.GetComponent<Entity>();
        if (entity && canClick) {
            StartCoroutine(CullDown());
            if (entity != this) return entity;
        }

        return null;
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        // Gizmos.color = Color.green;
        // Gizmos.DrawSphere(ray.origin, 0.01f);
    }
#endif

    public void OnEnemyLookAt() {
        if (_entityLook) _spriteBone.transform.LookAt2D(_entityLook.transform, _spriteBone.right);
        else _spriteBone.transform.LookAt2D(transform, _spriteBone.right);
    }

    IEnumerator CullDown() {
        canClick = false;
        yield return new WaitForSeconds(0.1f);
        canClick = true;
    }

    public override void Attack(bool value) {
        if (_weaponBeahviour) _weaponBeahviour.Attack(value);
    }

    public override void Move() {
        float _horizontal = CnControls.CnInputManager.GetAxis("Horizontal");

        animator.SetBool(_moveBoolName, (_horizontal != 0));
        if (_horizontal == 0) return;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, ((_horizontal > 0) ? 0 : 180), transform.eulerAngles.z);
        transform.position += transform.right * ((_horizontal > 0) ? 1 : -1) * Game.Player.speed * _horizontal * Time.deltaTime;
    }

    public override void Damage(float value) {
        Game.Player.DamageHealth(value);
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
