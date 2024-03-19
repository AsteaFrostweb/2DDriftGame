using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player_trans;
    private Vector3 origonal_offset;
    public bool offset = true;

    // Start is called before the first frame update
    void Start()
    {
        player_trans = GameObject.Find("Player").transform;
        if (offset)
        {
            origonal_offset = player_trans.position - transform.position;
        }
        else 
        {
            origonal_offset = Vector3.zero;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player_trans.position - origonal_offset;   
    }

  
}
