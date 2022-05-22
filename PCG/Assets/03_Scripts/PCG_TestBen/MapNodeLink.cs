using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeLink : MonoBehaviour
{
    public MapNode firstMapNode;
    public MapNode secondMapNode;

    //this represent if the root are already check this node, if false the root use it, if true the root use
    //only if the rootPathing is true-
    public bool alreadyCheck;
    //this is represent if the root take this path or not, if false it's not assigned to the root.
    public bool rootPathing;
    
    //this is the type of the door is in the link between the two room
    public PcgPopulate.LinkType doorType;

    MapNodeLink()
    {
        rootPathing = false;
    }
}
