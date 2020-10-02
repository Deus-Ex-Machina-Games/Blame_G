using Game.LevelGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneGenerator : MonoBehaviour {
    public static Level level = new Level(0);
    [SerializeField] private Vector2 _generateStep = new Vector2(1f, 1f);
    [SerializeField] private GameObject _chunkPrefab = null;
    [SerializeField] public GameObject chunkGameObject = null;

    [HideInInspector] private static SceneGenerator _internal;
    public static SceneGenerator Internal {
        get { return _internal; }
    }

    private void Awake() {
        GenerateScene();

        _internal = this;
    }

    private void Start() {

    }

    private void Update() {

    }

    public void GenerateRoom(Chunk currentChunk, int _x, int _y) {
        GameObject _roomPrefab = Instantiate(currentChunk.rooms[_y][_x].type.prefab, new Vector2(_generateStep.x * _x, _generateStep.y * _y), Quaternion.identity, chunkGameObject.transform);
        _roomPrefab.GetComponent<RoomOnScene>().room = currentChunk.rooms[_y][_x];

        for (int i = 0; i < currentChunk.rooms[_y][_x].enemies.Count; i++)
            GenerateSpawnEnemy(currentChunk.rooms[_y][_x].enemies[i], new Vector2(_generateStep.x * _x + Random.Range(-0.24f, 0.24f), _generateStep.y * _y + Random.Range(-0.01f, 0.01f)));
    }

    public void GenerateSpawnEnemy(string name, Vector2 position) {
        CEnemy enemy = Game.Assets.Data.GetEnemyByName(name);
        EnemyController enemyController = Instantiate(enemy.prefab, position, Quaternion.identity, chunkGameObject.transform).GetComponent<EnemyController>();

        enemyController.health = enemyController.GetComponent<EHealth>();

        enemyController.health.health = enemy.heatlh;
        enemyController.health.maxHealth = enemy.heatlh;
    }

    public void GenerateScene() {
        level.GenerateNextChunck();

        chunkGameObject = Instantiate(_chunkPrefab, new Vector2(0f, 0f), Quaternion.identity);

        Chunk currentChunk = level.chunks[Player.currentID];
        for (int y = 0; y < currentChunk.rooms.Count; y++) {
            for (int x = 0; x < currentChunk.rooms[y].Count; x++) {
                GenerateRoom(currentChunk, x, y);
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
        Player.inventory.AppendItem("FAKit", 5);
        Player.inventory.AppendItem("Dominator_Bullet", 2000);

        Player.equipment.ChangeItemByIndex(0, "Head_Trash");
        Debug.Log(Player.inventory.GetDebugString());
    }
}
