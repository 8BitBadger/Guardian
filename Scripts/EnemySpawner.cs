using Godot;
using System;
using System.Collections.Generic;

public class EnemySpawner : Node2D
{
    //The level of the wave only have ten levels
    int waveLevel = 1;
    [Export]
    //The point were the enemie will spawn
    Vector2[] spawnPoints = new Vector2[8];
    //A list of all the enemy scenes
    List<PackedScene> enemyScenes = new List<PackedScene>();
    PackedScene trapScene;
    List<Node2D> enemies = new List<Node2D>();
    List<Node2D> traps = new List<Node2D>();
    //Random number generator used to choose the spawn locations for the enemies
    RandomNumberGenerator rng = new RandomNumberGenerator();

    TileMap map;
    public override void _Ready()
    {
        map = GetNode<TileMap>("../Map/TileMap");
        enemyScenes.Add(ResourceLoader.Load("res://Scenes/SmallTank.tscn") as PackedScene);
        enemyScenes.Add(ResourceLoader.Load("res://Scenes/LargeTank.tscn") as PackedScene);
        trapScene = ResourceLoader.Load("res://Scenes/Trap.tscn") as PackedScene;
        SetTraps();
        SpawnWave();
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

    private void SpawnWave()
    {
        //Randomize the number set before we use it
        rng.Randomize();
        for (int i = 0; i < waveLevel * 1.35; i++)
        {
            Vector2 spawnLocation = spawnPoints[rng.RandiRange(0, spawnPoints.Length - 1)];
            Node tempNode = enemyScenes[0].Instance();
            ((Node2D)tempNode).Position = spawnLocation;
            AddChild(tempNode);

            //enemies[i].Position = spawnLocation;
            //enemies[i].Name = "enemy" + i; 
            //enemies.Add(tempNode);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}