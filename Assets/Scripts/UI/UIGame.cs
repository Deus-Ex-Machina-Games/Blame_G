using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {
    [SerializeField] private UIBar _healthBar = null;


    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Minus)) Game.Player.Health--;
        _healthBar.SetVariable(0, Game.Player.GetCurrentCharacter().maxHealth, Game.Player.Health);
    }
}
