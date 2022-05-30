using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    ////Reference scripts
    //[Header("Reference Scripts")]
    //[SerializeField] private PcgCreateRoom _pcgRoom;
    //[SerializeField] private MapScript _mapScript;

    //[SerializeField] private List<GameObject> _trackedEnemies;
    //[SerializeField] private List<GameObject> _trackedBosses;

    ////Prefabs to spawn
    //[Header("Enemy1 \"Chaser\"")]
    //[SerializeField] private GameObject _enemyPrefab1;
    //[SerializeField] private int _minSpawnAmountEnemy1 = 0;
    //[SerializeField] private int _maxSpawnAmountEnemy1 = 10;
    //[Header("Enemy2 \"Shooter\"")]
    //[SerializeField] private GameObject _enemyPrefab2;
    //[SerializeField] private int _minSpawnAmountEnemy2 = 0;
    //[SerializeField] private int _maxSpawnAmountEnemy2 = 10;
    //[Header("Jar")]
    //[SerializeField] private GameObject _jarPrefab;
    //[SerializeField] private int _minSpawnAmountJar = 0;
    //[SerializeField] private int _maxSpawnAmountJar = 10;

    ////Spawning variables
    //[Header("Spawning Details")]
    //[SerializeField] private List<Vector3> spawnPositions;
    //[SerializeField] private float _spawnRange = 1.0f;

    //public List<GameObject> TrackedEnemies { get => _trackedEnemies; set => _trackedEnemies = value; }
    //public List<GameObject> TrackedBosses { get => _trackedBosses; set => _trackedBosses = value; }

    //void Start()
    //{
    //    _mapScript = _pcgRoom.MapGameObject.GetComponent<MapScript>();

    //    for (int i = 0; i < _mapScript.mapNodes.Count; i++)
    //    {
    //        //Spawning for Enemies
    //        if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.MonsterRoom)
    //        {
    //            for (int spawnAmount = 0; spawnAmount < Random.Range(_minSpawnAmountEnemy1, _maxSpawnAmountEnemy1); spawnAmount++)
    //            {
    //                GameObject ennemy = SpawnEntity(_enemyPrefab1,
    //                _mapScript.mapNodes[i].transform.position);
    //                _trackedEnemies.Add(ennemy);
    //                _mapScript.mapNodes[i].RoomPopulate.entity.Add(ennemy);
    //            }
    //        }
    //        Spawning for Bosses
    //        if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.BossRoom)
    //            {
    //                for (int spawnAmount = 0; spawnAmount < Random.Range(_minSpawnAmountEnemy2, _maxSpawnAmountEnemy2); spawnAmount++)
    //                {
    //                    GameObject Boss = SpawnEntity(_enemyPrefab2,
    //                    _mapScript.mapNodes[i].transform.position);
    //                    _trackedBosses.Add(Boss);
    //                    _mapScript.mapNodes[i].RoomPopulate.entity.Add(Boss);
    //                }
    //            }
    //        //Spawing for Jars
    //        if (_mapScript.mapNodes[i].roomType == PcgPopulate.RoomType.CrateRoom)
    //        {
    //            //Checks for the lowest size value
    //            if (_mapScript.mapNodes[i].sizeRoom.x > _mapScript.mapNodes[i].sizeRoom.y)
    //            {
    //                //and used it(divided by 4) as the spawn range
    //                _spawnRange = _mapScript.mapNodes[i].sizeRoom.y / 4.0f;
    //            }
    //            else
    //            {
    //                _spawnRange = _mapScript.mapNodes[i].sizeRoom.x / 4.0f;
    //            }




    //            for (int spawnAmount = 0; spawnAmount < Random.Range(_minSpawnAmountJar, _maxSpawnAmountJar); spawnAmount++)
    //            {
    //                GameObject Jar = SpawnEntity(_jarPrefab,
    //                _mapScript.mapNodes[i].transform.position);
    //                _mapScript.mapNodes[i].RoomPopulate.entity.Add(Jar);
    //            }
    //        }
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    foreach (var item in spawnPositions)
    //    {
    //        Gizmos.DrawSphere(new Vector3(item.x, item.y, item.z), 0.5f);
    //    }
    //}

    //public GameObject SpawnEntity(GameObject entityPrefab, Vector3 spawnPosition)
    //{
    //    return Instantiate(entityPrefab,
    //    spawnPosition + new Vector3(Random.Range(-_spawnRange, _spawnRange),Random.Range(-_spawnRange, _spawnRange), 0.0f),
    //    Quaternion.identity);        
    //}

    ///// <summary>
    ///// Sets all the enemies in Play(true)/Pause(false) state
    ///// </summary>
    ///// <param name="state">state in which the enemies have to be</param>
    //public void PauseEnemies(bool state)
    //{
    //    foreach (GameObject enemy in _trackedEnemies)
    //    {
    //        enemy.GetComponent<Enemy.EnemyAI>().PauseEnemy(state);
    //    }
    //}
}
