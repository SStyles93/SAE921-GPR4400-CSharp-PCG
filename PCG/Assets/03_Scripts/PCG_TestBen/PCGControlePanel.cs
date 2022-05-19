using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//this script is used for control manually the PCG, for debug.
public class PCGControlePanel : MonoBehaviour
{
   //here are listed all script to need to be here for the generator,all this script is in the same game object
   //than the ControlePanel
   
    [SerializeField] PcgCreateRoom _createRoom;
    [SerializeField] LinkScript _createLink;
    [SerializeField] PaintGroundandWall _paintGround;
    [SerializeField] PlayerSpawner _playerSpawner;
    
    private MapScript _map;

    /// <summary>
    /// Launches successively all the methods in order to create a map
    /// </summary>
    public void GenerateAll()
    {
        _map = _createRoom.GenerateMapNodes();
        _createLink.CreateAllLink(_map);
        _paintGround.PaintAllGround(_map);
        _playerSpawner.SpawnPosition = _map.mapNodes[0].transform.position;
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
}
