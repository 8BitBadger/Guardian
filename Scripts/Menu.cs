using Godot;
using System;

public class Menu : Node2D
{
    [Export]
    int speed;
    Sprite background;
    //Check where to move the background to
    bool moveRight = true;
    public override void _Ready()
    {
        //Grab the refference for the background sprite
        background = GetNode<Sprite>("Background");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (moveRight)
        {
            background.Position = new Vector2(background.Position.x + speed * delta, background.Position.y);
        }

    }
}
