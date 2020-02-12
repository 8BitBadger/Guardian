using Godot;
using System;

public class Main : Node2D
{
//A refference to the map scene
PackedScene mapRef = new PackedScene();
Node map;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Grabs the map scene from the Scene tree on the games start up
        mapRef = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        //The instance of the map
        map = mapRef.Instance();
        //Change the name of the map node
        map.Name = "Map";
        //Instantiate the firts level
        AddChild(map);
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      
  }
}
