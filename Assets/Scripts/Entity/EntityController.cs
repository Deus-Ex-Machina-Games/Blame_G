using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour {
    public virtual void Attack(bool value) { }
    public virtual void Move() { }
    public virtual void Damage(float value) { }
    public virtual void Dead() { }
}
