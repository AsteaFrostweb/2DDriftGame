using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class CarSFXHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource EngineAudio;
    [SerializeField]
    private AudioSource DriftAudio;
    [SerializeField]
    private AudioSource BrakeAudio;
    [SerializeField]
    private AudioSource ComboAudio;

    //Car related sounds
    [SerializeField]
    private AudioClip combo_mult_increase;
    [SerializeField]
    private AudioClip combo_end_sucess;
    [SerializeField]
    private AudioClip combo_end_fail;   

    //Car Specific sounds
    [SerializeField]
    private AudioClip engine_start;
    [SerializeField]
    private AudioClip engine_loop;
    [SerializeField]
    private AudioClip brake;
    [SerializeField]
    private AudioClip drift_loop;


    private CarController controller;
    private DriftScoreHandler driftScoreHandler;

    //Change trackers
    private ChangeTracker<bool> engine_loop_tracker;
    private ChangeTracker<bool> drift_loop_tracker;
    private ChangeTracker<bool> combo_change_tracker;
    private ChangeTracker<bool> braking_tracker;
    private ChangeTracker<int> combo_mult_tracker;
  
    // Start is called before the first frame update
    void Start()
    {
      
        controller = GameObject.Find("Player1").GetComponent<CarController>();
        

        StartEngineLoop();

        //Initalize change trackers
       

        combo_change_tracker = new ChangeTracker<bool>(() => driftScoreHandler.in_combo);
        braking_tracker = new ChangeTracker<bool>(() => controller.is_breaking);
        combo_mult_tracker = new ChangeTracker<int>(() => driftScoreHandler.current_combo.multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            controller = GameObject.Find("Player1").GetComponent<CarController>();
            if (controller == null) return; //If cant find car contoller component then return
        }
        else if(driftScoreHandler == null)
        {
            driftScoreHandler = controller.gameObject.GetComponent<DriftScoreHandler>();
            if (driftScoreHandler == null) return;  //If cant find the players driftscorehandler
        }      

        if (engine_loop_tracker.Update()) //if the player has started acceleratings
        {
            SetEngineLoop(engine_loop_tracker.state);
        }

        if (drift_loop_tracker.Update()) 
        {
            SetDriftLoop(drift_loop_tracker.state);
        }

        if (combo_change_tracker.Update()) 
        {
            if (!combo_change_tracker.state) //runs first frame we leave combo
            {
                if (driftScoreHandler.crashed)  
                {
                    PlayComboFailSound();//If we crashed on the drfit end
                }
                else 
                {
                    PlayComboSuccessSound();//If we didnt crash 
                }
            }              
        }

        if(combo_mult_tracker.Update()) 
        {
            if (driftScoreHandler.current_combo.multiplier >= 2) //If the combo multiplier has changed and the combo multiplier is higher than 1
            {
                PlayComboMultSound();
            }
        }

        if (braking_tracker.Update()) 
        {
            SetBrakeSound(braking_tracker.state);
        }
        
      
    }


    public void SetBrakeSound(bool state) 
    {
        if(state) 
        { 
            StartBrakeSound(); 
        }else
        { 
            StopBrakeSound(); 
        }
    }
    private void StartBrakeSound()
    {
        BrakeAudio.loop = false;
        BrakeAudio.clip = brake;
        BrakeAudio.Play();
    }
    private void StopBrakeSound()
    {
        BrakeAudio.loop = false;      
        BrakeAudio.Stop();
    }

    //Combo sound functions
    private void PlayComboSuccessSound() 
    {
        ComboAudio.loop = false;
        ComboAudio.clip = combo_end_sucess;
        ComboAudio.Play();
    }
    private void PlayComboFailSound()
    {
        ComboAudio.loop = false;
        ComboAudio.clip = combo_end_fail;
        ComboAudio.Play();
    }
    private void PlayComboMultSound()
    {
        ComboAudio.loop = false;
        ComboAudio.clip = combo_mult_increase;
        ComboAudio.Play();
    }

    //Drift loop functions
    public void SetDriftLoop(bool state)
    {
        if (state)
        {
            StartDriftLoop();
        }
        else
        {
            StopDriftLoop();
        }
    }
    private void StartDriftLoop() 
    {
        DriftAudio.loop = true;
        DriftAudio.clip = drift_loop;
        DriftAudio.Play();
    }
    private void StopDriftLoop()
    {
        DriftAudio.loop = false;       
        DriftAudio.Play();
    }


    //Engine loop functions
    public void SetEngineLoop(bool state) 
    {
        if (state)
        {
            StartEngineLoop();
        }
        else 
        {
            StopEngineLoop();
        }
    }
    private void StartEngineLoop() 
    {
        EngineAudio.loop = true;
        EngineAudio.clip = engine_start;
        EngineAudio.Play();
    }  
    private void StopEngineLoop() 
    {
        EngineAudio.loop = false;
        EngineAudio.Stop();
    }
}
