using Godot;
using System;
using EventCallback;

public class UI : Node2D
{
    //Refference to the health bar
    TextureProgress healthbar;
    Label waveCountDown;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        HealthEvent.RegisterListener(UpdateHealthbar);
        healthbar = GetNode<TextureProgress>("Healthbar");
        waveCountDown = GetNode<Label>("CountDown");
    }

    private void UpdateHealthbar(HealthEvent healthEvent)
    {
        //Update the healthbar to the players current health
        if (healthEvent.target.IsInGroup("Player"))
        {
            healthbar.Value = healthEvent.health;
        }
    }
    public override void _ExitTree()
    {
        HealthEvent.UnregisterListener(UpdateHealthbar);
    }
}
