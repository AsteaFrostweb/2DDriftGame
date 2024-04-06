using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    Vector3 forward_projection;
    Car car_info;
    
    public float current_speed = 0f;
    public float current_forward_speed = 0f;    
    public float turn_angle = 0f;
    public float current_drift_angle = 0f;
    public float current_step = 0f;

    public bool control_locked = true;
    public bool is_drifting;
    public bool is_breaking;
    public bool is_moving_backwards;
    public bool is_touching_edge;

    private Rigidbody2D rb;

    float rotation = 0f;
    float horizontalInput = 0f;
    float verticalInput = 0f;
    void Start()
    {
        is_drifting = false;
        is_breaking = false;
        is_moving_backwards = false;
        is_touching_edge = false;
        control_locked = true;


        rb = GetComponent<Rigidbody2D>();
        car_info = gameObject.GetComponent<Car>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        
        //assigning values of input variables
        GetInputs();

        //Gets neccesary information to calculate movement rotation etc.
        GetMovementInfo();

        //Handle's the rotation of the car.
        HandleRotation();

        //Handle's assigning of horizntal input
        HandleHorizontalInput();       

        //Clamps speed at cars max speed
        ClampSpeed();

        //Clamps turn speed at cars max turn speed
        ClampTurnSpeed();
    }

    void GetMovementInfo() 
    {
        forward_projection = Vector3.Project(rb.velocity, transform.up);

        current_drift_angle = Vector3.Angle(rb.velocity, transform.up);
        if ((180 - current_drift_angle) < current_drift_angle) 
        {
            current_drift_angle = 180 - current_drift_angle;
        }

        if (!is_moving_backwards)
        {
            current_forward_speed = forward_projection.magnitude; //if where moving forward then the current_forward_soeed is posititive +
        }
        else
        {
            current_forward_speed = -forward_projection.magnitude;//if where moving backwards then the current_forward_soeed is negative -
        }
    }
    void HandleRotation() 
    {
        //Debugging.Log("HandleRoatitom");
        float speed_delta = 0f;
        float max_speed_delta = 1f;
        float rotation_delta = 0f;
        float speed_ratio = 0f;

        
        speed_delta = Mathf.Abs(current_forward_speed) - car_info.min_turn_speed_speedThreshold;
        max_speed_delta = car_info.max_turn_speed_speedThreshold - car_info.min_turn_speed_speedThreshold;

        speed_ratio = speed_delta / max_speed_delta;

        if (turn_angle > car_info.min_turn_speed)
        {
            rotation_delta = Mathf.Lerp(car_info.min_turn_speed, turn_angle, (speed_ratio));
        }
        else if (turn_angle < car_info.min_turn_speed)
        {
            rotation_delta = -Mathf.Lerp(car_info.min_turn_speed, Mathf.Abs(turn_angle), (speed_ratio));
        }

        if (!is_moving_backwards)
        {
            if (current_forward_speed > car_info.min_turn_speed_speedThreshold)
            {
                rotation += rotation_delta * Time.deltaTime;
                //Debugging.Log("Rotation Increment by:  " + rotation_delta);
            }
        }
        else
        {
            if (current_forward_speed < -car_info.min_turn_speed_speedThreshold)
            {
                rotation += -rotation_delta * Time.deltaTime;
                //Debugging.Log("Rotation Increment by:  " + rotation_delta);
            }
        }
    }
    void HandleHorizontalInput() 
    {
        if (horizontalInput == 0) //If there is no horizontal (AD) input
        {
            DampenTurnSpeed(Time.deltaTime);
        }
        else //If there IS horizontal input            
        {
            float angle_delta = -horizontalInput * car_info.turn_acceleration;

            if (angle_delta > 0)
            {
                if (turn_angle > 0) //If the angle_deta is POSITIVE AND the turn_angle is POSITIVE
                {
                    turn_angle += angle_delta;
                }
                else  //If the angle_deta is POSITIVE AND the turn_angle is NEGATIVE
                {
                    turn_angle += angle_delta * car_info.turn_speed_invert_rate; //If we are steering in the opposite direction to the direction we are currently steering apply an extra scalar
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
                    turn_angle += angle_delta * car_info.turn_speed_invert_rate;
                }
            }

            ClampMinTurnAngle(); //if minimum turn angle is below threshold, set it to 0
        }
    }
    void GetInputs() 
    {
        if (control_locked) { return; }
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        verticalInput += Input.GetAxis("Vertical") * Time.deltaTime;

        is_drifting = IsDrifting();
        is_moving_backwards = IsMovingBackward();
    }
    void ClampMinTurnAngle() 
    {
        if (Mathf.Abs(turn_angle) < car_info.min_turn_angle)
        {
            if (horizontalInput < 0)
            {
                turn_angle = car_info.min_turn_angle;
            }
            else
            {
                turn_angle = - car_info.min_turn_angle;
            }
        }
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

        if ((angle >= car_info.drift_threshold) && !is_touching_edge)
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
        float grip = car_info.defualt_tyre_grip;
        if (is_drifting) 
        {
            grip -= car_info.drift_grip_reduction;
            grip -= car_info.drift_grip_reduction;
        }

        return grip;
    }

    void DampenTurnSpeed(float time) 
    {
        float turn_delta = car_info.turn_acceleration * time * car_info.turn_speed_reset_rate;
        if (turn_angle > 0)
        {
            turn_angle -= turn_delta;
        }
        else if (turn_angle < 0)
        {
            turn_angle += turn_delta;
        }

        if (Mathf.Abs(turn_angle) < car_info.min_turn_angle + turn_delta)
        {
            turn_angle = 0f;
        }
    }

    void ClampTurnSpeed() 
    {
        if (Mathf.Abs(turn_angle) > car_info.max_turn_angle) 
        {
            if (turn_angle > 0)
            {
                turn_angle = car_info.max_turn_angle;
            }
            else 
            {
                turn_angle = -car_info.max_turn_angle;
            }
        }
    }
    void ClampSpeed() 
    {
        current_speed = rb.velocity.magnitude;
        if (current_speed > car_info.max_speed)
        {
            rb.velocity = rb.velocity.normalized * car_info.max_speed;
        }
    }
    void ApplyFriction(float time) 
    {
        Vector2 forward_vector = transform.up;

        float front_angle = Vector2.Angle(rb.velocity, forward_vector);// gets the angle between our forward movement and our current velocity
        float back_angle = Vector2.Angle(rb.velocity, -forward_vector);
        //Debugging.Log("Front Angle: " + front_angle);
        //Debugging.Log("Back Angle: " + back_angle);

        float angle = (front_angle <= back_angle) ? front_angle : back_angle;

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
        //Making car's velocity move towards the forward direction of the car dependant on the tyres grip
        ApplyFriction(Time.fixedDeltaTime);

        //Incrementing the velocity by the input * scalars and vectors in the forward direction
        rb.velocity += verticalInput * car_info.acceleration * (Vector2)transform.up * Time.fixedDeltaTime;
        


        rb.angularVelocity = 0f;
        rb.rotation += (rotation * Time.fixedDeltaTime);
        //transform.rotation = Quaternion.Euler(0F, 0F, physics_body.current_rotation);



        rotation = 0f;       
        verticalInput = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        is_touching_edge = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        is_touching_edge = false;
    }
}
