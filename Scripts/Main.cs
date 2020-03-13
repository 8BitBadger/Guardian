using Godot;
using System;
using EventCallback;

public class Main : Node2D
{
    //The preloads for thte game
    PackedScene mapScene = new PackedScene();
    PackedScene playerScene = new PackedScene();
    PackedScene crystalScene = new PackedScene();
    PackedScene enemySpawnerScene = new PackedScene();
    PackedScene UIScene = new PackedScene();
    PackedScene missilePickupScene = new PackedScene();
    //The nodes that will link to the objects once instanced
    Node map, player, crystal, enemySpawner, ui;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Grabs the map scene from the Scene tree on the games start up
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        playerScene = ResourceLoader.Load("res://Scenes/Player.tscn") as PackedScene;
        crystalScene = ResourceLoader.Load("res://Scenes/Crystal.tscn") as PackedScene;
        enemySpawnerScene = ResourceLoader.Load("res://Scenes/EnemySpawner.tscn") as PackedScene;
        UIScene = ResourceLoader.Load("res://Scenes/UI.tscn") as PackedScene;
        missilePickupScene = ResourceLoader.Load("res://Scenes/Missile.tscn") as PackedScene;
        MainEvent.RegisterListener(UIEventFired);
        UnitDeathEvent.RegisterListener(EndGame);

        ui = UIScene.Instance();
        ui.Name = "UI";
        AddChild(ui);
    }
    private void GameStart()
    {
        //The instance of the map packed scene
        map = mapScene.Instance();
        //Change the name of the map node
        map.Name = "Map";
        //Instantiate the firts level
        AddChild(map);
        //Instance the player packed scene
        player = playerScene.Instance();
        player.Name = "Player";
        //Set the players start position, I was to lazy to calculate the 
        //exact position so I used the tile size and position to do it in program, don't judge me its late at night
        ((Node2D)player).Position = new Vector2(64 * 20, 64 * 18);
        AddChild(player);
        //Instance the crystal packed scene
        crystal = crystalScene.Instance();
        crystal.Name = "Crystal";
        AddChild(crystal);
        //Instance the enemySpawned packed scene
        enemySpawner = enemySpawnerScene.Instance();
        enemySpawner.Name = "EnemySpawner";
        AddChild(enemySpawner);

    }
    private void EndGame(UnitDeathEvent ude)
    {
        if (ude.UnitNode.IsInGroup("Player"))
        {
            enemySpawner.QueueFree();
            crystal.QueueFree();
            player.QueueFree();
            map.QueueFree();
        }
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
    private void UIEventFired(MainEvent mainEvent)
    {
        //If the ui is activated then we start the game
        if (mainEvent.startBtnPressed) GameStart();
    }
    public override void _ExitTree()
    {
        MainEvent.UnregisterListener(UIEventFired);
        UnitDeathEvent.UnregisterListener(EndGame);
    }
}
