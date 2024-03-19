using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DriftMarkHandler : MonoBehaviour
{
    TrailRenderer[] trails;
    CarController player_car;

    private bool trail_emit = false;
    // Start is called before the first frame update
    void Start()
    {
        trails = GetComponentsInChildren<TrailRenderer>();
        player_car = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_car.is_drifting)
        {
            if (!trail_emit) 
            {
                foreach (var trail in trails) 
                {
                    trail.emitting = true;
                }
                trail_emit = true;
            }
        }
        else 
        {
            if (trail_emit)
            {
                foreach (var trail in trails)
                {
                    trail.emitting = false;
                }
                trail_emit = false;
            }
        }
    }
}
