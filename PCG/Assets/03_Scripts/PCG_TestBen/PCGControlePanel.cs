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

   //button for create the room in the PCGCreateRoom script
   public void GenerateRoom()
   {
      _createRoom.GenerateMapNodes();
   }

   public void GenerateLink()
   {
      _createLink.CreateAllLink();
   }

   public void paintGround()
   {
      _paintGround.PaintAllGround();
   }

   public void clearGround()
   {
      _paintGround.ClearMap();
   }
}
