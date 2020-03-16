using Godot;
using System;

public class Menu : Node2D
{
    [Export]
    int speed = 10;
    Sprite background;
    //Check where to move the background to
    bool moveRight = true;
    public override void _Ready()
    {
        //Grab the refference for the background sprite
        background = GetNode<Sprite>("../Background");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (background.Position.x <= 0)
        {
            SwingMovement();
        }
        else if (background.Position.x >= 1281)
        {
            SwingMovement();

        }
        if (moveRight)
        {
            background.Position = new Vector2(background.Position.x - speed * delta, background.Position.y);
        }
        else
        {
            background.Position = new Vector2(background.Position.x + speed * delta, background.Position.y);
        }
    }
    private void SwingMovement()
    {
        moveRight = !moveRight;
    }
}
