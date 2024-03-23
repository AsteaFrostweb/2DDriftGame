using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSetupHandler : MonoBehaviour
{
    [SerializeField]
    GameObject red_car_prefab;
    [SerializeField]
    GameObject blue_car_prefab;
    [SerializeField]
    GameObject green_car_prefab;
    [SerializeField]
    GameObject white_car_prefab;

    GameState game_state;
    
    [SerializeField]
    Transform Spawn_position;
    public bool player_spawned;

    // Start is called before the first frame update
    void Start()
    {
        game_state = GameObject.Find("GameState").GetComponent<GameState>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (game_state == null) 
        {
            game_state = GameObject.Find("GameState").GetComponent<GameState>();
            
        }
        if (!player_spawned)
        {            
            try
            {
                SpawnCar();
               player_spawned = true;
            }
            catch
            {
                Debug.Log("Error spawning player");
            }
        }
    }


    void SpawnCar() 
    {
        switch (game_state.current_car) 
        {
            case GameState.Cars.GREEN:
                GameObject.Instantiate(green_car_prefab, Spawn_position.position, Spawn_position.rotation);
                break;
            case GameState.Cars.RED:
                GameObject.Instantiate(red_car_prefab, Spawn_position.position, Spawn_position.rotation);
                break;
            case GameState.Cars.BLUE:
                GameObject.Instantiate(blue_car_prefab, Spawn_position.position, Spawn_position.rotation);
                break;
            case GameState.Cars.WHITE:
                GameObject.Instantiate(white_car_prefab, Spawn_position.position, Spawn_position.rotation);
                break;
        }
    }
}
