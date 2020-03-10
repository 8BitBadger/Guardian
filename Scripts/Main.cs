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
    //The nodes that will link to the objects once instanced
    Node map, player, crystal, enemySpawner;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Grabs the map scene from the Scene tree on the games start up
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        playerScene = ResourceLoader.Load("res://Scenes/Player.tscn") as PackedScene;
        crystalScene = ResourceLoader.Load("res://Scenes/Crystal.tscn") as PackedScene;
        enemySpawnerScene = ResourceLoader.Load("res://Scenes/EnemySpawner.tscn") as PackedScene;
        UIEvent.RegisterListener(UIEventFired);
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

    private void EndGame()
    {
        enemySpawner.QueueFree();
        crystal.QueueFree();
        player.QueueFree();
        map.QueueFree();
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
    private void UIEventFired(UIEvent uiEvent)
    {
        //If the ui is activated then we start the game
        if(uiEvent.uiActive) GameStart();

        if(uiEvent.winActive) EndGame();

        if(uiEvent.loseActive) EndGame();
    }
    public override void _ExitTree()
    {
        UIEvent.UnregisterListener(UIEventFired);
    }
}
