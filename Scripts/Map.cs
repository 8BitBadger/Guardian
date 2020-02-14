using Godot;
using System;

public class Map : Node2D
{
    [Export]
    //The tile size of the map
    int mapSize = 20;
    //The tile data for the tile map
    TileData[,] tileDataMap;
    //The refeference for the map scenes TileMap
    TileMap tileMap;
    //Grab mouse clikced signal from the input manager, might need to assign input manager tot a node 2D in the main scnene itself
    public override void _Ready()
    {
        //Set the tile map data size
        tileDataMap = new TileData[mapSize, mapSize];
        //Grab a refference to the tile map node
        tileMap = GetNode<TileMap>("TileMap");
        //Connect to the input Manager
        GetNode<Node2D>("../InputManager").Connect("mouseClicked", this, nameof(CheckTileClicked));
        //
        InitMap();
        //Init test of the tile map generation
        GenerateMap();
    }

    private void InitMap()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                tileDataMap[x, y] = new TileData();
            }
        }
    }

    private void GenerateMap()
    {
        ClearMap();
        PopulateMap();
        CreateTileMap();
    }
    private void ClearMap()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                tileDataMap[x, y].Type = TileType.Forests;
            }
        }
    }
    private void PopulateMap()
    {
        //Populate the map with forests and mountains
    }
    private void CreateTileMap()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                tileMap.SetCellv(new Vector2(x, y), (int)tileDataMap[x, y].Type, false, false, false);
            }
        }
        //Here we read the tile data map anad create the tile map from it

    }
    private void CheckTileClicked(Vector2 tilePos)
    {
        Vector2 tempPos = tileMap.WorldToMap(tilePos);
        int tile = tileMap.GetCellv(tempPos);
        GD.Print("Click registered by Tile Map at tile maps pos: " + tempPos + " tile clicked was: " + tile);
    }
}
