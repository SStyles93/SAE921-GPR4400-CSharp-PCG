using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private PcgCreateRoom _pcgRoom;
    [SerializeField] private MapScript _mapScript;
    [SerializeField] private List<Vector3> spawnPositions;

    private void Awake()
    {
        _mapScript = _pcgRoom.MapGameObject.GetComponent<MapScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < _mapScript.mapNodes.Count; i++)
        {
            spawnPositions.Add(_mapScript.mapNodes[i].transform.position);
        }
        foreach (var position in spawnPositions)
        {
            SpawnEnemy(_enemyPrefab, position, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        float spawnRange = 1.0f;

        for (int i = 0; i < numbersOfEnemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab,
                spawnPosition + new Vector3(Random.Range(-spawnRange, spawnRange),Random.Range(-spawnRange, spawnRange), 0.0f),
                Quaternion.identity);
        }
    }
}
