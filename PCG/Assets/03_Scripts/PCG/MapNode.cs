using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
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
   [SerializeField] private RoomPopulate _roomPopulate;

    private int _minEnemySpawnAmount = 1;
    private int _maxEnemySpawnAmount = 5;
    private int _minBossSpawnAmount = 1;
    private int _maxBossSpawnAmount = 1;
    private int _minCrateSpawnAmount = 1;
    private int _maxCrateSpawnAmount = 5;

    //Properties
    public RoomPopulate RoomPopulate => _roomPopulate;

    MapNode()
    {
      linkToOtherNode = new List<MapNodeLink>();
      rootPos = 0;
    }
   

   public MapNode(Transform newTransform,Vector2Int size)
   {
      sizeRoom = size;
   }

    /// <summary>
    /// Sets all spawing values
    /// </summary>
    /// <param name="minEnemySpawnAmount"></param>
    /// <param name="maxEnemySpawnAmount"></param>
    /// <param name="minBossSpawnAmount"></param>
    /// <param name="maxBossSpawnAmount"></param>
    /// <param name="minCrateSpawnAmount"></param>
    /// <param name="maxCrateSpawnAmount"></param>
    public void SetSpawningValues(
        int minEnemySpawnAmount, int maxEnemySpawnAmount,
        int minBossSpawnAmount, int maxBossSpawnAmount,
        int minCrateSpawnAmount, int maxCrateSpawnAmount)
    {
        _minEnemySpawnAmount = minEnemySpawnAmount;
        _maxEnemySpawnAmount = maxEnemySpawnAmount;

        _minBossSpawnAmount = minBossSpawnAmount;
        _maxBossSpawnAmount = maxBossSpawnAmount;

        _minCrateSpawnAmount = minCrateSpawnAmount;
        _maxBossSpawnAmount = maxBossSpawnAmount;
    }

   public void GrowRoot(int rootLenght, int lastLenght,PcgPopulate populate)
   {
      if (rootPos == 0)
      {
         rootPos = rootLenght;
         roomType = populate.GetTypeRoom(rootPos);

         switch (roomType)
         {
            case PcgPopulate.RoomType.BossRoom:
                    gameObject.AddComponent(typeof(BossRoomPopulate));
                    _roomPopulate = GetComponent<RoomPopulate>();
                    _roomPopulate.SetPrefabLibrary(populate.PrefabTank);
                    _roomPopulate.SetGameManager(populate.GameManager);
                    _roomPopulate.SetSpawningValues(_minBossSpawnAmount, _maxBossSpawnAmount);
                    _roomPopulate.SetMapNode(this);
                    break;

               
            case PcgPopulate.RoomType.MonsterRoom:
                    gameObject.AddComponent(typeof(MonsterRoomPopulate));
                    _roomPopulate = GetComponent<RoomPopulate>();
                    _roomPopulate.SetPrefabLibrary(populate.PrefabTank);
                    _roomPopulate.SetGameManager(populate.GameManager);
                    _roomPopulate.SetSpawningValues(_minEnemySpawnAmount, _maxEnemySpawnAmount);
                    _roomPopulate.SetMapNode(this);
                    break;
               
            case PcgPopulate.RoomType.CrateRoom:
                    gameObject.AddComponent(typeof(CrateRoomPopulate));
                    _roomPopulate = GetComponent<RoomPopulate>();
                    _roomPopulate.SetPrefabLibrary(populate.PrefabTank);
                    _roomPopulate.SetGameManager(populate.GameManager);
                    _roomPopulate.SetSpawningValues(_minCrateSpawnAmount, _maxCrateSpawnAmount);
                    _roomPopulate.SetMapNode(this);
                    break;
               
            case PcgPopulate.RoomType.PlayerBase:
                    gameObject.AddComponent(typeof(PlayerBasePopulate));
                    _roomPopulate = GetComponent<RoomPopulate>();
                    _roomPopulate.SetPrefabLibrary(populate.PrefabTank);
                    _roomPopulate.SetGameManager(populate.GameManager);
                    _roomPopulate.SetMapNode(this);
                    break;
               
            default:
                    break;
         }
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
               targetNode.GrowRoot(rootLenght, rootPos, populate);
            }

            if (nodeLink.alreadyCheck == false)
            {
               if (targetNode.rootPos == 0)
               {
                  nodeLink.alreadyCheck = true;
                  nodeLink.rootPathing = true;
                  nodeLink.doorType = populate.GetTypeLink(rootLenght);

                    switch (nodeLink.doorType)
                    {
                        case PcgPopulate.LinkType.FreeAccesses:
                            nodeLink.gameObject.AddComponent(typeof(FreeAccessPopulate));
                            nodeLink.SetPopulate(nodeLink.GetComponent<LinkPopulate>());
                            break;

                        case PcgPopulate.LinkType.BlockedByCrate:
                            nodeLink.gameObject.AddComponent(typeof(BlockedByCratePopulate));
                            nodeLink.SetPopulate(nodeLink.GetComponent<LinkPopulate>());
                            break;

                        case PcgPopulate.LinkType.BlockedByDoor:
                            nodeLink.gameObject.AddComponent(typeof(DoorPopulate));
                            nodeLink.SetPopulate(nodeLink.GetComponent<LinkPopulate>());
                            break;
                        default:
                            break;
                    }

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
