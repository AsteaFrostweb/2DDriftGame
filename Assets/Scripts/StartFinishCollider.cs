using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinishCollider : MonoBehaviour
{
    public RaceGameplayHandler gameplayHandler;
    private void Start()
    {
        gameplayHandler = GameObject.FindAnyObjectByType<RaceGameplayHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debugging.Log("Enter");
        gameplayHandler.SetFinishline(collision.gameObject, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debugging.Log("Exit");
        gameplayHandler.SetFinishline(collision.gameObject, false);
    }
 
}
