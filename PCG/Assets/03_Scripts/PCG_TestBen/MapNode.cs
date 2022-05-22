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
    
    //this is the type of the room, that is use for populate the room of monster crate etc..
    public PcgPopulate.RoomType roomType;

   MapNode()
   {
      linkToOtherNode = new List<MapNodeLink>();
      rootPos = 0;
   }

   public MapNode(Transform newTransform,Vector2Int size)
   {
      sizeRoom = size;
   }

   public void GrowRoot(int rootLenght,int lastLenght,PcgPopulate populate)
   {
      if (rootPos == 0)
      {
         rootPos = rootLenght;
         roomType = populate.GetTypeRoom(rootPos);

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
               targetNode.GrowRoot(rootLenght,rootPos,populate);
            }

            if (nodeLink.alreadyCheck == false)
            {
               if (targetNode.rootPos == 0)
               {
                  nodeLink.alreadyCheck = true;
                  nodeLink.rootPathing = true;
                  nodeLink.doorType = populate.GetTypeLink(rootLenght);
                  targetNode.GrowRoot(rootLenght,rootPos,populate);
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
