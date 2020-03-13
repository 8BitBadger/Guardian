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
        UIEvent.RegisterListener(UpdateUI);
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
    private void UpdateUI(UIEvent uiEvent)
    {
        waveCountDown.Text = "Wave " + uiEvent.level + " starts in\n" + uiEvent.WaveTimeCountdown;
        if(uiEvent.countdownActive) 
        {
            waveCountDown.Show();
        }
        else
        {
            waveCountDown.Hide();
        }
    }
    public override void _ExitTree()
    {
        HealthEvent.UnregisterListener(UpdateHealthbar);
        UIEvent.UnregisterListener(UpdateUI);
    }
}
