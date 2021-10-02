using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movement;
    public float movementY;
    public Vector2 direction;
    public float speed;
    public Transform highlight;
    public Vector3Int highlightPos;

    private Tilemap tilemap;
    public bool carry = false;
    public TileBase tile;
    private float carryCounter;

    public float jumpTime;
    private float jumpCounter;
    public float jumpForce;

    public Transform groundPos;
    public LayerMask ground;
    public bool land;

    public float rot;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement = Input.GetAxis("Horizontal");

        #region Carry-old


        /*movementY = Input.GetAxis("Vertical");

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
        }*/

        #endregion

        /*if (Physics2D.OverlapCircle(groundPos.position, 0.03f, ground))
        {
            if (land == false)
            {
               // Vector3Int tilepos = tilemap.WorldToCell(transform.position);
            }

            land = true;

            if (Input.GetKeyDown(KeyCode.X))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            land = false;
        }



        if (jumpCounter > 0)
        {
            jumpCounter -= Time.deltaTime;
        }*/



        if (Input.GetKey(KeyCode.RightArrow))
        {
            rot += 5;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot -= 5;
        }
    }
    
    private void FixedUpdate()
    {
        // rb.velocity = new Vector2(movement * speed, rb.velocity.y);

        if (land)
        {
            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, rot) * Vector2.right);

            rb.velocity = dir * speed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Tilemap"))
        {
            land = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.CompareTag("Tilemap"))
        {
            land = false;
        }
    }
}