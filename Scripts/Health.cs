using Godot;
using System;

public class Health : Node2D
{
    //Make the health of the enemy tank set able outside in the inspector
    [Export]
    int health = 100;
    //The signal to the fx node - To be added later
    [Signal]
    delegate void fxState(int health);
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connect to the players Hit emmit
        GetNode<Shoot>("../../Player/Body/Gun/BulletSpawnPoint").Connect("Hit", this, nameof(WasHit));
    }
    private void WasHit(string myName)
    {
        if (GetParent().Name == myName)
        {
            //Reduce the enemy tank health with a fixed amount for now
            health -= 10;
CheckHealth();
            GD.Print(GetParent().Name + " says: Oucj now I only have " + health + " left");

            //If health is low send a signal to the fx node to change to the relevant fx
            EmitSignal(nameof(fxState), health);
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            GD.Print("Ok Im dead");
            //Delete this node
        }
    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
