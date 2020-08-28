using LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Game;

public class SceneGenerator : MonoBehaviour {
    public static Level level = new Level(2, 8);
    [SerializeField] private Vector2 _generateStep = new Vector2(1f, 1f);

    private void Awake() {
        Foundations.LoadFoundations();

        level.GenerateLevel();

        Chunk currentChunk = level.chunks[Player.chunkX][Player.chunkY];
        for (int y = 0; y < currentChunk.rooms.Count; y++) {
            for (int x = 0; x < currentChunk.rooms[y].Count; x++) {
                GameObject _roomPrefab = Instantiate(currentChunk.rooms[y][x].type.prefab, new Vector2(_generateStep.x * x, _generateStep.y * y), Quaternion.identity);
                _roomPrefab.GetComponent<RoomOnScene>().room = currentChunk.rooms[y][x];
            }
        }

        print(level.GetLevelString());
    }

    private void Start() {
        
    }

    private void Update() {

    }
}
