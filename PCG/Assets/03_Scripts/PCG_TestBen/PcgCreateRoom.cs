using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcgCreateRoom : MonoBehaviour
{
  [Tooltip("Size of the map, this represent the boundary where the map is created. bigger map create more room")]
    [SerializeField] private Vector2Int _canvaSize;
  [Tooltip("Min size of a room, this define the minimal size possible room")]
    [SerializeField] private Vector2Int _minSizeRoom = new Vector2Int(10,10);
    [Tooltip("this ratio is the ratio that divide each room, if 1 each divided room is in equal ration, if 0 each room is drastically different")]
    [SerializeField] [Range(0, 1)] private float _dividRatio = 0.5f;





    public void GenerateMapNodes()
    {
        GameObject mapGameObject = new GameObject("Generated Map");
        MapScript map = mapGameObject.AddComponent<MapScript>();
        
        List<MapNode> generatedNodes = map.mapNodes ;

        GameObject newGameNode = new GameObject("First map node");
        newGameNode.transform.parent = mapGameObject.transform;
        
        generatedNodes.Add(newGameNode.gameObject.AddComponent<MapNode>());

        generatedNodes[0].sizeRoom = _canvaSize;

        
        for (int i = 0; i < generatedNodes.Count; i++)
        {
            var nodes = generatedNodes[i];

            for (bool cuttingFinish = false; !cuttingFinish;)
            {
                MapNode newNode = cuttingMapNode(nodes, mapGameObject);

                if (newNode != null)
                { 
                    generatedNodes.Add(newNode);
                }
                else
                {
                    cuttingFinish = true;
                }
            }
            
        }
    }


    
    //this method is for define and launch the cutting process to creat two nodes from one node
    private MapNode cuttingMapNode(MapNode mapToCut,GameObject mapGameObject)
    {
        if(mapToCut.sizeRoom.x >= _minSizeRoom.x * 2 && mapToCut.sizeRoom.y >= _minSizeRoom.y * 2)
        {
            if (Random.value > 0.5)
            {
                return cuttingMapNodeHorizontal(mapToCut,mapGameObject);
            }
            else
            {
                return cuttingMapNodeVertical(mapToCut,mapGameObject);
            }
        }
        else
        {
            if (mapToCut.sizeRoom.x >= _minSizeRoom.x * 2)
            {
                return cuttingMapNodeHorizontal(mapToCut,mapGameObject);
            }

            if (mapToCut.sizeRoom.y >= _minSizeRoom.y * 2)
            {
                return cuttingMapNodeVertical(mapToCut,mapGameObject);
            }
        }

        return null;
    }

    private MapNode cuttingMapNodeHorizontal(MapNode mapToCut,GameObject mapGameObject)
    {
        GameObject newGameNode = new GameObject("Map node");
        newGameNode.transform.parent = mapGameObject.transform;

        MapNode newNode = newGameNode.AddComponent<MapNode>();
        Vector3 posNewNode = mapToCut.transform.position;
        newNode.transform.position = posNewNode;
        newNode.sizeRoom = mapToCut.sizeRoom;

        int newSizeRoomX = (int) (mapToCut.sizeRoom.x / (Random.value * (1 - _dividRatio) + 2));

        if (newSizeRoomX % 2!= 0)
        {
            newSizeRoomX--;
        }

        if (newSizeRoomX < _minSizeRoom.x)
        {
            newSizeRoomX = _minSizeRoom.x;
        }

        mapToCut.sizeRoom.x = newSizeRoomX;
        newNode.sizeRoom.x -= newSizeRoomX;
        
        posNewNode.x -= (newNode.sizeRoom.x / 2);
        
        mapToCut.transform.position = posNewNode;
        
        posNewNode = newNode.transform.position;
        posNewNode.x += (mapToCut.sizeRoom.x / 2);
        
        newNode.transform.position = posNewNode;

        return newNode;

    }
    
    private MapNode cuttingMapNodeVertical(MapNode mapToCut,GameObject mapGameObject)
    {
        GameObject newGameNode = new GameObject("Map node");
        newGameNode.transform.parent = mapGameObject.transform;

        MapNode newNode = newGameNode.AddComponent<MapNode>();
        Vector3 posNewNode = mapToCut.transform.position;
        newNode.transform.position = posNewNode;
        newNode.sizeRoom = mapToCut.sizeRoom;

        int newSizeRoomY = (int) (mapToCut.sizeRoom.y / (Random.value * (1 - _dividRatio) + 2));

        if (newSizeRoomY % 2!= 0)
        {
            newSizeRoomY--;
        }
        
        if (newSizeRoomY < _minSizeRoom.y)
        {
            newSizeRoomY = _minSizeRoom.y;
        }

        mapToCut.sizeRoom.y = newSizeRoomY;
        newNode.sizeRoom.y -= newSizeRoomY;
        
        posNewNode.y -= (newNode.sizeRoom.y / 2);
        
        mapToCut.transform.position = posNewNode;
        
        posNewNode = newNode.transform.position;
        posNewNode.y += (mapToCut.sizeRoom.y / 2);
        
        newNode.transform.position = posNewNode;

        return newNode;
    }

    
}
