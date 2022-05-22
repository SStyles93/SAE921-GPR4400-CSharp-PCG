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
    [SerializeField] GizmosDrawPCG _GizmosDrawPcg;
    [SerializeField] PcgCreateRootRoad _rootRoad;
    private MapScript _map;

    public MapScript Map { get => _map; private set => _map = value; }

    //button for create the room in the PCGCreateRoom script
    public void GenerateRoom()
   {
        _map = _createRoom.GenerateMapNodes();
        _GizmosDrawPcg.AddMapToGizmos(_map);
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

    public void CreateRoot()
    {
        _rootRoad.CreateRoot(_map);
    }
}
