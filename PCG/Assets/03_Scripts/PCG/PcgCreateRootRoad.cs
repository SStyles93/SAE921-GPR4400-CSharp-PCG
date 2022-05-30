using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PcgCreateRootRoad : MonoBehaviour
{
    //for some reason this is the number -1(if 6 it's work like a 5) if it's used
    [Tooltip("this is the max number of room we can have after the first room in each direction")]
    [SerializeField] private int rootLenght = 5;
    private MapScript _map;
    private PcgPopulate _populate;

    public void CreateRoot(MapScript map)
    {
        _map = map;
        _populate = GetComponent<PcgPopulate>();

        
        var firstMap = _map.mapNodes[_map.mapNodes.Count / 2];

        for (int actualRoot = 1; actualRoot < rootLenght; actualRoot++)
        {
            firstMap.GrowRoot(actualRoot,0,_populate);
        }
        

    }





}
