using System.Collections;
using System.Collections.Generic;
using Godot;

namespace EventCallbacks
{
    public class DeathListener : Node
    {

        // Use this for initialization
        void Start()
        {
            UnitDeathEvent.RegisterListener(OnUnitDied);
        }

        void OnDestroy()
        {
            UnitDeathEvent.UnregisterListener(OnUnitDied);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnUnitDied(UnitDeathEvent unitDeath)
        {
            GD.Print("Alerted about unit death: " + unitDeath.UnitNode.Name);
        }
    }
}