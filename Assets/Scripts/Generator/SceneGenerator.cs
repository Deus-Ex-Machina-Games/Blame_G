using LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour {
    public static Level level = new Level(2, 8);

    void Start() {
        level.GenerateLevel();
        print(level.GetLevelString());
    }

    void Update() {

    }
}
