using Godot;
using System;

public class CameraControl : Camera2D
{
    bool mouseButtonHeld = false;
    [Export]
    bool LerpMovement = false;
    [Export]
    float lerpSpeed = 1f;
    [Export]
    float speed = 5f;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connect to the input Manager
        GetNode<Node2D>("../InputManager").Connect("rightMouseClicked", this, nameof(MouseClicked));
        GetNode<Node2D>("../InputManager").Connect("rightMouseReleased", this, nameof(MouseReleased));
    }
    private void MouseClicked(Vector2 mousePos)
    {
        mouseButtonHeld = true;
    }
    private void MouseReleased(Vector2 mousePos)
    {
        mouseButtonHeld = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //If hte mouse button is being held down check
        if (mouseButtonHeld)
        {
            //Get the distance difference from the camera to the mouses global position
            Vector2 diff = GetGlobalMousePosition() - Position;
            //Normalize the distance
            diff = diff.Normalized();
                //Else add the mouse position to the normalized distance thats been multiplied by the cameras speed
                Position += diff * speed;
        }
    }
}
