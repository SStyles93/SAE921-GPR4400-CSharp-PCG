using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcgCreateRoom : MonoBehaviour
{
    //NEVER SET MINSIZEROOM TO ZERO, if zero it's go to a infinite room, this is bad for our computer.
    [Tooltip("Size of the map, this represent the boundary where the map is created. bigger map create more room")]
    [SerializeField] private Vector2Int _canvaSize;
    [Tooltip("Min size of a room, this define the minimal size possible room")]
    [SerializeField] private Vector2Int _minSizeRoom = new Vector2Int(10,10);
    [Tooltip("this ratio is the ratio that divide each room, if 0 each divided room is in equal ratio")]
    [SerializeField] [Range(0, 10)] private float _randomRange = 1f;

    [SerializeField] private GameObject mapGameObject;

    /// <summary>
    /// This methode is used to create a "base" component called Generated map and ad the first node to it.
    /// It then calls the method to cut this first node to create a lot more node.
    /// </summary>
    /// <returns>The MapScript</returns>
    public MapScript GenerateMapNodes()
    {
        //Destroy current map before creating a new one
        if(mapGameObject != null)
        DestroyImmediate(mapGameObject);

        mapGameObject = new GameObject("Generated Map");
        MapScript map = mapGameObject.AddComponent<MapScript>();
        
        List<MapNode> generatedNodes = map.mapNodes ;
        map.mapSize = _canvaSize;
        
        GameObject newListGameNode = new GameObject("List map node");
        newListGameNode.transform.parent = mapGameObject.transform;
        
        GameObject newGameNode = new GameObject("First map node");
        newGameNode.transform.parent = newListGameNode.transform;
        
        generatedNodes.Add(newGameNode.gameObject.AddComponent<MapNode>());

        generatedNodes[0].sizeRoom = _canvaSize;

        
        for (int i = 0; i < generatedNodes.Count; i++)
        {
            var nodes = generatedNodes[i];

            for (bool cuttingFinish = false; !cuttingFinish;)
            {
                MapNode newNode = cuttingMapNode(nodes, newListGameNode);

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

        return map;
    }

    /// <summary>
    /// This method is for defining and launching the cutting process to create two nodes from one.
    /// It defines if we cut verticaly or horizontaly the node and other paramaters of cutting
    /// and calls the desired method to use for cutting
    /// </summary>
    /// <param name="mapToCut">The MapNode to cut</param>
    /// <param name="listMapNode">GameObject</param>
    /// <returns>The cut MapNode</returns>
    private MapNode cuttingMapNode(MapNode mapToCut,GameObject listMapNode)
    {
        if(mapToCut.sizeRoom.x >= _minSizeRoom.x * 2 && mapToCut.sizeRoom.y >= _minSizeRoom.y * 2)
        {
            if (Random.value > 0.5)
            {
                return cuttingMapNodeHorizontal(mapToCut,listMapNode);
            }
            else
            {
                return cuttingMapNodeVertical(mapToCut,listMapNode);
            }
        }
        else
        {
            if (mapToCut.sizeRoom.x >= _minSizeRoom.x * 2)
            {
                return cuttingMapNodeHorizontal(mapToCut,listMapNode);
            }

            if (mapToCut.sizeRoom.y >= _minSizeRoom.y * 2)
            {
                return cuttingMapNodeVertical(mapToCut,listMapNode);
            }
        }

        return null;
    }


    /// <summary>
    /// Cutting in horizontal a node to create two nodes.
    /// </summary>
    /// <param name="mapToCut">The MapNode to cut</param>
    /// <param name="listMapNodes">GameObject</param>
    /// <returns>The cut MapNode</returns>
    private MapNode cuttingMapNodeHorizontal(MapNode mapToCut,GameObject listMapNodes)
    {
        GameObject newGameNode = new GameObject("Map node");
        newGameNode.transform.parent = listMapNodes.transform;

        MapNode newNode = newGameNode.AddComponent<MapNode>();
        Vector3 posNewNode = mapToCut.transform.position;
        newNode.transform.position = posNewNode;
        newNode.sizeRoom = mapToCut.sizeRoom;

        int newSizeRoomX = (int) (mapToCut.sizeRoom.x / (Random.value *_randomRange + 2));

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

    /// <summary>
    /// Cutting in vertical a node for create two node.
    /// </summary>
    /// <param name="mapToCut">The MapNode to cut</param>
    /// <param name="mapGameObject">GameObject</param>
    /// <returns>The cut MapNode</returns>
    private MapNode cuttingMapNodeVertical(MapNode mapToCut,GameObject mapGameObject)
    {
        GameObject newGameNode = new GameObject("Map node");
        newGameNode.transform.parent = mapGameObject.transform;

        MapNode newNode = newGameNode.AddComponent<MapNode>();
        Vector3 posNewNode = mapToCut.transform.position;
        newNode.transform.position = posNewNode;
        newNode.sizeRoom = mapToCut.sizeRoom;

        int newSizeRoomY = (int) (mapToCut.sizeRoom.y / (Random.value * _randomRange + 2));

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
