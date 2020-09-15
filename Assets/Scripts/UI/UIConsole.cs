using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConsole : MonoBehaviour {
    [SerializeField] private Text _logger = null;
    [SerializeField] private InputField _inputField = null;
    [SerializeField] private Scrollbar _vertical = null;

    void Start() {

    }

    void Update() {
        if (_logger.text.Length != Game.Console.DebugConsole.Log.Length) {
            _logger.text += Game.Console.DebugConsole.Log.Substring(_logger.text.Length);
            _vertical.value = 0;
        }
    }

    public void OnClickSend_Button() {
        string _command = _inputField.text;
        Game.Console.DebugConsole.EnterCommand(_command);
    }
}
