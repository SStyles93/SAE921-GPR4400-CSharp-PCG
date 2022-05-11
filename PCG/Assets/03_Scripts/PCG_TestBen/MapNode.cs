using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapNode : MonoBehaviour
{ 
   //this is the size of the room, 
   public Vector2Int sizeRoom;
   
   
   
   public MapNode(Transform newTransform,Vector2Int size)
   {
      
      sizeRoom = size;
      
   }

   
   
   
}
