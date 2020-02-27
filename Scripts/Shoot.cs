using Godot;
using Godot.Collections;
using System;

public class Shoot : Node2D
{
    //Used to show the bullet path when the gun is fired
    Line2D traceLine;
Vector2 hitPos;

    //We need a line rendered to show the path of the projectile
    //We need to cas a ray to the mouse coordinates
    public override void _Ready()
    {
        //Set the trace line properties
        traceLine = new Line2D();
        
        GetNode<InputManager>("../../../../InputManager").Connect("leftMouseClicked", this, nameof(Fire));
    }
    private void Fire()
    {

    }
}
