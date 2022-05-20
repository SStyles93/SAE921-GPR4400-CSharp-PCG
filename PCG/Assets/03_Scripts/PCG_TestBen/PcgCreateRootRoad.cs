using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PcgCreateRootRoad : MonoBehaviour
{
    [Tooltip("this is the max number of room we can have after the first room in each direction")]
    [SerializeField] private int rootLenght = 5;
    private MapScript _map;


    public void CreateRoot(MapScript map)
    {
        _map = map;

        
        var firstMap = _map.mapNodes[(int)_map.mapNodes.Count / 2];

        for (int actualRoot = 1; actualRoot < rootLenght; actualRoot++)
        {
            firstMap.growRoot(actualRoot,0);
        }
        
        
        

    }





}
