using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    [SerializeField] private float _damping = 1.5f;
	[SerializeField] private Vector2 _offset = new Vector2(0, 0);
    [SerializeField] private bool _faceLeft;
    [HideInInspector] private Transform _player;
	[HideInInspector] private int _lastX;

	void Start() {
		_offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
		FindPlayer(_faceLeft);
	}

	public void FindPlayer(bool playerFaceLeft) {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_lastX = Mathf.RoundToInt(_player.position.x);
		if (playerFaceLeft) transform.position = new Vector3(_player.position.x - _offset.x, _player.position.y + _offset.y, transform.position.z);
		else transform.position = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, transform.position.z);
	}

	void Update() {
		if (_player) {
			int currentX = Mathf.RoundToInt(_player.position.x);
			if (currentX > _lastX) _faceLeft = false; else if (currentX < _lastX) _faceLeft = true;
			_lastX = Mathf.RoundToInt(_player.position.x);

			Vector3 target;
			if (_faceLeft) target = new Vector3(_player.position.x - _offset.x, _player.position.y + _offset.y, transform.position.z);
			else target = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, transform.position.z);

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, _damping * Time.deltaTime);
			transform.position = currentPosition;
		} else {
            FindPlayer(_faceLeft);
        }
	}
}
