using LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour {
    public static Level level = new Level(2, 8);

    private void Awake() {
        level.GenerateLevel();
        print(level.GetLevelString());
    }

    private void Start() {
        
    }

    private void Update() {

    }
}
