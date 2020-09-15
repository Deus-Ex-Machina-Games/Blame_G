using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Abilities {
    public class AbilityManager {
        public static AAbility GetAbilityByName(string className) {
            return (AAbility)Activator.CreateInstance(Type.GetType($"Game.Abilities.{className}"));
        }
    }


    public abstract class AAbility {
        public virtual void Awake() {
        }

        public virtual void Start() {
        }

        public virtual void Update() {
        }

        public virtual void Use() {
        }
    }

    public abstract class AActive : AAbility {
        public string name = "Ability";
        public float coolDown = 1.0f;
        public bool isCoolDown = false;

        public override void Use() {

        }

        public IEnumerator CoolDown() {
            isCoolDown = true;

            yield return new WaitForSeconds(coolDown);

            isCoolDown = false;
        }
    }

    public abstract class APassive : AAbility {
        public string name = "Passive";
        public float coolDown = 10.0f;
        public bool isCoolDown = false;

        public override void Use() {

        }

        public IEnumerator CoolDown() {
            isCoolDown = true;

            yield return new WaitForSeconds(coolDown);

            isCoolDown = false;
        }
    }

    public class Dash : AActive {
        private float _strength = 100.0f;

        public override void Use() {
            if (!isCoolDown) {
                Debug.Log("USE!");
                PlayerController.Internal.rigidbody2D.AddForce(PlayerController.Internal.transform.right * _strength, ForceMode2D.Impulse);
                PlayerController.Internal.StartCoroutine(CoolDown());
            }
        }
    }
}
