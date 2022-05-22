using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PcgPopulate : MonoBehaviour
{
    

    public enum RoomType
    {
        None,
        PlayerBase,
        MonsterRoom,
        CrateRoom,
        BossRoom

    }

    public enum LinkType
    {
        None,
        FreeAccesses,
        BlockedByCrate,
        BlockedByDoor
    }
    
    private PcgCreateRootRoad _rootRoad;
    
    //all the value is use by the populate for limit the number of room of each type.
    //each room have a associated two number that regis the number max of the room and when pop it
    //some room is dependant of a another room
    //ex: when the FirstMineral don't exist actually the MineralRoom don't spawn
    //this information is in the appropriate method that relly for each room type

    [Tooltip("Max boss in the dungeon,generally one")]
    [SerializeField] private int maxBossRoom = 1; private int _actualBossRoom = 0;
    [Tooltip("where the bossRoom is generated if the number is big, the boss room is more away of the playerBase")]
    [SerializeField] private int deltaBossRoom = 4;


    private void Awake()
    {
        _rootRoad = GetComponent<PcgCreateRootRoad>();
    }
    
    
    public RoomType GetTypeRoom(int rootLenght)
    {
        
        if (rootLenght == 1)
        {
            return RoomType.PlayerBase;
        }


        float rdmValue = Random.value;

        if (rootLenght >= deltaBossRoom && _actualBossRoom < maxBossRoom)
        {
            _actualBossRoom++;
            return RoomType.BossRoom;
        }
        
        if (rdmValue < 0.3)
        {
            return RoomType.CrateRoom;
        }
        else
        {
            return GetMonsterRoom(rootLenght);
        }
        
        
    }

    public LinkType GetTypeLink(int rootLenght)
    {
        float rdmValue = Random.value;
        
        if (rdmValue< 0.3)
        {
            return LinkType.BlockedByCrate;
        }
        else
        {
            return LinkType.FreeAccesses;
        }
        
    }


    private RoomType GetMonsterRoom(int rootLenght)
    {
        return RoomType.MonsterRoom;
    }
    
}
