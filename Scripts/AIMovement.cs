using Godot;
using System;
public enum AIStates
{
    Wander,
    Attack,
    MoveTo,
    LookForTarget
};
public class AIMovement : KinematicBody2D
{
    //The vector for hte targets position, need to rethink this
    Vector2 target;
    //The current state of the AI used for behaviour, for now very low level
    AIStates myState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set the target to the dome by defualt so it goes and attacks it by defualt
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //Cycle through and run the ai behaviour that is set to the ais currnet state
        switch (myState)
        {
            case AIStates.Attack:
            break;
            case AIStates.MoveTo:
            break;
            case AIStates.Wander:
            break;
            case AIStates.LookForTarget:
            break;
        }

        //If im being attacked then switch target to the attacker

        if (Position.DistanceTo(target) < 10)
        {
            GD.Print("Bang Bang I shoot you!!!");
            //Shoot it now
        }
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

    private void MoveTo(Vector2 pos)
    {

    }
}
