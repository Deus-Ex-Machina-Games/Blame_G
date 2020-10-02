using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBullet : AGunBehaviour {
    [SerializeField] private bool isShoot = false;
    [SerializeField] private bool isCoolDown = false;
    [SerializeField] private Transform _spawnPoint = null;
    [SerializeField] private float _ammoCount = 0;
    [SerializeField] private float _reloadTime = 0;

    public override void Awake() {
        
    }

    public override void Start() {

    }

    public override void Update() {
        
    }

    public void Reload() {
        _reloadTime -= Time.deltaTime;
        if (_reloadTime <= 0) {
            _ammoCount += 1 / ammoCount;
            _reloadTime = 1 / speed;
        }

        _ammoCount = Mathf.Clamp(_ammoCount, 0, ammoCount);
    }

    public override void Attack(bool value) {
        isShoot = value;
        if (!isShoot) return;

        _ammoCount = Game.Player.inventory.GetCountItemsByName(bulletName);

        if (_ammoCount > 0 && !isCoolDown) StartCoroutine(IEShoot());
        else isShoot = false;
    }

    private IEnumerator IEShoot() {
        Shoot();
        isCoolDown = true;
        yield return new WaitForSeconds(speed / 2);
        isCoolDown = false;
    }

    private void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
        ABullet aBullet = bullet.GetComponent<ABullet>();

        aBullet.ABulletInit(damage, bulletLifeTime, "Player");


        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Impulse);

        int index = Game.Player.inventory.GetIndexByName(bulletName);
        Game.Player.inventory.RemoveItemByIndex(index, 1);
    }
}
