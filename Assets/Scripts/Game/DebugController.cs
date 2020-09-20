using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {
    [HideInInspector] private static DebugController _internal;
    public static DebugController Internal {
        get { return _internal; }
    }

    public void SpawnEnemy(string name, Vector2 position) {
        
    }
}
