using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    PostGameData previous_game_data;

    public enum Cars { RED, GREEN, BLUE, WHITE}

    public enum State {MENU, IN_GAME, POST_GAME}
    public State game_state = State.MENU;
    public Cars current_car;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

   
}
