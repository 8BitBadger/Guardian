using Godot;
using System;

public class Shoot : Node2D
{
    [Signal]
    delegate void Hit(string name);

    //Refference to tanks KinematicBody2D
    KinematicBody2D tankBody;
    //Used to show the bullet path when the gun is fired
    Line2D traceLine;
    //The position were the ray cast hit
    Vector2 hitPos;
    //Timer used to time the flash of hte bullet
    Timer traceTimer;

    //We need a line rendered to show the path of the projectile
    //We need to cas a ray to the mouse coordinates
    public override void _Ready()
    {
        //Set up the timer for the muzzle flash
        traceTimer = new Timer();
        traceTimer.WaitTime = .05f;
        traceTimer.OneShot = true;
        traceTimer.Name = "traceTimer";

        //Create the new timer
        AddChild(traceTimer, true);
        GetNode<Timer>(traceTimer.Name).Connect("timeout", this, nameof(HideTrace));

        //Set the trace line properties
        traceLine = new Line2D();
        traceLine.Width = 1f;
        traceLine.Name = "traceLine";
        traceLine.DefaultColor = new Color(1, 1, 1);
        AddChild(traceLine);
        traceLine.Visible = false;

        //Set the refference for the tanks Kinematic Body 2D
        tankBody = GetNode<KinematicBody2D>("../../../../Player");
        //Connect to the input manager to read the mouses click event
        GetNode<InputManager>("../../../../InputManager").Connect("leftMouseClicked", this, nameof(Fire));
    }
    private void Fire()
    {
        //Set the start and end of the line2D and then make it visible
        traceLine.Points = new Vector2[] { GlobalPosition, GetGlobalMousePosition() };
        traceLine.Visible = true;
        traceTimer.Start();
        //Get a snapshot of the physics state of he world at this moment
        Physics2DDirectSpaceState worldState = GetWorld2d().DirectSpaceState;
        //Get the raycast hits and store them in a dictionary
        //Dictionary hits = worldState.IntersectRay(Position, GetGlobalMousePosition(), new object[]{this});
        Godot.Collections.Dictionary hits = worldState.IntersectRay(GlobalPosition, GetGlobalMousePosition(), new Godot.Collections.Array { tankBody }, tankBody.CollisionMask);

        if (hits.Count > 0)
        {
            if (hits.Contains("collider"))
            {
                //EmitSignal(nameof(Hit), ((KinematicBody2D)hits["collider"]).Name);
            }
        }
    }
    private void HideTrace()
    {
        //traceLine.Visible = false;
    }
}
