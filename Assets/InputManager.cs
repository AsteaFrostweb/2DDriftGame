using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    public Car player_car;
    public GameState gs;



    public float horizontal_input = 0f;
    public float vertical_input = 0f;

    bool brake_pressed = false;

    

    // Start is called before the first frame update
    void Start()
    {
        player_car = GameObject.Find("Player").GetComponent<Car>();
        gs = GameObject.Find("GameState").GetComponent<GameState>();
    }

    private void Update()
    {
        switch (gs.game_state)
        {
            case GameState.State.MENU:
                break;
            case GameState.State.IN_GAME:
                HandleGameInput();
                break;
            case GameState.State.POST_GAME:
                break;
        }
    }

    private void FixedUpdate()
    {

        if (player_car.car_rb.velocity.magnitude <= player_car.max_speed)
        {
            player_car.car_rb.AddForce(player_car.transform.up * vertical_input * player_car.acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
            if (player_car.car_rb.velocity.magnitude > player_car.max_speed)
            {
                player_car.car_rb.velocity = player_car.car_rb.velocity.normalized * player_car.max_speed; //if velocxiity excees speed limit cap it at that speed limit
            }
        }

        if (horizontal_input != 0)
        {
            if (Mathf.Abs(player_car.steering_angle) <= player_car.max_steering_angle)
            {
                player_car.steering_angle -= player_car.steering_acceleration * player_car.max_steering_angle * horizontal_input * Time.fixedDeltaTime; //Intcrement steering angle by horinztal input

                if (Mathf.Abs(player_car.steering_angle) > player_car.max_steering_angle)
                {
                    if (player_car.steering_angle > 0)
                    {
                        player_car.steering_angle = player_car.max_steering_angle; //if steering angle is above max steering angle cap it 
                    }
                    else
                    {
                        player_car.steering_angle = -player_car.max_steering_angle; //if steering angle is below max steering angle cap it 
                    }
                }
            }
        }
        else 
        {
            player_car.DampedSteering(Time.fixedDeltaTime);
        }

        float forward_mag = Vector3.Project(player_car.car_rb.velocity, player_car.transform.up).magnitude;

        player_car.car_rb.angularVelocity += forward_mag * horizontal_input * Time.fixedDeltaTime;

        ResetInputs();
    }


    public void ResetInputs()
    {
        brake_pressed = false;
        horizontal_input = 0f;
        vertical_input = 0f;
    }

    public void HandleGameInput()
    {
        horizontal_input += Input.GetAxis("Horizontal") * Time.deltaTime;
        vertical_input += Input.GetAxis("Vertical") * Time.deltaTime;

        if (Input.GetButton("Jump"))
        {
            brake_pressed = true;
        }



    }
}
