using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLaser : AGunBehaviour {
    [SerializeField] private bool isShoot = false;
    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private float _ammoCount = 0;
    [SerializeField] private float _reloadTime = 0;

    public override void Awake() {
        _lineRenderer.enabled = false;
        _ammoCount = ammoCount;
        _reloadTime = _ammoCount * 2;
    }

    public override void Start() {

    }

    public override void Update() {
        if (Input.GetKey(KeyCode.F)) Attack();
        else isShoot = false;

        _lineRenderer.enabled = isShoot;
        
        if (!isShoot && _ammoCount < ammoCount) Reload();
    }

    public void Reload() {
        _reloadTime -= Time.deltaTime;
        if (_reloadTime <= 0) {
            _ammoCount += 1 / ammoCount;
            _reloadTime = 1 / speed;
        }

        _ammoCount = Mathf.Clamp(_ammoCount, 0, ammoCount);
    }

    public override void Attack() {
        _ammoCount -= Time.deltaTime * speed;
        _reloadTime = 1 / speed;

        if (_ammoCount > 0) Shoot();
        else isShoot = false;
    }

    private void Shoot() {
        isShoot = true;

        _lineRenderer.enabled = true;
        RaycastHit2D raycastHit = Physics2D.Raycast(_lineRenderer.transform.position, transform.right, range);
        _lineRenderer.SetPosition(0, _lineRenderer.transform.position);
        _lineRenderer.SetPosition(1, ((raycastHit) ? raycastHit.point : new Vector2(range, 0)));
    }
}
