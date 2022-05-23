using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Reference scripts
    [SerializeField] private PcgCreateRoom _pcgRoom;
    [SerializeField] private MapScript _mapScript;
    
    //Prefabs to spawn
    [SerializeField] private GameObject _enemyPrefab1;
    [SerializeField] private GameObject _enemyPrefab2;

    //Spawning variables
    [SerializeField] private List<Vector3> spawnPositions;
    [SerializeField] private float spawnRange = 1.0f;

    void Start()
    {
        _mapScript = _pcgRoom.MapGameObject.GetComponent<MapScript>();

        for (int i = 1; i < _mapScript.mapNodes.Count; i++)
        {
            //Spawning for enemies
            if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.MonsterRoom)
                SpawnEnemy(_enemyPrefab1, _mapScript.mapNodes[i].transform.position, 1);

            //Spawning for boss
            if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.BossRoom)
                SpawnEnemy(_enemyPrefab2, _mapScript.mapNodes[i].transform.position, 1);

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

    public void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition, int numbersOfEnemiesToSpawn)
    {
        for (int i = 0; i < numbersOfEnemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab,
                spawnPosition + new Vector3(Random.Range(-spawnRange, spawnRange),Random.Range(-spawnRange, spawnRange), 0.0f),
                Quaternion.identity);
        }
    }
}
