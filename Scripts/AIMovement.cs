using Godot;
using System;
using EventCallback;

public enum AIStates
{
    BeingAttacked,
    Attack,
    MoveTo,
    LookForTarget
};
public class AIMovement : KinematicBody2D
{
    //The movemnt speed of the tank
    [Export]
    int speed = 80;
    //Signal to send to the health script attached to the player
    [Signal]
    public delegate void Hit(String targetName, String attackerName);
    //The current state of the AI used for behaviour, for now very low level
    AIStates myState;
    Sprite gunSprite, muzzleFlash;
    //The timer for the muzzle flash and the attack interval
    Timer attackTimer, flashTimer;
    //The name of the target
    Node2D target;

    bool canAttack = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        attackTimer = new Timer();
        //Set up the timer for the muzzle flash
        attackTimer.WaitTime = 2f;
        attackTimer.OneShot = true;
        attackTimer.Name = "attacktTimer";
        //Create the new timer
        AddChild(attackTimer, true);

        GetNode<Timer>(attackTimer.Name).Connect("timeout", this, nameof(attackReset));

        flashTimer = new Timer();
        //Set up the timer for the muzzle flash
        flashTimer.WaitTime = .05f;
        flashTimer.OneShot = true;
        flashTimer.Name = "flashTimer";
        AddChild(flashTimer, true);

        //Grab the timer node for the muzzle flash
        GetNode<Timer>(flashTimer.Name).Connect("timeout", this, nameof(HideFlash));
        //Get this nodes health scripgt and init the conevtion
        //GetNode<Health>("Health").Connect("BeingAttacked", this, nameof(BeingAttacked));

        UnitHitEvent.RegisterListener(BeingAttacked);

        gunSprite = (Sprite)FindNode("Gun");

        muzzleFlash = (Sprite)FindNode("MuzzleFlash");

        myState = AIStates.MoveTo;
        //Set the defualt target to the crystal
        target = GetNode<Node2D>("../../Crystal");

        //Set the target to the dome by defualt so it goes and attacks it by defualt
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //targetPos = crystalKB2D.Position;
        //Cycle through and run the ai behaviour that is set to the ais currnet state
        switch (myState)
        {
            case AIStates.Attack:
                Attack();
                break;
            case AIStates.MoveTo:
                MoveTo();
                break;
            case AIStates.BeingAttacked:
                break;
            case AIStates.LookForTarget:
                break;
        }

        //Godot.Collections.Array incommingSignals = GetIncomingConnections();
        //Godot.Collections.Dictionary temDict = (Godot.Collections.Dictionary)incommingSignals[0];
        //GD.Print(temDict["signal_name"]);

        //If im being attacked then switch target to the attacker


    }

    private void SetTarget(Vector2 target)
    {

    }

    private void WasHit(int id)
    {
        GD.Print("AI checking if I was hit");
    }

    private void Wander()
    {

    }

    private Vector2 ScanForTarget()
    {
        return Vector2.Zero;
    }

    private void MoveTo()
    {
        //If the target is in position we switch tto the attack state
        if (Position.DistanceTo(target.Position) < 300)
        {
            //Change the state to the attack state
            myState = AIStates.Attack;
        }
        else
        {
            //Get the direction to move in
            Vector2 dir = target.Position - Position;
            //Normalizing direction for movement
            dir = dir.Normalized();
            //Move and slide has the phsics delta already worked in and using it helps the objects slide past each other
            MoveAndSlide(dir * speed);
            //Rotate to target
            Rotation = Mathf.LerpAngle(Rotation, dir.Angle(), 0.2f);
        }
    }

    private void Attack()
    {
        //Check if the target is still in the desired distane
        if (Position.DistanceTo(target.Position) > 300)
        {
            //Change the state to the attack state
            myState = AIStates.MoveTo;
        }
        else
        {
            //Get the direction to move in
            Vector2 dir = target.Position - Position;
            //Normalizing direction for movement
            dir = dir.Normalized();
            //gunSprite.Rotation = Mathf.LerpAngle(gunSprite.Rotation, dir.Angle(), 0.2f);
            gunSprite.LookAt(target.Position);
        }

        //If the attack timer has not run out yet we just return out of the method
        if (!canAttack) return;
        //Reset the can attack bool
        canAttack = false;
        //Start the attack timer
        attackTimer.Start();

        //so I am brute forcing the heck out of this mthod sorry 
        ShowFlash();

        Physics2DDirectSpaceState worldState = GetWorld2d().DirectSpaceState;
        //Get the raycast hits and store them in a dictionary
        Godot.Collections.Dictionary hits = worldState.IntersectRay(GlobalPosition, target.GlobalPosition, new Godot.Collections.Array { this }, this.CollisionMask);
        //Check if there was a hit
        if (hits.Count > 0)
        {
            if (hits.Contains("collider"))
            {
                UnitHitEvent uhei = new UnitHitEvent();
                uhei.attacker = (Node2D)GetParent();
                uhei.target = (Node2D)hits["collider"];
                uhei.damage = 5;
                uhei.Description = uhei.attacker.Name + " attacked " + uhei.target.Name;
                uhei.FireEvent();
            }
        }

    }
    private void BeingAttacked(UnitHitEvent unitHit)
    {
        if (this.Name == unitHit.target.Name)
        {
            GD.Print("AttackerName = " + unitHit.attacker.Name);
            target = (Node2D)unitHit.attacker;
        }

    }

    private void ShowFlash()
    {
        flashTimer.Start();
        muzzleFlash.Visible = true;
    }

    private void HideFlash()
    {
        muzzleFlash.Visible = false;
    }

    private void attackReset()
    {
        canAttack = true;
    }

    public override void _ExitTree()
    {
        UnitHitEvent.UnregisterListener(BeingAttacked);
    }
}
