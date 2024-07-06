using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEditor.Tilemaps;
using UnityEngine;


public class TileBoard : MonoBehaviour
{
    public Tile tilePrefab;
    public TileState[] tileStates;

    private TileGrid grid;
    private List<Tile> tiles;



    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);

    }
    private void Start()
    {
        CreateTile();
        CreateTile();
    }

    private void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform.parent.parent);
        
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveTiles(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveTiles(Vector2Int.left, 1, 1, 0, 1);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
        }
    }
    public void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        for (int x = startX; x>=0 && x < grid.width; x+=incrementX)
        {
            for (int y=startY; y>=0 && y<grid.height; y+=incrementY)
            {
                TileCell cell = grid.GetCell(x, y);
                if (cell.occupied)
                {
                    MoveTile(cell.tile, direction);
                }
            }
        }
    }
    private void MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while(adjacent != null)
        {
            if (adjacent.occupied)
            {
                //TODO: merging
                break;
            }
            newCell= adjacent;
            adjacent= grid.GetAdjacentCell(adjacent, direction);
        }
        if(newCell != null)
        {
            tile.MoveTo(newCell);
        }
    }
}