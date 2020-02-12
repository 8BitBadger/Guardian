using Godot;
using System;

public class TileMap : Godot.TileMap
{
    //The refeference for the map scenes TileMap script
    TileMap mapScript;
    //Grab mouse clikced signal from the input manager, might need to assign input manager tot a node 2D in the main scnene itself
    public override void _Ready()
    {
        //Connect to the input Manager
        GetNode<Node2D>("../InputManager").Connect("mouseClicked", this, nameof(CheckTileClicked));
    }

    private void CheckTileClicked(Vector2 tilePos)
    {
        GD.Print("Click registered by Tile Map");
    }
}
