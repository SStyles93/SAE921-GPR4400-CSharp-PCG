using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class RoomPopulate : MonoBehaviour
{
    protected PrefabLibrary _prefabLibrary;
    protected GameManager _gameManager;

    public List<GameObject> entity = new List<GameObject>();

    protected MapNode _myMapNode;
    protected BoxCollider2D _roomCollider;
    protected bool _roomActive = false;

    protected float _spawnRange = 1f;
    protected float _minSpawnAmount = 0f;
    protected float _maxSpawnAmount = 10f;


    public virtual void PcgPopulate()
    {
        _roomCollider = gameObject.AddComponent<BoxCollider2D>();
        _roomCollider.size = _myMapNode.sizeRoom - new Vector2Int(3,3);
        _roomCollider.isTrigger = true;
    }

    /// <summary>
    /// Sets the Prefab Library
    /// </summary>
    /// <param name="prefabLibrary">The Library to be given to the RoomPopulate</param>
    public void SetPrefabLibrary(PrefabLibrary prefabLibrary)
    {
        _prefabLibrary = prefabLibrary;
    }

    /// <summary>
    /// Sets the Game Manager in the RoomPopulate
    /// </summary>
    /// <param name="gameManager">The GameManager to be given to RoomPopulate</param>
    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    /// <summary>
    /// Sets the MapNode in the RoomPopulate
    /// </summary>
    /// <param name="mapNode">The MapNode to be given to RoomPopulate</param>
     public void SetMapNode(MapNode mapNode)
     {
         _myMapNode = mapNode;
     }

    /// <summary>
    /// Sets the values for the spawning method
    /// </summary>
    /// <param name="spawnRange">Range of spawn</param>
    /// <param name="minSpawnAmount">Mininal amount of entities to spawn</param>
    /// <param name="maxSpawnAmount">Maximal amount of entities to spawn</param>
    public void SetSpawningValues(float spawnRange, int minSpawnAmount, int maxSpawnAmount)
    {
        _spawnRange = spawnRange;
        _minSpawnAmount = minSpawnAmount;
        _maxSpawnAmount = maxSpawnAmount;
    }

    /// <summary>
    /// Method used to Spawn entities 
    /// </summary>
    /// <param name="entityPrefab">The entity to spawn</param>
    /// <param name="spawnPosition">the spawning position</param>
    /// <returns>The entity spawned</returns>
    public GameObject SpawnEntity(GameObject entityPrefab, Vector3 spawnPosition)
    {
        return Instantiate(entityPrefab,
        spawnPosition + new Vector3(Random.Range(-_spawnRange, _spawnRange), Random.Range(-_spawnRange, _spawnRange), 0.0f),
        Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D col)
     {
         if (col.CompareTag("Player"))
         {
             _roomActive = true;
             if (entity.Count != 0)
             {
                 CloseDoors();
             }
             
         }
     }

     

     private void CloseDoors()
     {
         foreach (var mapNodeLink in _myMapNode.linkToOtherNode)
         {
             mapNodeLink.CloseDoor();
         }
     }

     private void Update()
     {
         if (entity.Count == 0 && _roomActive)
         {
             foreach (var mapNodeLink in _myMapNode.linkToOtherNode)
             {
                 mapNodeLink.OpenDoor();
             }

             _roomActive = false;
         }

         for (int i = entity.Count - 1; i >= 0; i--)
         {
             if (entity[i] == null)
             {
                 entity.RemoveAt(i);
                 
             }
         }
     }
     
     
}
