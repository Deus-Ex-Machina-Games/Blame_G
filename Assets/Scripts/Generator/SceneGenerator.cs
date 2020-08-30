using LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Game;

public class SceneGenerator : MonoBehaviour {
    public static Level level = new Level(0);
    [SerializeField] private Vector2 _generateStep = new Vector2(1f, 1f);
    [SerializeField] private GameObject _chunkPrefab = null;

    private void Awake() {
        Foundations.LoadFoundations();

        level.GenerateNextChunck();
         
        GameObject chunkGameObject = Instantiate(_chunkPrefab, new Vector2(0f, 0f), Quaternion.identity);

        Chunk currentChunk = level.chunks[Player.currentID];
        for (int y = 0; y < currentChunk.rooms.Count; y++) {
            for (int x = 0; x < currentChunk.rooms[y].Count; x++) {
                GameObject _roomPrefab = Instantiate(currentChunk.rooms[y][x].type.prefab, new Vector2(_generateStep.x * x, _generateStep.y * y), Quaternion.identity, chunkGameObject.transform);
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
