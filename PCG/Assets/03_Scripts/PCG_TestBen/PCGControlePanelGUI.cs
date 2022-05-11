using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PCGControlePanel))]
public class PCGControlePanelGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PCGControlePanel myScript = (PCGControlePanel)target;
        
        if(GUILayout.Button("Generate room(map Node)"))
        {
            myScript.GenerateRoom();
        }
        
    }
}
