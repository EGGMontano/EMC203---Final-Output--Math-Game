using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro.Examples;

public class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public List<GameObject> spawnedTiles = new List<GameObject>();

    public GameObject towerPrefab;
    public GameObject coinPrefab;
    public GameObject doorPrefab;

    public Text score;                    // Reference to UI Text for displaying score
    GameObject[] tiles;                   // Internal array to hold the instantiated tiles

    //Ground
    long groundBB = 0; // Bitboard Tracking Ground;
    long towerBB = 0; // Bitboard for our tower;

    //Money
    long moneyBB = 0;       //Money = tile
    long coinBB = 0;        //Coin = actual thing

    //Death
    long deathBB;
    long doorBB;


    public void CreateBoard()   //Create Board function, for button manager
    {
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new Vector3(r, 0, c);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tile.name = tile.tag + "_" + r + "_" + c;
                spawnedTiles.Add(tile);
            }
    }

    public void DeleteBoard()   //Delete Board function, for button manager
    {
        for (int i = 0; i < spawnedTiles.Count; i++)
        {
            if (spawnedTiles[i] != null)
                DestroyImmediate(spawnedTiles[i]);
        }
        spawnedTiles.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[64];       // Prepare storage for an 8×8 board
        for (int r = 0; r < 8; ++r)        // Loop over each row (0–7)
        {
            for (int c = 0; c < 8; ++c)    // Loop over each column (0–7)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                // Pick a random index into our tile prefabs
                Vector3 pos = new Vector3(c, 0, r);
                // Compute world position (x=c, y=0, z=r)
                GameObject tile = Instantiate(
                    tilePrefabs[randomTile],
                    pos,
                    Quaternion.identity
                );                     // Spawn that tile at the position with no rotation
                tile.name = tile.tag + "_" + r + "_" + c;
                // Rename for easy debugging, e.g. "Dirt_2_5"
                tiles[r * 8 + c] = tile;



                // Store reference in the 1D array
                if (tile.tag == "Ground") // if Ground is tag
                {
                    groundBB = SetCellState(groundBB, r, c);
                    PrintBB("Ground", groundBB);
                }

                // Money tag
                if (tile.tag == "Money") // if Money is tag
                {
                    moneyBB = SetCellState(moneyBB, r, c);
                    PrintBB("Money", moneyBB);
                }

                // Death tag
                if (tile.tag == "Death") // if Death is tag
                {
                    deathBB = SetCellState(deathBB, r, c);
                    PrintBB("Death", deathBB);
                }
            } // end for c
        } // end for r

        Debug.Log("Ground Cells = " + CellCount(groundBB));// print how many ground we have
        Debug.Log("Money Cells = " + CellCount(moneyBB));// print how many money we have
        Debug.Log("Death Cells = " + CellCount(deathBB));// print how many death we have


        InvokeRepeating("SpawnTower", 0.25f, 0.25f);     //ground spawn
        InvokeRepeating("SpawnCoin", 0.25f, 0.25f);   //money spawn
        InvokeRepeating("SpawnDoor", 0.25f, 0.25f);  //death spawn
    } // end Start

    void PrintBB(string name, long BB)
    {
        Debug.Log(name + ": " + Convert.ToString(BB, 2).PadLeft(64, '0'));
    }

    long SetCellState(long Bitboard, int row, int col)
    {
        long newBit = 1L << (row * 8 + col);
        return (Bitboard |= newBit);
    }

    bool GetCellState(long Bitboard, int row, int col)
    {
        long mask = 1L << (row * 8 + col);
        return ((Bitboard & mask) != 0);
    }

    int CellCount(long bitboard) // Count how many Cells is created base on tags
    {
        int count = 1;
        long bb = bitboard;
        while (bb != 0)
        {
            bb &= bb - 1;
            count++;
        }
        return count;
    }

    void SpawnTower() //spawns a tower on ground tiles
    {
        int rr = UnityEngine.Random.Range(0, 8);//random row
        int rc = UnityEngine.Random.Range(0, 8);//random column
        if (GetCellState(groundBB, rr, rc))
        {
            GameObject tower = Instantiate(towerPrefab);
            tower.transform.parent = tiles[rr * 8 + rc].transform;//parent it
            tower.transform.localPosition = new Vector3 (0, 1, 0);  //aligns it
            towerBB = SetCellState(towerBB, rr, rc);
        }
    }

    void SpawnCoin() //spawns coin on money tile
    {
        int rr = UnityEngine.Random.Range(0, 8);//random row
        int rc = UnityEngine.Random.Range(0, 8);//random column
        if (GetCellState(moneyBB, rr, rc))
        {
            GameObject coin = Instantiate(coinPrefab);                          //Change later//Change later//Change later//Change later
            coin.transform.parent = tiles[rr * 8 + rc].transform;   //parent it
            coin.transform.localPosition = new Vector3 (0, 2, 0);    //aligns it
            coinBB = SetCellState(coinBB, rr, rc);
        }
    }

    void SpawnDoor() // spawns door on death tile
    {
        int rr = UnityEngine.Random.Range(0, 8);//random row
        int rc = UnityEngine.Random.Range(0, 8);//random column
        if (GetCellState(deathBB, rr, rc))
        {
            GameObject door = Instantiate(doorPrefab);                          //Change later//Change later//Change later//Change later
            door.transform.parent = tiles[rr * 8 + rc].transform;//parent it
            door.transform.localPosition = Vector3.zero;//aligns it
            doorBB = SetCellState(doorBB, rr, rc);
        }
    }
}