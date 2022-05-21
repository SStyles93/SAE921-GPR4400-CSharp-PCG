using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapNode : MonoBehaviour
{ 
    //this is the size of the room,nothing less or more.
    public Vector2Int sizeRoom;
   
    //this is represent the length to the first root, if 0 it's not assigned to a root.
    public int rootPos;

    //this is the list of link for accessible room.
    public List<MapNodeLink> linkToOtherNode;

   MapNode()
   {
      linkToOtherNode = new List<MapNodeLink>();
      rootPos = 0;
   }

   public MapNode(Transform newTransform,Vector2Int size)
   {
      sizeRoom = size;
   }

   public void growRoot(int rootLenght,int lastLenght)
   {
      if (rootPos == 0)
      {
         rootPos = rootLenght;
      }
      else if (rootPos >= rootLenght|| lastLenght>=rootPos )
      {
         //do nothing and finish this never append in a good behavior.
         //but for avoid crash or infinite loop this exist
         //the second condition is for we don't going back.
         
         
      }
      else
      {
         foreach (var nodeLink in linkToOtherNode)
         {

            MapNode targetNode;

            if (nodeLink.firstMapNode == this)
            {
               targetNode = nodeLink.secondMapNode;
            }
            else
            {
               targetNode = nodeLink.firstMapNode;
            }

            if (nodeLink.alreadyCheck && nodeLink.rootPathing)
            {
               targetNode.growRoot(rootLenght,rootPos);
            }

            if (nodeLink.alreadyCheck == false)
            {
               if (targetNode.rootPos == 0)
               {
                  nodeLink.alreadyCheck = true;
                  nodeLink.rootPathing = true;
                  targetNode.growRoot(rootLenght,rootPos);
               }
               else
               {
                  nodeLink.alreadyCheck = true;
               }
            }
         }
      }
   }
}
