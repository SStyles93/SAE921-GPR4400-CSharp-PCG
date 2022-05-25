using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class PcgPopulate : MonoBehaviour
{
    

    public enum RoomType
    {
        None,//Node by default
        PlayerBase,
        MonsterRoom,
        CrateRoom,
        BossRoom

    }

    public enum LinkType
    {
        None,//Node by default
        FreeAccesses,
        BlockedByCrate,
        BlockedByDoor
    }
    
    
    //all the value is use by the populate for limit the number of room of each type.
    //each room have a associated two number that regis the number max of the room and when pop it
    //some room is dependant of a another room
    //ex: when the FirstMineral don't exist actually the MineralRoom don't spawn
    //this information is in the appropriate method that relly for each room type

    [Tooltip("Max boss in the dungeon,generally one")]
    [SerializeField] private int maxBossRoom = 1; private int _actualBossRoom = 0;
    [Tooltip("where the bossRoom is generated if the number is big, the boss room is more away of the playerBase")]
    [SerializeField] private int deltaBossRoom = 4;
    [Tooltip("this is the script that contain all access to prefab that use the populate")]
    public PrefabTank prefabTank;

    private MapScript _map;


    private void Start()
    {
        prefabTank = GetComponent<PrefabTank>();
    }

    public void SetMap(MapScript mapScript)
    {
        _map = mapScript;
    }
    public void CallPcgPopulate()
    {
        foreach (var mapNode in _map.mapNodes)
        {
            mapNode.RoomPopulate.PcgPopulate();
            mapNode.RoomPopulate.SetPrefabTank(prefabTank);
        }

        foreach (var mapNodeLink in _map.maplinks)
        {
            mapNodeLink._linkPopulate.SetPrefabTank(prefabTank);
        }
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
