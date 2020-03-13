using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public class EnemySpawner : Node2D
{
    //The level of the wave only have ten levels
    int waveLevel = 10;
    [Export]
    int nextWaveTime = 5;
    [Export]
    //The point were the enemie will spawn
    Vector2[] spawnPoints = new Vector2[8];
    //A list of all the enemy scenes
    List<PackedScene> enemyScenes = new List<PackedScene>();
    //The packed scene to store the trap in for pre loading
    PackedScene trapScene;
    //Keeps score of the amount of enemies on thte board
    int enemyCount = 0;
    //Random number generator used to choose the spawn locations for the enemies
    RandomNumberGenerator rng = new RandomNumberGenerator();
    //The refference to the wave timer
    Timer waveTimer;

    TileMap map;
    public override void _Ready()
    {
        waveTimer = GetNode<Timer>("StartWaveTimer");
        //Connect to the Start wave timers timout method
        waveTimer.Connect("timeout", this, nameof(Countdown));
        //Start the new wave timer countdown
        waveTimer.Start();
        //Regestir the Unit death event to listn for unit deaths
        UnitDeathEvent.RegisterListener(EnemyDies);
        //Get a refference to the map, used later
        map = GetNode<TileMap>("../Map/TileMap");
        //Add the different enemies that cna be spawned
        enemyScenes.Add(ResourceLoader.Load("res://Scenes/SmallTank.tscn") as PackedScene);
        enemyScenes.Add(ResourceLoader.Load("res://Scenes/LargeTank.tscn") as PackedScene);
        //Load the traps that are used later
        trapScene = ResourceLoader.Load("res://Scenes/Trap.tscn") as PackedScene;
        //Set up the traps as needed
        SetTraps();

    }
    private void SetTraps()
    {
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (map.GetCell(x, y) == 0)
                {
                    Node tempNode = trapScene.Instance();
                    ((Node2D)tempNode).Position = new Vector2(x * 64 + 32, y * 64 + 32);
                    tempNode.Name = "Trap";
                    AddChild(tempNode);

                }
            }
        }
    }
    private void Countdown()
    {
        //Use the uievent system to update the up to update or hide the countdown timer as needed
        UIEvent uiEvent = new UIEvent();
        //If the countdown is still avtive send the message to show the count down timer label active
        uiEvent.countdownActive = true;
        //Update the wave countdown property
        nextWaveTime -= 1;
        //Set the count down timer to the updates time left
        uiEvent.WaveTimeCountdown = nextWaveTime;
        uiEvent.level = waveLevel;
        //If the timer has reached zero then we reset the timer value and send the message that the count down timer label needds to be hidden
        if (nextWaveTime == 0)
        {
            //We call the spawn method
            SpawnWave();
            //We reset the count down timer for the next wave
            nextWaveTime = 10;
            //We stop the count down timer
            waveTimer.Stop();
            //We hide the count down label
            uiEvent.countdownActive = false;
        }
        //We fire the UI event
        uiEvent.FireEvent();
    }

    private void SpawnWave()
    {
        GD.Print("Wave Level = " + waveLevel);
        //Randomize the number set before we use it
        rng.Randomize();
        for (int i = 0; i < waveLevel * 1.35; i++)
        {
            //Instance the new enmy object and set it to the temp node for editing
            Node tempNode = enemyScenes[0].Instance();
            //Get a random lacation from the spawn points list
            Vector2 spawnLocation = spawnPoints[rng.RandiRange(0, spawnPoints.Length - 1)];
            //Get a snapshot of the physics state of he world at this moment

            ((Node2D)tempNode).Position = spawnLocation;

            //Add the new enemy to the enemy spawner as a child of it
            AddChild(tempNode);
            //Increase the enemy count to keep track of how many enemies are on the board at any given time
            enemyCount++;
        }

        GD.Print( + enemyCount + " enemies spawned");
    }
    public void EnemyDies(UnitDeathEvent deathEvent)
    {
        //Check if the destroyd object was in the enemy group
        deathEvent.UnitNode.IsInGroup("Enemies");
        {
            //Subtract from thte enemy list
            enemyCount--;

        }
        if (enemyCount == 0)
        {
            //Add to the wave level
            waveLevel++;
            //Start the new wave timer countdown
            waveTimer.Start();
        }
        //Check if the wave level is done
        if (waveLevel == 11)
        {
            //Create a new win event ad populate it
            WinEvent win = new WinEvent();
            win.won = true;
            win.FireEvent();
            UIEvent uiEvent = new UIEvent();
            uiEvent.winActive = true;
            uiEvent.FireEvent();

        }
        //Go through the list of enemies and remove them from the list and then check if the list is empty if the list is 
        //empty then the next wave function is called

    }
    public override void _ExitTree()
    {
        UnitDeathEvent.UnregisterListener(EnemyDies);
    }
}