using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Manager : MonoBehaviour
{
    private Tilemap tilemap;

    public Vector2 waveTime;
    private float waveCounter;
    public Vector2Int waveStrength;

    public Vector2Int size;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();

        waveCounter = Random.Range(waveTime.x, waveTime.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCounter > 0)
        {
            waveCounter -= Time.deltaTime;
        }
        else
        { 
            List<Vector3Int> allTiles = new List<Vector3Int>();

            for (int i = -size.x; i < size.x; i++)
            {
                for (int o = -size.y; o < size.y; o++)
                {
                    if (tilemap.HasTile(new Vector3Int(i, o, 0)))
                    {
                        allTiles.Add(new Vector3Int(i, o, 0));
                    }
                }
            }

            for (int i = 0; i < Random.Range(waveStrength.x, waveStrength.y); i++)
            {
                tilemap.SetTile(allTiles[Random.Range(0, allTiles.Count - 1)], null);
            }

            waveCounter = Random.Range(waveTime.x, waveTime.y);
        }
    }
}
