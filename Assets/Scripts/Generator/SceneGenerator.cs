using Game.LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneGenerator : MonoBehaviour {
    public static Level level = new Level(0);
    [SerializeField] private Vector2 _generateStep = new Vector2(1f, 1f);
    [SerializeField] private GameObject _chunkPrefab = null;

    private void Awake() {
        GenerateScene();
    }

    private void Start() {

    }

    private void Update() {

    }

    public void GenerateScene() {
        Debug.LogWarning($"[FOUNDATIONS] {Foundations.FOUNDATIONS.Length}");

        level.GenerateNextChunck();

        GameObject chunkGameObject = Instantiate(_chunkPrefab, new Vector2(0f, 0f), Quaternion.identity);

        Chunk currentChunk = level.chunks[Player.currentID];
        for (int y = 0; y < currentChunk.rooms.Count; y++) {
            for (int x = 0; x < currentChunk.rooms[y].Count; x++) {
                GameObject _roomPrefab = Instantiate(currentChunk.rooms[y][x].type.prefab, new Vector2(_generateStep.x * x, _generateStep.y * y), Quaternion.identity, chunkGameObject.transform);
                _roomPrefab.GetComponent<RoomOnScene>().room = currentChunk.rooms[y][x];

                /*
                if (x == 0 ) _roomPrefab.GetComponent<SpriteRenderer>().sprite = currentChunk.rooms[y][x].type.end_left;
                else if (x == Foundations.chunkSizeW - 1) _roomPrefab.GetComponent<SpriteRenderer>().sprite = currentChunk.rooms[y][x].type.end_right;
                else _roomPrefab.GetComponent<SpriteRenderer>().sprite = currentChunk.rooms[y][x].type.end_none;
                */
            }
        }

        Player.SelectPlayer(Player.currentCharacterID);
        Instantiate(Player.GetCurrentCharacter().prefab, new Vector2(_generateStep.x * 0, _generateStep.y * Player.currentY), Quaternion.identity);
        Debug.Log(Player.inventory.GetDebugString());
        Player.inventory.AppendItem("Dominator");
        Player.inventory.AppendItem("GBE");
        Player.inventory.AppendItem("Head_Max");
        Player.inventory.AppendItem("Potato", 20);
        Player.inventory.AppendItem("Potato", 20);

        Player.equipment.ChangeItemByIndex(0, "Head_Trash");
        Debug.Log(Player.inventory.GetDebugString());
    }
}
