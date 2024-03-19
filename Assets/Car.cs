using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int ID = 0;
    public bool is_drifting = false;
    public bool is_braking = false;

   
    public float current_speed = 0f;
    public float max_speed = 25f;

    public float acceleration = 10f;
    public float drag_ratio = 100f; //smaller the less drag the car has


    public float min_turn_angle = 10f;
    public float max_turn_angle = 80f;

    public float current_drift_angle = 0f;
    public float steering_angle = 0f;
    public float min_steering_angle = 0.05f;
    public float steering_acceleration = 3f;
    public float steering_angle_reset_rate = 2f;
    public float max_steering_angle = 50f;
    public float speed_steering_cap = 10f; //at 10 speed the steering will cap its rotation speed / torque applicaiton
    

    public float default_tyre_grip = 2f;
    public float tyre_grip = 0.9f;
    public float drift_threshold = 0.2f;
    public float drift_grip_reduction = 0.8f;

    public float drift_grip_multiplier = 1f;

    public float brake_speed = 100f;
    public float brake_grip_reduction = 0.7f;
    bool braking_reduction_applied = false;


    public Rigidbody2D car_rb;

    public void UpdateSpeed() 
    {
        current_speed = car_rb.velocity.magnitude;
        
    }

    public void CheckDrifting()
    {
        if (IsDrifting())
        {
            if (!is_drifting)  //runs first frame the car is drifting
            {
                drift_grip_multiplier -= drift_grip_reduction;
            }
            is_drifting = true;
        }
        else
        {
            if (is_drifting)
            {
                drift_grip_multiplier += drift_grip_reduction;
            }
            is_drifting = false;
        }
    }

    public bool IsDrifting()
    {
        if (car_rb.velocity.magnitude > 0)
        {
            Vector2 velocity_direction = car_rb.velocity.normalized;
            current_drift_angle = Vector2.Angle(velocity_direction, transform.up) % 180;
        }
        else
        {
            current_drift_angle = 0f;
        }
        return (current_drift_angle > drift_threshold);
    }

    public void BoundSteeringAngle()
    {
        //Capping cars stearing angle     
        if (steering_angle > max_steering_angle)
        {
            steering_angle = max_steering_angle;
        }
        else if (steering_angle < -max_steering_angle)
        {
            steering_angle = -max_steering_angle;
        }
    }
    public void DampedSteering(float time)
    {
        if (Mathf.Abs(steering_angle) < min_steering_angle)
        {
            steering_angle = 0f;
            return;
        }

        if (steering_angle > 0)
        {
            steering_angle -= steering_angle_reset_rate * time;
        }
        else 
        {
            steering_angle += steering_angle_reset_rate * time;
        }
    }
    public void ApplyFriction(float time)
    {
        Vector2 forward_vector = transform.up;

        float angle = Vector2.Angle(car_rb.velocity, forward_vector);// gets the angle between our forward movement and our current velocity


        float step = angle * tyre_grip * drift_grip_multiplier * time * (1 + current_speed);

        float dot_product = Vector2.Dot(car_rb.velocity, forward_vector); //get a float value to tell us which direction the car is facing
        if (dot_product > 0)
        {
            car_rb.velocity = Vector2.MoveTowards(car_rb.velocity, forward_vector * car_rb.velocity.magnitude, step);
        }
        else if (dot_product < 0)
        {
            car_rb.velocity = Vector2.MoveTowards(car_rb.velocity, -forward_vector * car_rb.velocity.magnitude, step);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        car_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        UpdateSpeed();
        CheckDrifting();
        BoundSteeringAngle();
        ApplyFriction(Time.deltaTime);
    }
}
