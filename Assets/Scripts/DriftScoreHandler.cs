using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;


public struct Drift 
{    
    public float score;

    public Drift(float _score) 
    {        
        this.score = _score;
    }
}

public struct DriftCombo
{
    public bool crashed;
    public DateTime start_time;
    public DateTime end_time;
    public int multiplier;
    public List<Drift> drifts;

    public DriftCombo(DateTime s, DateTime e) 
    {
        crashed = false;
        start_time = s;
        end_time = e;
        multiplier = 1;
        drifts = new List<Drift>();
    }
    public float TotalScore() 
    {
        float f = 0f;
        foreach (Drift drift in drifts) 
        {
            
            f += drift.score;
        }
        return f;
    }
    public void AddDrift(Drift d) 
    {
        drifts.Add(d);       
    }
}

public class DriftScoreHandler : MonoBehaviour
{
    CarController player_car;

    [Header("Outputs")]
    public bool crashed = false;
    public bool in_combo = false;
    public float current_total_drift_score = 0f;
    public float current_combo_multiplier = 0f;
    public float current_drift_score = 0f;

    [Header("Inputs")]
    public float drift_score_multiplier = 124f;
    public float crash_no_drift_time = 2f;
    public float drift_multiplier_time = 1f;

    private DateTime previous_drift_end;
    private DateTime previous_crash_time;
    private bool was_drifting = false;
   

    private List<DriftCombo> all_combos;

    public DriftCombo current_combo;
    private Drift current_drift;

    // Start is called before the first frame update
    void Start()
    {
        all_combos = new List<DriftCombo>();
        player_car = GetComponent<CarController>();
        previous_drift_end = DateTime.Now.AddSeconds(-drift_multiplier_time);
        previous_crash_time = DateTime.Now.AddSeconds(crash_no_drift_time);
    }

    // Update is called once per frame
    void Update()
    {
        if (crashed) 
        {
            if (DateTime.Now.Subtract(previous_crash_time).Seconds >= crash_no_drift_time)
            {
                crashed = false; 
                //if its been "crash_no_drift_time" since we last crashed in a combo then crasehd = fasle;
            }
        }
       


        if (player_car.is_drifting)
        {
            if (!was_drifting)
            {
                //Runs on the first frame we ARE drifting
                if (in_combo) //if we are in a combo
                {
                    current_combo.multiplier++;   
                }
                else if(!crashed)
                {
                    current_combo = new DriftCombo(DateTime.Now, DateTime.Now); //start new combo
                    in_combo = true;
                }

                current_drift = new Drift(0f);
                was_drifting = true;
                return;
            }
            //IF the player is drifting

            current_combo_multiplier = current_combo.multiplier; //asiging inspector output variables
            current_drift_score = current_drift.score;

            float frame_score = drift_score_multiplier * current_combo.multiplier * (player_car.current_drift_angle / 10) * Time.deltaTime;           
            current_drift.score += frame_score;

        }
        else  //IF PLAYER ISNT DRIFTING
        {
            if (was_drifting) 
            {
                Debug.Log("Eng of drift");
                //Runs on the first frame we stop drifting 
                previous_drift_end = DateTime.Now;

                current_combo.AddDrift(current_drift);
                current_drift = new Drift(0f);              
                
                was_drifting = false;
            }
            if (in_combo)
            {
                if (player_car.is_touching_edge) 
                {
                    previous_crash_time = DateTime.Now;
                    current_combo.crashed = true;
                    in_combo = false;
                    crashed = true;                    
                    all_combos.Add(current_combo);
                    Debug.Log("Crashed with combo score of: " + current_combo.TotalScore());
                }

                if (DateTime.Now.Subtract(previous_drift_end).Seconds >= drift_multiplier_time)
                {
                    current_combo.end_time = DateTime.Now;
                    //Debug.Log(current_combo.TotalScore());
                    current_total_drift_score += current_combo.TotalScore();
                    all_combos.Add(current_combo);
                    in_combo = false;
                }
            }

            //if the player isnt drifting
        }
    }
}
