using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABullet : MonoBehaviour {
    public float damage;
    public float lifeTime;

    private void Awake() {
        
    }

    public void ABulletInit(float _damage, float _lifeTime) {
        damage = _damage;
        lifeTime = _lifeTime;

        StartCoroutine(Life());
    }

    private void Dead() {
        Destroy(gameObject);
    }

    private IEnumerator Life() {
        yield return new WaitForSeconds(lifeTime);
        Dead();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.isTrigger)
            Dead();
    }
}
