using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hazards : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tilemap"))
        {
            Tilemap tilemap = collision.GetComponent<Tilemap>();
            tilemap.SetTile(tilemap.WorldToCell(transform.position), null);
        }
    }
}
