using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[ExecuteInEditMode]
//this script is used for control manually the PCG, for debug.
public class PCGControlePanel : MonoBehaviour
{
   //here are listed all script to need to be here for the generator,all this script is in the same game object
   //than the ControlePanel
   
    [SerializeField] PcgCreateRoom _createRoom;
    [SerializeField] LinkScript _createLink;
    [SerializeField] PaintGroundandWall _paintGround;
    [SerializeField] PlayerSpawner _playerSpawner;
    [SerializeField] AstarPath _aStar;    
    private MapScript _map;

    private void Start()
    {
        StartCoroutine(CreateAndScan());
    }

    /// <summary>
    /// Launches successively all the methods in order to create a map
    /// </summary>
    public void GenerateAll()
    {
        //Generate the map
        _map = _createRoom.GenerateMapNodes();
        _createLink.CreateAllLink(_map);
        _paintGround.PaintAllGround(_map);
    }

    /// <summary>
    /// Gets the player's spawnPosition
    /// </summary>
    private void GetPlayerSpawn()
    {
        //Spawn player
        _playerSpawner.SpawnPosition = _map.mapNodes[0].transform.position;

    }

    /// <summary>
    /// Scans the map for the A* pathfinding
    /// </summary>
    private void ScanAll()
    {

        //Set A*
        _aStar.data.gridGraph.SetDimensions((int)_map.mapSize.x, (int)_map.mapSize.y, 1.0f);
        foreach (var item in _aStar.graphs)
        {
            item.Scan();
        }
    }

   //button for create the room in the PCGCreateRoom script
   public void GenerateRoom()
   {
        _map = _createRoom.GenerateMapNodes();
   }

    public void GenerateLink()
    {
        _createLink.CreateAllLink(_map);
    }

    public void paintGround()
    {
        _paintGround.PaintAllGround(_map);
    }

    public void clearGround()
    {
      _paintGround.ClearMap();
    }

    /// <summary>
    /// Corountine used to Scan the map, without a delay the scan appens too early and the scan is the result of the old map
    /// </summary>
    /// <returns>Waiting time</returns>
    public IEnumerator CreateAndScan()
    {
        GenerateAll();
        yield return new WaitForSeconds(0.1f);
        GetPlayerSpawn();
        yield return new WaitForSeconds(0.1f);
        ScanAll();
    }
}
