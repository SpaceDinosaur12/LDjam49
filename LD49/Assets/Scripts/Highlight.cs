using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Highlight : MonoBehaviour
{
    public Vector2 direction;
    public Tilemap tilemap;

    public float movementY;
    public Movement player;
    public float movement;

    public static Vector3Int highlightPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = player.movement;
        movementY = Input.GetAxis("Vertical");

        if (movement != 0)
        {
            direction = new Vector2(movement, movementY);
        }

        if (direction.x > 0)
        {
            direction = new Vector2(1, direction.y);
        }

        if (direction.x < 0)
        {
            direction = new Vector2(-1, direction.y);
        }

        if (direction.y > 0)
        {
            direction = new Vector2(direction.x, 1);
        }

        if (direction.y < 0)
        {
            direction = new Vector2(direction.x, -1);
        }

        Vector3Int tilepos = tilemap.WorldToCell(player.transform.position);

        Vector3Int highPos = new Vector3Int((int)direction.x, (int)direction.y, 0);

        transform.position = tilemap.CellToWorld(tilepos + highPos);

        highlightPos = tilepos + highPos;
    }
}
