using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapNode : MonoBehaviour
{ 
   //this is the size of the room,nothing less or more.
   public Vector2Int sizeRoom = Vector2Int.zero;

   public List<MapNodeLink> _linkToOtherNode;

   MapNode()
   {
      _linkToOtherNode = new List<MapNodeLink>();
   }

   public MapNode(Transform newTransform,Vector2Int size)
   {
      sizeRoom = size;
   }

   
   
   
}
