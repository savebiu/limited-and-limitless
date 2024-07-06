using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size/height;
    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = rows[0].cells;
    }

    private  void Start()
    { 
        Debug.Log(rows.Length  );
        for (int y = 0; y< rows.Length; y++)
        {
            for (int x = 0; x< cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }


    }

}
