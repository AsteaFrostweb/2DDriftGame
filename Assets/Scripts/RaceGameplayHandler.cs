using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaceGameplayHandler : MonoBehaviour
{
    public struct PlayerRace
    {
        public int current_node;

    }
    public struct Racetracker
    {
        public Track track;
        public PlayerRace[] players;

    }
    public struct RaceData
    {
        public TimeSpan fastest_lap;
        public TimeSpan total_time;
    }
    private Racetracker race;
    

    // Start is called before the first frame update
    void Start()
    {
        race.track = GameObject.FindAnyObjectByType<Track>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
