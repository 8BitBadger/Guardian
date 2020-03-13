using Godot;
using System;
using EventCallback;
public class Shoot : Node2D
{
    //Refference to tanks KinematicBody2D
    KinematicBody2D tankBody;

    bool missileUpgrade = false;
    //Used to show the bullet path when the gun is fired
    //Line2D traceLine;
    //The position were the ray cast hit
    //Vector2 hitPos;
    //Timer used to time the flash of hte bullet
    //Timer traceTimer;

    //We need a line rendered to show the path of the projectile
    //We need to cas a ray to the mouse coordinates
    public override void _Ready()
    {
        //Set up the timer for the muzzle flash
        //traceTimer = new Timer();
        //traceTimer.WaitTime = .05f;
        //traceTimer.OneShot = true;
        //traceTimer.Name = "traceTimer";

        //Create the new timer
        //AddChild(traceTimer, true);
        //GetNode<Timer>(traceTimer.Name).Connect("timeout", this, nameof(HideTrace));

        //Set the trace line properties
        //traceLine = new Line2D();
        //traceLine.Width = 1f;
        //traceLine.Name = "traceLine";
        //traceLine.DefaultColor = new Color(1, 1, 1);
        //AddChild(traceLine);
        //traceLine.Visible = false;

        //Set the refference for the tanks Kinematic Body 2D
        tankBody = GetNode<KinematicBody2D>("../../../../Player");
        //Connect to the input manager to read the mouses click event
        GetNode<InputManager>("../../../../InputManager").Connect("leftMouseClicked", this, nameof(Fire));

        MissilePickupEvent.RegisterListener();

    }
    private void Fire()
    {
        //Get a snapshot of the physics state of he world at this moment
        Physics2DDirectSpaceState worldState = GetWorld2d().DirectSpaceState;
        //Get the raycast hits and store them in a dictionary
        Godot.Collections.Dictionary hits = worldState.IntersectRay(GlobalPosition, GetGlobalMousePosition(), new Godot.Collections.Array { tankBody }, tankBody.CollisionMask);
        //Check if there was a hit
        if (hits.Count > 0)
        {
            
            //Change the line2d end position if there was a hit
            //hitPos = (Vector2)hits["position"];
            if (hits.Contains("collider"))
            {
                UnitHitEvent uhei = new UnitHitEvent();
                uhei.attacker = (Node2D)Owner;
                uhei.target = (Node2D)hits["collider"];
                uhei.damage = 50;
                uhei.Description = uhei.attacker.Name + " attacked " + uhei.target.Name;
                uhei.FireEvent();
            }
        }
        //Set the start and end of the line2D and then make it visible
        //traceLine.Points = new Vector2[] {Vector2.Zero, hitPos };
        //traceLine.Visible = true;
        //traceTimer.Start();
    }

    private void SetMissileUpgrade(MissilePickupEvent mpe)
    {
        missileUpgrade = mpe.missileUpgrade;
    }
    //private void HideTrace()
    //{
        //traceLine.Visible = false;
    //}
}
