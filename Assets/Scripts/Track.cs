using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Track : MonoBehaviour
{
    public enum TrackType { PATH, ASCENDING}
    [SerializeField]
    public TrackType track_type;

    [Header("The positions of track nodes")]
    [SerializeField]    
    public Transform[] nodes;
    [Header("Which order the nodes come in")]
    public int[] path_node_order;
    [Header("Distance to \"reach\" nodes")]
    public int[] node_ranges;
    [Header("Does the track loop?")]
    public bool loop;
    [Header("Leave as 0 for infinite loop")]
    public int loop_count;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
