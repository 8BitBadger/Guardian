using Godot;
using System;
using EventCallback;

public class Trap : Node2D
{
    //Refference to the traps collision shape 
    CollisionShape2D trapCollider;
    //The particle effect for the trap
    CPUParticles2D fireParticles;
    //The timer for the trap to activate and deactivate
    Timer trapTimer;
    //a bool to toggle if the trap goes on or off
    bool trapActive;
    public override void _Ready()
    {
        trapTimer = GetNode<Timer>("OnOffTimer");
        trapCollider = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        fireParticles = GetNode<CPUParticles2D>("Area2D/CPUParticles2D");
        //Switch the trap and effects off when initialized
        trapCollider.Disabled = true;
        fireParticles.Emitting = false;
        //Start the timer for the trap
        trapTimer.Start();
        //Connect to the timers time out in order to toggle the trap on or off
        GetNode<Timer>("OnOffTimer").Connect("timeout", this, nameof(ToggleTrap));
        GetNode<Area2D>("Area2D").Connect("body_entered", this, nameof(BodyEntered));
    }

    private void ToggleTrap()
    {
        //Toggle the trp to the oposite of what it is currently
        trapActive = !trapActive;

        //Check the status of the trap toggle and set the collider and particles acordingly
        if (trapActive)
        {
            trapCollider.Disabled = false;
            fireParticles.Emitting = true;
        }
        else
        {
            trapCollider.Disabled = true;
            fireParticles.Emitting = false;
        }
    }

    private void BodyEntered(Node node)
    {
        UnitHitEvent uhei = new UnitHitEvent();
        uhei.attacker = (Node2D)this;
        uhei.target = (Node2D)node;
        uhei.damage = 200;
        uhei.Description = uhei.attacker.Name + " attacked " + uhei.target.Name;
        uhei.FireEvent();
    }
}
