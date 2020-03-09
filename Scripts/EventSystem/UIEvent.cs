using Godot;
using System;

namespace EventCallback
{
    public class UIEvent : Event<UIEvent>
    {
        //The health to display for the player
        int health;
        //The amount of kills the player has racked up
        int kills;
//The amount of time the before the new wave starts
        int WaveTimeCountdown;
    }
}
