# Tenacious Drift 2D

Welcome to Tenacious Drift 2D, a project to continue growing skills in game development; Focusing on intergration with and creation of online services.<br>

## How to Run

---

## Why I Created This Project:

Tenacious Drift 2D and 3D have been a learning experience, improving my game development skills:

- **Game Design** Design of systems, art, map creation.
- **UI Development**: Crafting user-friendly interfaces.
- **Networking with ASP.NET Core**: Learning about ASP.net core 8 MVC.
- **Version Control**: Managing the project with Git and GitHub.


## Gameplay Overview:

- **Login Scene**: Connect to high scores or play in offline mode.
- **Menu Scene**: Choose maps, cars, settings, etc.
- **Race Scene**: Race around the map trying to get a high drift-score, lap-time, or combo time.
- **Post-Game Scene**: Display and update highscores on the current map.


## Feature Overview:

### - Developer Console - 

The developer/debugging console is enabled with the **`** key.  It allows debugging and access to a limited selection of commands.

#### Commands
  - "show-ip"  :  Displays the current server IP assigned in the NetworkManager
  - "set-ip [ip]"  :  Sets the curerent server IP. Example: "set-ip localhost"
  - "set-port [port]"  : Sets the curerent server Port. Example: "set-port 25565"

### - User accounts and Highscores - 

The user accounts and high scores are handles by the external [HighscoresApplication](https://github.com/AsteaFrostweb/2DDriftGameHighscores/tree/main). 
This allows the game to poll the server upon race completion to determine whether to update the highscores for that particular person ont that map.
The highscores contains different aspects like: Longest Combo Time, Longest Combo Distance, Best Combo Score, Fastest Lap which can each be updated individually.

### - Main Menu -

---

## Credits & Software:

 - **Music/Sound**: All music was created by Mark Lee | [Youtube](https://www.youtube.com/@Markjameslee) | [SoundCloud](https://soundcloud.com/charkmomiak) |
 - **Engine**: Unity - Version: 2022.3.20f1
 - **IDE**: Visual Studio 2022 

---

## Acknowledging Areas for Improvement:

- **Coding Practices**: Consistency of style, proper encapsulation, Rhobustness.
- **Game Design**: Scope managment, Consistency of vision
- **Version Control**: Commiting more frequently, Making new branches for changes. 


## If I Continued:

I will now be moving onto new projects.<br>
These are some thigs that I would add if I were to continue with this project:

- **Add Info screen to explain how to play**
- **Add more verbose/usefull login error messages**
- **Improve and polish other maps**
- **Add Credits Screen**
- **Add Settings Screen**
- **Improve Race Scene Decoration**

--- 

## Dependencies:

This project relies on [Drift Game Highscores](https://github.com/AsteaFrostweb/2DDriftGameHighscores) for Highscore functionality.

