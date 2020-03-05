using Godot;
using System;
namespace EventCallbacks
{
    public class Health : Node2D
    {
        //Make the health of the enemy tank set able outside in the inspector
        [Export]
        int health = 100;
        //The signal to the fx node - To be added later
        [Signal]
        public delegate void BeingAttacked(String targetName, String attackerName);
        //The signal when the tank dies
        [Signal]
        public delegate void Die(String name);
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            //Connect to the players Hit emmit
            //GetTree().GetNodesInGroup("Player");
            //GetNode<Shoot>("../../Player/Body/Gun/BulletSpawnPoint").Connect("Hit", this, nameof(WasHit));
            //Connection to the AIs Hit emmit
            //GetNode<AIMovement>("../../SmallTank").Connect("Hit", this, nameof(WasHit));
            //GetNode<AIMovement>("../../SmallTank2").Connect("Hit", this, nameof(WasHit));

        }

        public override void _Input(InputEvent @event)
        {
            //If the mouse wa clicked
            if (@event is InputEventMouseButton)
            {
                if (@event.IsActionPressed("leftMouseButton"))
                {
                    Vector2 mousePos = GetGlobalMousePosition();
                    Die();
                }
            }
        }

        private void WasHit(String targetName, String attackerName)
        {
            if (GetParent().Name == targetName)
            {
                GD.Print(this.GetParent() + " takes damage");
                //Reduce the enemy tank health with a fixed amount for now
                health -= 10;
                //Check if the health has gone down to zero
                CheckHealth();
                //If health is low send a signal to the fx node to change to the relevant fx
                EmitSignal(nameof(BeingAttacked), attackerName);
            }
        }
        private void CheckHealth()
        {
            if (health <= 0)
            {
                //Make sure the health does not go below 0
                health = 0;
                Die();
            }
        }
        void Die()
        {
            // I am dying for some reason.

            UnitDeathEvent udei = new UnitDeathEvent();
            udei.Description = "Unit " + this.Name + " has died.";
            udei.UnitNode = this;
            udei.FireEvent();
            //Remove the parent node forn the scene
            GetParent().QueueFree();

        }
    }
}
