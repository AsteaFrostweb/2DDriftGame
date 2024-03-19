using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 10f;
    public float current_speed = 0f;
    public float current_forward_speed = 0f;
    public float max_speed = 50f;


    public float min_turn_angle = 10f;
    

    public float turn_acceleration = 10f;
    public float turn_angle = 0f;
    public float max_turn_angle = 50f;
    public float turn_speed_reset_rate = 3f;
    public float turn_speed_invert_rate = 4f;
    public float min_turn_speed = 0.5f;

    public float current_drift_angle = 0f;
    public float drift_threshold = 15f;

    public float min_turn_speed_speedThreshold = 1f;
    public float max_turn_speed_speedThreshold = 1f;
    
    public float defualt_tyre_grip = 0.8f;
    public float drift_grip_reduction = 0.4f;


    public bool is_drifting;
    public bool is_breaking;
    public bool is_moving_backwards;

    private Rigidbody2D rb;

    float rotation = 0f;
    float horizontalInput = 0f;
    float verticalInput = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        horizontalInput = Input.GetAxis("Horizontal") *Time.deltaTime;
        verticalInput += Input.GetAxis("Vertical") *Time.deltaTime;

        is_drifting = IsDrifting();
        is_moving_backwards = IsMovingBackward();


        Vector3 projeciton = Vector3.Project(rb.velocity, transform.up);

        if (!is_moving_backwards)
        {
            current_forward_speed = projeciton.magnitude; //if where moving forward then the current_forward_soeed is posititive +
        }
        else 
        {
            current_forward_speed = -projeciton.magnitude;//if where moving backwards then the current_forward_soeed is negative -
        }
                

        float speed_delta = 0f;
        float max_speed_delta = 1f;

        speed_delta = current_forward_speed - min_turn_speed_speedThreshold;
        max_speed_delta = max_turn_speed_speedThreshold - min_turn_speed_speedThreshold;

        if (!is_moving_backwards)
        {                       
            if (current_forward_speed > min_turn_speed_speedThreshold)
            {
               
                rotation = Mathf.Lerp(min_turn_speed, turn_angle, (speed_delta / max_speed_delta)); //set the rotation this frame to be the speed me are moving "forward" * the steering angle
            }
        }
        else
        {            
            if (Mathf.Abs(current_forward_speed) > min_turn_speed_speedThreshold)
            {
                
                rotation = Mathf.Lerp(-min_turn_speed, -turn_angle, (speed_delta / max_speed_delta)); //set the rotation this frame to be the speed me are moving "forward" * the steering angle
            }
        }




        if (horizontalInput == 0)
        {
            DampenTurnSpeed(Time.deltaTime);
        }else             
        {
            float angle_delta = -horizontalInput * turn_acceleration;
            if (angle_delta != 0f)
            {
                if (angle_delta > 0)
                {
                    if (turn_angle > 0) //If the angle_deta is POSITIVE AND the turn_angle is POSITIVE
                    {
                        turn_angle += angle_delta;
                    }
                    else  //If the angle_deta is POSITIVE AND the turn_angle is NEGATIVE
                    {
                        turn_angle += angle_delta * turn_speed_invert_rate; //If we are steering in the opposite direction to the direction we are currently steering apply an extra scalar
                    }
                }
                else 
                {
                    if (turn_angle < 0) //If the angle_deta is NEGATIVE AND the turn_angle is NEGATIVE
                    {
                        turn_angle += angle_delta;
                    }
                    else //If the angle_deta is NEGATIVE AND the turn_angle is POSTIVE
                    {
                        turn_angle += angle_delta * turn_speed_invert_rate;
                    }
                }
            }

           

            if (Mathf.Abs(turn_angle) < min_turn_angle)
            {
                if (horizontalInput < 0)
                {
                    turn_angle = min_turn_angle;
                }
                else
                {
                    turn_angle = -min_turn_angle;
                }
            }
        }


   

















        
        ClampSpeed();
        ClampTurnSpeed();
    }


    public bool IsMovingBackward() 
    {
        float dot_product = Vector2.Dot(rb.velocity, transform.up); //get a float value to tell us which direction the       
        if (dot_product >= 0f)
        {
            return false;
        }
        
        return true;
        
    }
    public bool IsDrifting() 
    {
        float angle = Vector3.Angle(rb.velocity, transform.up);
        if ((180 - angle) < angle) 
        {
            angle = 180 - angle;
        }

        if (angle >= drift_threshold)
        {
            return true;
        }
        else 
        {
            return false;
        }

    }

    public float GetTyreGrip() 
    {
        float grip = defualt_tyre_grip;
        if (is_drifting) 
        {
            grip -= drift_grip_reduction;
        }

        return grip;
    }

    void DampenTurnSpeed(float time) 
    {
        float turn_delta = turn_acceleration * time * turn_speed_reset_rate;
        if (turn_angle > 0)
        {
            turn_angle -= turn_delta;
        }
        else if (turn_angle < 0)
        {
            turn_angle += turn_delta;
        }

        if (Mathf.Abs(turn_angle) < min_turn_angle)
        {
            turn_angle = 0f;
        }
    }

    void ClampTurnSpeed() 
    {
        if (Mathf.Abs(turn_angle) > max_turn_angle) 
        {
            if (turn_angle > 0)
            {
                turn_angle = max_turn_angle;
            }
            else 
            {
                turn_angle = -max_turn_angle;
            }
        }
    }
    void ClampSpeed() 
    {
        current_speed = rb.velocity.magnitude;
        if (current_speed > max_speed)
        {
            rb.velocity = rb.velocity.normalized * max_speed;
        }
    }
    void ApplyFriction(float time) 
    {
        Vector2 forward_vector = transform.up;

        float angle = Vector2.Angle(rb.velocity, forward_vector);// gets the angle between our forward movement and our current velocity


        float step = angle * GetTyreGrip() * time;

        float dot_product = Vector2.Dot(rb.velocity, forward_vector); //get a float value to tell us which direction the car is facing
        if (dot_product > 0)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, forward_vector * rb.velocity.magnitude, step);
        }
        else if (dot_product < 0)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, -forward_vector * rb.velocity.magnitude, step);
        }
    }

    private void FixedUpdate()
    {
        ApplyFriction(Time.fixedDeltaTime);

        rb.velocity += verticalInput * acceleration * (Vector2)transform.up * Time.fixedDeltaTime;
        


        rb.angularVelocity = 0f;
        rb.rotation += (rotation * Time.fixedDeltaTime);
        //transform.rotation = Quaternion.Euler(0F, 0F, physics_body.current_rotation);


        rotation = 0f;
       
        verticalInput = 0f;
    }
}
