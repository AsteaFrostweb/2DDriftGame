using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player_trans;
    [SerializeField]
    private Vector3 origonal_offset;
    public bool offset = true;

    private void Awake()
    {
        CarController controller = GetComponent<CarController>();
        if (controller != null) 
        {
            player_trans = controller.transform;
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }



    // Update is called once per frame
    void Update()
    {
        if (player_trans == null)
        {
            CarController c = GameObject.FindAnyObjectByType<CarController>();
            if (c != null) player_trans = c.transform;           
        }
        else 
        {
            transform.position = player_trans.position + origonal_offset;
        }
        
    }

  
}
