
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// this class is for create link on each room possible on the map.
public class LinkScript : MonoBehaviour
{
    private MapScript _map;
    [SerializeField] private uint minSizeDoor = 5;
    private GameObject _newListLink;

    // create a new game object and use the create link methode on each map node for have all link possible
    public void CreateAllLink(MapScript map)
    {
        _map = map;
        _newListLink = new GameObject("List link node")
        {transform = {parent = _map.transform}};

        for (int it = 0; it < _map.mapNodes.Count; it++)
        {
            for (int i = it + 1; i < _map.mapNodes.Count; i++)
            {
                CreateLink(_map.mapNodes[it], _map.mapNodes[i]);
            }
        }
        
        
    }
    //in this method we create somme spatial information for the link like the size of the room or where the link is created
    //and we verify if a door can be created in this link, if not the link is not created.
    private void CreateLink(MapNode baseNode,MapNode secondNode)
    {
        
            float deltaY = baseNode.sizeRoom.y / 2 + secondNode.sizeRoom.y / 2;
            float deltaX = baseNode.sizeRoom.x / 2 + secondNode.sizeRoom.x / 2;

            var poseBaseNode = baseNode.transform.position;
            var poseSecondNode = secondNode.transform.position;

            Vector2 pointBaseNode = Vector2.zero;
            Vector2 pointSecondNode = Vector2.zero;
                // this if is for control if we can place a door in the two axis.
                if ((math.abs(poseBaseNode.x - poseSecondNode.x) <= deltaX &&
                    math.abs(poseBaseNode.y - poseSecondNode.y) <= deltaY - minSizeDoor) ||
                    (math.abs(poseBaseNode.y - poseSecondNode.y) <= deltaY &&
                    math.abs(poseBaseNode.x - poseSecondNode.x) <= deltaX - minSizeDoor))
                {
                    //we create a game object for stock all the link nodes
                    GameObject newLink = new GameObject("Link node")
                    {transform = {parent = _newListLink.transform}};
                    
                    //chained if for see where the door is, this is a research by quadrant, this is probably unoptimised
                    //this is useful for telling where the link is created we need this for create the door.
                    //and we create two collision point(extremity of each room) for the door behavior.
                    if (poseBaseNode.x < poseSecondNode.x && poseBaseNode.y < poseSecondNode.y )
                    {
                        pointBaseNode = new Vector2(poseBaseNode.x + baseNode.sizeRoom.x / 2,
                            poseBaseNode.y + baseNode.sizeRoom.y / 2);
                        pointSecondNode = new Vector2(poseSecondNode.x - secondNode.sizeRoom.x / 2 ,
                            poseSecondNode.y - secondNode.sizeRoom.y / 2);
                    }else if (poseBaseNode.x >= poseSecondNode.x && poseBaseNode.y < poseSecondNode.y )
                    {
                        pointBaseNode = new Vector2(poseBaseNode.x - baseNode.sizeRoom.x / 2,
                            poseBaseNode.y + baseNode.sizeRoom.y / 2);
                        pointSecondNode = new Vector2(poseSecondNode.x + secondNode.sizeRoom.x / 2 ,
                            poseSecondNode.y - secondNode.sizeRoom.y / 2);
                    }else if (poseBaseNode.x >= poseSecondNode.x && poseBaseNode.y >= poseSecondNode.y )
                    {
                        pointBaseNode = new Vector2(poseBaseNode.x - baseNode.sizeRoom.x / 2,
                            poseBaseNode.y - baseNode.sizeRoom.y / 2);
                        pointSecondNode = new Vector2(poseSecondNode.x + secondNode.sizeRoom.x / 2 ,
                            poseSecondNode.y + secondNode.sizeRoom.y / 2);
                    }else if (poseBaseNode.x < poseSecondNode.x && poseBaseNode.y >= poseSecondNode.y )
                    {
                        pointBaseNode = new Vector2(poseBaseNode.x + baseNode.sizeRoom.x / 2,
                            poseBaseNode.y - baseNode.sizeRoom.y / 2);
                        pointSecondNode = new Vector2(poseSecondNode.x - secondNode.sizeRoom.x / 2 ,
                            poseSecondNode.y + secondNode.sizeRoom.y / 2);
                    }
                    else
                    {
                        //this else theoretically never append but it's here in a case of bad behavior.
                        pointBaseNode = Vector2.zero;
                        pointSecondNode = Vector2.zero;
                    }
                    
                    var newDoorPos = CreateNewDoor(baseNode, secondNode, pointBaseNode, pointSecondNode);
                    
                    //after the door is placed (center of the GameObject) we add all information the node need for function
                    MapNodeLink newLinkNode = newLink.AddComponent<MapNodeLink>();
                    
                    newLinkNode.transform.position = newDoorPos;
                    newLinkNode.firstMapNode = baseNode;
                    newLinkNode.secondMapNode = secondNode;
                    _map.maplinks.Add(newLinkNode);
                    
                    baseNode.linkToOtherNode.Add(newLinkNode);
                    secondNode.linkToOtherNode.Add(newLinkNode);
                }
    }
//in this method we create a pos for the door of the link, we see if it possible to draw it between the two point.
//it cover all spec of a door, in major part it's only go in the exact middle of the two collision point.
    private Vector3 CreateNewDoor(MapNode baseNode,MapNode secondNode,Vector2 pointBaseNode,Vector2 pointSecondNode)
    {
        if (pointBaseNode.x != pointSecondNode.x)
        {
            if (math.abs(pointBaseNode.x - pointSecondNode.x) >= baseNode.sizeRoom.x)
            {
                if (pointBaseNode.x < pointSecondNode.x)
                {
                    return new Vector3(pointBaseNode.x + baseNode.sizeRoom.x / 2, pointBaseNode.y);  
                }
                return new Vector3(pointBaseNode.x - baseNode.sizeRoom.x / 2, pointBaseNode.y);
            }
            if (math.abs(pointBaseNode.x - pointSecondNode.x) >= secondNode.sizeRoom.x)
            {
                if (pointSecondNode.x < pointBaseNode.x)
                {
                    return new Vector3(pointSecondNode.x + secondNode.sizeRoom.x / 2, pointSecondNode.y);  
                }
                return new Vector3(pointSecondNode.x - secondNode.sizeRoom.x / 2, pointSecondNode.y);
            }
        }
        
        if (pointBaseNode.y != pointSecondNode.y)
        {
            if (math.abs(pointBaseNode.y - pointSecondNode.y) >= baseNode.sizeRoom.y)
            {
                if (pointBaseNode.y < pointSecondNode.y)
                {
                    return new Vector3( pointBaseNode.x,pointBaseNode.y + baseNode.sizeRoom.y / 2);  
                }
                return new Vector3( pointBaseNode.x,pointBaseNode.y - baseNode.sizeRoom.y / 2);
            }
            if (math.abs(pointBaseNode.y - pointSecondNode.y) >= secondNode.sizeRoom.y)
            {
                if (pointSecondNode.y < pointBaseNode.y)
                {
                    return new Vector3(pointSecondNode.x,pointSecondNode.y + secondNode.sizeRoom.y / 2);  
                }
                return new Vector3(pointSecondNode.x,pointSecondNode.y - secondNode.sizeRoom.y / 2);
            }
        }

        
        return new Vector3((pointBaseNode.x + pointSecondNode.x) / 2, (pointBaseNode.y + pointSecondNode.y) / 2);
    }
}
