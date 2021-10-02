using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 direction;
    public float speed;
    public Transform highlight;
    public Vector3Int highlightPos;

    private Tilemap tilemap;
    public bool carry = false;
    public TileBase tile;
    private float carryCounter;

    public Collider2D col;
    public float jumpTime;
    private float jumpCounter;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movement != Vector2.zero)
        {
            direction = movement;
        }

        if (direction.x > 0)
        {
            direction = new Vector2(1, direction.y);
        }

        if (direction.y > 0)
        {
            direction = new Vector2(direction.x, 1);
        }


        if (direction.x < 0)
        {
            direction = new Vector2(-1, direction.y);
        }

        if (direction.y < 0)
        {
            direction = new Vector2(direction.x, -1);
        }

        Vector3Int tilepos = tilemap.WorldToCell(transform.position);

        Vector3Int highPos = new Vector3Int((int)direction.x, (int)direction.y, 0);

        highlight.position = tilemap.CellToWorld(tilepos + highPos) + new Vector3(0.5f, 0.5f);

        highlightPos = tilepos + highPos;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (carry && !tilemap.HasTile(highlightPos) && carryCounter <= 0)
            {
                tilemap.SetTile(highlightPos, tile);

                carry = false;

                carryCounter = 0.1f;
            }

            if (!carry && tilemap.HasTile(highlightPos) && carryCounter <= 0)
            {
                tile = tilemap.GetTile(highlightPos);
                tilemap.SetTile(highlightPos, null);

                carry = true;

                carryCounter = 0.1f;
            }
        }

        if (carryCounter > 0)
        {
            carryCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            jumpCounter = jumpTime;

            rb.velocity = (direction * jumpForce);
        }

        if (jumpCounter > 0)
        {
            jumpCounter -= Time.deltaTime;

            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }
    
    private void FixedUpdate()
    {
        if (jumpCounter <= 0)
        {
            rb.velocity = movement * speed;
        }
    }
}
