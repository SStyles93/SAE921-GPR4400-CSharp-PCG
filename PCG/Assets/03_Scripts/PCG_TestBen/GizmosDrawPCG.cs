using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawPCG : MonoBehaviour
{
    [SerializeField] private MapScript mapScript;

    public void AddMapToGizmos(MapScript map)
    {
        mapScript = map;
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        foreach (var roomNode in mapScript.mapNodes)
        {
            Gizmos.DrawWireCube(roomNode.transform.position,new Vector3(roomNode.sizeRoom.x,roomNode.sizeRoom.y));
        }
        
        Gizmos.color = Color.green;
        foreach (var linkNode in mapScript.maplinks)
        {
            Gizmos.DrawLine(linkNode.firstMapNode.transform.position,linkNode.secondMapNode.transform.position);
        }
        
        foreach (var linkNode in mapScript.maplinks)
        {
            if (linkNode.rootPathing)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(linkNode.transform.position, linkNode.firstMapNode.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(linkNode.transform.position, linkNode.secondMapNode.transform.position);
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(linkNode.transform.position, new Vector3(1, 1));
            }
        }
    }
}
