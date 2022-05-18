using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LinkScript : MonoBehaviour
{
    [SerializeField] private MapScript map;
    [SerializeField] private uint minSizeDoor = 5;
    private GameObject _newListLink;

    // create a new game object and use the create link methode on each map node for have all link possible
    public void CreateAllLink()
    {
        _newListLink = new GameObject("List link node");
        _newListLink.transform.parent = map.transform;
        List<MapNode> mapsToLink = map.mapNodes;

        for (int it = 0; it < mapsToLink.Count; it++)
        {
            for (int i = it + 1; i < mapsToLink.Count; i++)
            {
                CreateLink(mapsToLink[it], mapsToLink[i]);
            }
        }
        
        
    }
    
    private void CreateLink(MapNode baseNode,MapNode secondNode)
    {
        
            float deltaY = baseNode.sizeRoom.y / 2 + secondNode.sizeRoom.y / 2;
            float deltaX = baseNode.sizeRoom.x / 2 + secondNode.sizeRoom.x / 2;

            var poseBaseNode = baseNode.transform.position;
            var poseSecondNode = secondNode.transform.position;
            
        
                if (math.abs(poseBaseNode.x - poseSecondNode.x) <= deltaX &&
                    math.abs(poseBaseNode.y - poseSecondNode.y) <= deltaY - minSizeDoor )
                {
                    GameObject newLink = new GameObject("Link node");
                    newLink.transform.parent = _newListLink.transform;

                    int sizeXtra;

                    if (poseBaseNode.x < poseSecondNode.x)
                    {
                        sizeXtra = baseNode.sizeRoom.x / 2;
                    }
                    else 
                    { 
                        sizeXtra = - baseNode.sizeRoom.x / 2;
                    }
                    
                    MapNodeLink newLinkNode = newLink.AddComponent<MapNodeLink>();
                    newLinkNode.transform.position = new Vector3(sizeXtra, (poseBaseNode.y +poseSecondNode.y) /2);

                    newLinkNode.firstMapNode = baseNode;
                    newLinkNode.secondMapNode = secondNode;
                    
                    map.maplinks.Add(newLinkNode);
                }
                
                if  (math.abs(poseBaseNode.y - poseSecondNode.y) <= deltaY &&
                     math.abs(poseBaseNode.x - poseSecondNode.x) <= deltaX - minSizeDoor)
                {
                    GameObject newLink = new GameObject("Link node");
                    newLink.transform.parent = _newListLink.transform;

                    int sizeYtra;

                    if (poseBaseNode.y < poseSecondNode.y)
                    {
                        sizeYtra = baseNode.sizeRoom.y / 2;
                    }else 
                    {
                        sizeYtra = - baseNode.sizeRoom.y / 2;
                    }
                    
                    MapNodeLink newLinkNode = newLink.AddComponent<MapNodeLink>();
                    newLinkNode.transform.position = new Vector3((poseBaseNode.x +poseSecondNode.x) /2, sizeYtra);
                    
                    newLinkNode.firstMapNode = baseNode;
                    newLinkNode.secondMapNode = secondNode;
                   
                    map.maplinks.Add(newLinkNode);
                }
                
    }
}
