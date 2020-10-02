using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour { }

public abstract class EntityController : Entity {
    public virtual void Attack(bool value) { }
    public virtual void Move() { }
    public virtual void Damage(float value) { }
    public virtual void Dead() { }
}
