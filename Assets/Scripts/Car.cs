using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
   

    public float acceleration = 250f;
    public float max_speed = 35f;

    public float braking_strength = 0.5f;

    public float min_turn_angle = 0.1f;
    public float max_turn_angle = 12f;

    public float turn_acceleration = 250f;
    
    public float turn_speed_reset_rate = 1.5f;
    public float turn_speed_invert_rate = 2f;
    public float min_turn_speed = 0.1f;

    public float drift_threshold = 15f;

    public float min_turn_speed_speedThreshold = 1f;
    public float max_turn_speed_speedThreshold = 7.5f;

    public float defualt_tyre_grip = 1.75f;
    public float drift_grip_reduction = 0.75f;
    public float brake_grip_reduction = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
