using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABullet : MonoBehaviour {
    public float damage;
    public float lifeTime;
    public string tagToIgnore;

    private void Awake() {
        
    }

    public void ABulletInit(float _damage, float _lifeTime, string _tag) {
        damage = _damage;
        lifeTime = _lifeTime;
        tagToIgnore = _tag;

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
            if (collision.collider.tag == tagToIgnore) Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            else {
                EntityController entity = collision.gameObject.GetComponent<EntityController>();
                if (entity) entity.Damage(damage);

                Dead();
            }
    }
}
