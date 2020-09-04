using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour {
    [SerializeField] private GameObject _mask = null;
    [SerializeField] private GameObject _background = null;
    [SerializeField] private GameObject _foreground = null;

    [HideInInspector] private Vector2 _defaultMaskSize = Vector2.one;

    private void Awake() {
        _defaultMaskSize = _mask.GetComponent<RectTransform>().sizeDelta;
    }

    public void SetVariable(float min, float max, float value) {
        _mask.GetComponent<RectTransform>().sizeDelta = new Vector2(_defaultMaskSize.x * Mathf.InverseLerp(min, max, value), _defaultMaskSize.y);
    }
}
