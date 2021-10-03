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

    public static List<Vector3Int> desBlocks = new List<Vector3Int>();

    public float time;
    public float counter;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();

        waveCounter = Random.Range(waveTime.x, waveTime.y);
    }

    // Update is called once per frame
    void Update()
    {
        #region Waves-old

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

        #endregion

        if (counter < 0)
        {
            List<Vector3Int> v2 = new List<Vector3Int>();
            List<Vector3Int> v3 = new List<Vector3Int>();

            foreach (Vector3Int v in desBlocks)
            {
                tilemap.SetTile(v, null);

                if (tilemap.HasTile(new Vector3Int(v.x - 1, v.y, 0)))
                {
                    v2.Add(new Vector3Int(v.x - 1, v.y, 0));
                }

                if (tilemap.HasTile(new Vector3Int(v.x + 1, v.y, 0)))
                {
                    v2.Add(new Vector3Int(v.x + 1, v.y, 0));
                }

                if (tilemap.HasTile(new Vector3Int(v.x, v.y - 1, 0)))
                {
                    v2.Add(new Vector3Int(v.x, v.y - 1, 0));
                }

                if (tilemap.HasTile(new Vector3Int(v.x, v.y + 1, 0)))
                {
                    v2.Add(new Vector3Int(v.x, v.y + 1, 0));
                }


                v3.Add(v);
            }

            foreach (Vector3Int v in v2)
            {
                desBlocks.Add(v);
            }

            foreach (Vector3Int v in v3)
            {
                desBlocks.Remove(v);
            }

            counter = time;
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }

    public static void Drift(Vector3Int t)
    {
        //desBlocks.Add(t);
    }
}
