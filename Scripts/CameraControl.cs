using Godot;
using System;

public class CameraControl : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connect to the input Manager
        GetNode<Node2D>("../InputManager").Connect("mouseClicked", this, nameof(MouseReleased));
    }
    private void MouseReleased(Vector2 mousePos)
    {

    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
