using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Track;

public class MenuUIHandler : MonoBehaviour
{
    public enum SubMenus {NONE, SETTINGS, MAP_SELECT, CAR_SELECT, CREDITS, HIGHSCORES};
   
    
    private NetworkManager networkManager;
    private GameState game_state;
    private SubMenus current_sub_menu;
    private Maps current_map;

    public UnityEngine.UI.Button logout_button;
    public TextMeshProUGUI usernameTMP;
    public GameObject main_menu_panel;
    public GameObject map_select_panel;
    public GameObject car_select_panel;
    public GameObject credits_panel;
  
    private GameObject[] sub_panels;
    private GameObject lap_count_slider_obj;
    private UnityEngine.UI.Slider lap_count_slider;
    private TextMeshProUGUI lap_count_value;
    private bool username_set = false;
    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.FindAnyObjectByType<NetworkManager>();
        lap_count_slider_obj = GameObject.Find("LapCountSlider");
        lap_count_slider = lap_count_slider_obj.GetComponent<UnityEngine.UI.Slider>();
        lap_count_value = lap_count_slider_obj.transform.Find("Value").GetComponent<TextMeshProUGUI>();
        game_state = GameObject.Find("GameState").GetComponent<GameState>();
        game_state.game_state = GameState.State.MENU;

        current_sub_menu = SubMenus.NONE;
        current_map = Maps.NONE;

        sub_panels = new GameObject[] { map_select_panel, car_select_panel, credits_panel };
        CloseSubMenus();

      
    }

    // Update is called once per frame
    void Update()
    {
        lap_count_value.text = ((int)lap_count_slider.value).ToString();

        if (usernameTMP != null && !username_set)
        {
            if (networkManager.IsLoggedIn)
            {                
                usernameTMP.text = "User: " + networkManager.Username;
            }
            else
            {
                logout_button.gameObject.SetActive(false);
                usernameTMP.text = "User: Offline";
            }
            username_set = true;
        }
    }



    public void OnPlay() 
    {
        if (current_sub_menu == SubMenus.MAP_SELECT || current_sub_menu == SubMenus.CAR_SELECT) 
        {
            //if we are already in the play menu then return
            return;
        }
        if (current_sub_menu != SubMenus.NONE)
        {
            //if are in some other menu that isnt the play menu OR none
            CloseSubMenus();
        }

        map_select_panel.SetActive(true);
        current_sub_menu = SubMenus.MAP_SELECT;
    }
    public void OnSettings()
    {

    }
    public void OnCredits()
    {

    }
    public void OnQuit()
    {
        Application.Quit();
    }


    void CloseSubMenus() 
    {
        foreach (GameObject panel in sub_panels) 
        {
            if (panel != null) 
            {
                panel.SetActive(false);
            }
        }       
    }







    //-------- Map Select Functions----------
    public void OnCarteenaValley()
    {
        current_map = Maps.CARTEENA;
        game_state.current_map = Maps.CARTEENA;
        CloseSubMenus();
        car_select_panel.SetActive(true);
        
    }
    public void OnSandySlalom()
    {
        current_map = Maps.SANDY;
        game_state.current_map = Maps.SANDY;
        CloseSubMenus();
        car_select_panel.SetActive(true);

    }
    public void OnMarksMap()
    {
        current_map = Maps.MARKS;
        game_state.current_map = Maps.MARKS;
        CloseSubMenus();
        car_select_panel.SetActive(true);

    }



    //--------Car Select Functions--------

    public void OnGreenCar() 
    {
        SelectCar(GameState.Cars.GREEN);
    }
    public void OnWhiteCar()
    {
        SelectCar(GameState.Cars.WHITE);
    }
    public void OnRedCar()
    {
        SelectCar(GameState.Cars.RED);
    }
    public void OnBlueCar()
    {
        SelectCar(GameState.Cars.BLUE);
    }

    private void SelectCar(GameState.Cars car) 
    {     
        game_state.current_car = car;
        game_state.lap_count = (int)lap_count_slider.value;
        game_state.game_state = GameState.State.IN_GAME;

        CloseSubMenus();                
        LoadMap(current_map);
    }

    void LoadMap(Maps map) 
    {
        switch (map) 
        {
            case Maps.CARTEENA:
                SceneManager.LoadScene("Carteena Valley");
                break;
            case Maps.SANDY:
                SceneManager.LoadScene("Sandy Slalom");
                break;
            case Maps.MARKS:
                SceneManager.LoadScene("Marks Map");
                break;
        }
    }



}
