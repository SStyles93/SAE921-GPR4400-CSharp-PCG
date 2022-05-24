using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Reference scripts
    [Header("Reference Scripts")]
    [SerializeField] private PcgCreateRoom _pcgRoom;
    [SerializeField] private MapScript _mapScript;

    [SerializeField] private List<GameObject> _trackedEnemies;
    [SerializeField] private List<GameObject> _trackedBosses;

    //Prefabs to spawn
    [Header("Enemy1 \"Chaser\"")]
    [SerializeField] private GameObject _enemyPrefab1;
    [SerializeField] private int _minSpawnAmountEnemy1 = 0;
    [SerializeField] private int _maxSpawnAmountEnemy1 = 10;
    [Header("Enemy2 \"Shooter\"")]
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private int _minSpawnAmountEnemy2 = 0;
    [SerializeField] private int _maxSpawnAmountEnemy2 = 10;

    //Spawning variables
    [Header("Spawning Details")]
    [SerializeField] private List<Vector3> spawnPositions;
    [SerializeField] private float spawnRange = 1.0f;

    public List<GameObject> TrackedEnemies { get => _trackedEnemies; set => _trackedEnemies = value; }
    public List<GameObject> TrackedBosses { get => _trackedBosses; set => _trackedBosses = value; }

    void Start()
    {
        _mapScript = _pcgRoom.MapGameObject.GetComponent<MapScript>();

        for (int i = 0; i < _mapScript.mapNodes.Count; i++)
        {
            //Spawning for enemies
            if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.MonsterRoom)
                _trackedEnemies.Add(SpawnEnemy(_enemyPrefab1,
                    _mapScript.mapNodes[i].transform.position,
                    Random.Range(_minSpawnAmountEnemy1, _maxSpawnAmountEnemy1)));

            //Spawning for boss
            if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.BossRoom)
                _trackedBosses.Add(
                    SpawnEnemy(
                        _enemyPrefab2,
                        _mapScript.mapNodes[i].transform.position,
                        Random.Range(_minSpawnAmountEnemy2, _maxSpawnAmountEnemy2)));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var item in spawnPositions)
        {
            Gizmos.DrawSphere(new Vector3(item.x, item.y, item.z), 0.5f);
        }
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition, int numbersOfEnemiesToSpawn)
    {
        for (int i = 0; i < numbersOfEnemiesToSpawn; i++)
        {
                return Instantiate(enemyPrefab,
                spawnPosition + new Vector3(Random.Range(-spawnRange, spawnRange),Random.Range(-spawnRange, spawnRange), 0.0f),
                Quaternion.identity);
        }

        return null;
    }

    /// <summary>
    /// Sets all the enemies in Play(true)/Pause(false) state
    /// </summary>
    /// <param name="state">state in which the enemies have to be</param>
    public void PauseEnemies(bool state)
    {
        foreach (GameObject enemy in _trackedEnemies)
        {
            enemy.GetComponent<Enemy.EnemyAI>().PauseEnemy(state);
        }
    }
}
