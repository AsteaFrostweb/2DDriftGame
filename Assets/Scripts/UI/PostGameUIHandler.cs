using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct PostGameData
{
    public Track.Maps map;    
    public RaceGameplayHandler.RaceData race_data;
}



public class PostGameUIHandler : MonoBehaviour
{
    private GameState game_state;
    private PostGameData pg_data;




    [SerializeField]
    public Image car_image;
    [SerializeField]
    public TextMeshProUGUI total_score_text;
    private string total_score_text_base = "";
    [SerializeField]
    public TextMeshProUGUI total_time_text;
    private string total_time_text_base = "";
    [SerializeField]
    public TextMeshProUGUI best_combo_score_text;
    private string best_combo_text_base = "";
    [SerializeField]
    public TextMeshProUGUI fastest_lap_text;
    private string fastest_lap_text_base = "";
    [SerializeField]
    public TextMeshProUGUI longest_combo_text;
    private string longest_combo_text_base = "";

    // Start is called before the first frame update
    void Start()
    {
        game_state = GameObject.FindAnyObjectByType<GameState>();
        pg_data = game_state.post_game_data;

        total_score_text_base = total_score_text.text;
        total_time_text_base = total_time_text.text;
        best_combo_text_base = best_combo_score_text.text;
        fastest_lap_text_base = fastest_lap_text.text;
        longest_combo_text_base = longest_combo_text.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTextBoxes() 
    {
        RaceGameplayHandler.PlayerRaceData pr_data = pg_data.race_data.player_race_data[0]; //get player1's data
        DriftScoreHandler.DriftData dr_data = pg_data.race_data.player_race_data[0].drift_data;

        total_score_text.text = total_score_text_base + dr_data.total_score;
        total_time_text.text = total_time_text_base + pr_data.total_time;
        best_combo_score_text.text = best_combo_text_base + dr_data.best_combo_score;
        fastest_lap_text.text = fastest_lap_text_base + pr_data.fastest_lap.ToString();
        longest_combo_text.text = longest_combo_text_base + dr_data.longest_combo.ToString();

    }

    public void OnContinue() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
