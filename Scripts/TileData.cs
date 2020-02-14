using Godot;
using System;

public enum TileType
{
    Empty = -1,
    Grass,
    Forests,
    Mountains,
    Foresters,
    Mines,
    ManaShrines,
    Farms,
    House
};

public class TileData
{
    //The type of the tile
    TileType type;

    //The setter and getter for the tiles type, used for doing extra work whe the tile type is changed
    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            if (type != TileType.Grass)
            {
                //If the tile type was anything other than grass and we change it to something new we reset teh level and production Tick
                level = 1;
                productionTick = 1f;
                type = value;
            }
            else
            {
                //if the tile is of type grass then we just set the tile to the the new type
                type = value;
            }
        }
    }
    //If i have time impliment a leveling system to enable quicker resource creation
    int level;
    //The max amount of people the tile can handle
    int peopleCap;
    //The time interval for the generation of the resources, defualt is every second resources are produced
    float productionTick = 1f;

}
