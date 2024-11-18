# Tenacious Drift 2D

Welcome to Tenacious Drift 2D, a project to continue growing skills in software development; Focusing on intergration and creation of cross-platform online services within a video game context.<br>

## How to Run
### Client
- Clone repository onto your machine with Unity (2022.3.20f1) installed.<br>
- Open the Unity Hub and click: "**Add**" **->** "**Project from disk**" <br>
- Navigate to the folder/directory you cloned and select that folder to be the project folder.<br>
- Now, assuming to have Unity(2022.3.20f1), you just open and in the editor and build/run it.
- Once running you will be on a login screen. If the server is running you may login, if not then select **Offline Mode**.
- If you aren't using localhost you will need to use the [Developer Console](#developer-console) "set-ip" and "set-port" commands to update the port and IP.
- To open the developer console, if it isn't visible in thew top left corner already, you must press the backtick key ( ` ).
    
### Server
- Go to [Drift Game Highscores](https://github.com/AsteaFrostweb/2DDriftGameHighscores) and clone to your machine.
- Follow instructions in above repository's readme to get the server up and running.
- Once running you can: create your own account, view highscores through the webpage and access highscores funcitonality from the client.

## Why I Created This Project:

I Created Tenacious Drift 2D to learn and to improve as a developer; It's also just good fun!

#### Learning Goals
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

### Developer Console

The developer/debugging console is enabled with the **`** key.  It allows debugging and access to a limited selection of commands.

#### Commands
  - "show-ip"  :  Displays the current server IP assigned in the NetworkManager
  - "set-ip [ip]"  :  Sets the curerent server IP. Example: "set-ip localhost"
  - "set-port [port]"  : Sets the curerent server Port. Example: "set-port 25565"


### User accounts and Highscores

The user accounts and high scores are handles by the external [HighscoresApplication](https://github.com/AsteaFrostweb/2DDriftGameHighscores/tree/main). <br/>
The game then poll the server to determine whether to update the highscores for that particular person on that map. <br/>
The highscores contains different aspects like: <br/>
Longest Combo Time, Longest Combo Distance, Best Combo Score, Fastest Lap which can each be updated individually. 


### Main Menu 

#### Map Selection

The map selection screen on the main menu allows you to choose: Track, Car and Lap Count. There are 4 tracks and cars to chose from.

#### Settings screen

The settings screen is very limited but would be flushed out if I where to continue the project. <br/>
It currently only contains the ability to change the resolution of the game and to toggle fullscreen mode. <br/>

### Cars and Drifting

The cars have different sprites and stats which affect the way they handle and preform. <br/>
Depeinding on a cars grip, acceleration and velocity relative to direction the car is facing it can, and should, enter a drift. <br/>
This is an integral gameplay element and is whats use to generate combos and score. <br/>

### Tracks, Races and Checkpoints

Each track is divided up into a number of checopint "Nodes". <br/>
These nodes are a position and a radius. <br/>
Once a car comes within the nodes radius it has "reached" the node and that players "Target Node" becomes the next node in the track. <br/>
Once the player reaches the final node before the finish line it will enable completion of that lap.  <br/>
A race is a collection of laps and has some other information like which cars are involved ect. <br/>
In most places I have encluded the ability to handle multiple cars as I beleive BOT cars wouldn't be too difficult to implement with the node checkpoint system. <br/>
Therefore the races do have the ability to store multiple different car/node/lap states at once. <br/>

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

