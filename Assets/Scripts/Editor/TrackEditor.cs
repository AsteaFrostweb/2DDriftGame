using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Track))]
public class TrackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Track myTarget = (Track)target;
      
        myTarget.nodes = new Transform[myTarget.nodes.Length];
        for (int i = 0; i < myTarget.nodes.Length; i++) 
        {
            myTarget.nodes[i] = GameObject.Find("Node" + i).transform;
        }

        if (myTarget.track_type == Track.TrackType.ASCENDING) 
        {
            myTarget.path_node_order = new int[myTarget.nodes.Length];
            for (int i = 0; i < myTarget.nodes.Length; i++) 
            {
                myTarget.path_node_order[i] = i;
            }          
        }
    }
}