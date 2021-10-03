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
    private float realSpeed;
    public Transform highlight;

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

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (transform.parent)
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
        }

        #region Carry-old

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            if (carry && !tilemap.HasTile(Highlight.highlightPos) && carryCounter <= 0)
            {
                tilemap.SetTile(Highlight.highlightPos, tile);

                carry = false;

                carryCounter = 0.1f;
            }

            if (!carry && tilemap.HasTile(Highlight.highlightPos) && carryCounter <= 0)
            {
                tile = tilemap.GetTile(Highlight.highlightPos);
                tilemap.SetTile(Highlight.highlightPos, null);

                carry = true;

                carryCounter = 0.1f;
            }
        }

        if (carryCounter > 0)
        {
            carryCounter -= Time.deltaTime;
        }*/

        #endregion

        if (Physics2D.OverlapCircle(groundPos.position, 0.05f, ground))
        {
            if (land == false)
            {
               //Vector3Int t = tilemap.WorldToCell(transform.position);
                //Manager.Drift(t + Vector3Int.down);
            }

            land = true;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            land = false;

            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb.gravityScale = 0.5f;
            }
            else
            {
                rb.gravityScale = 2f;
            }
        }

        if (jumpCounter > 0)
        {
            jumpCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * realSpeed, rb.velocity.y);

        if (Physics2D.OverlapCircle(groundPos.position, 0.1f, ground))
        {

        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.gravityScale = 0.5f;

                realSpeed = speed * 2;
            }
            else
            {
                rb.gravityScale = 2f;

                realSpeed = speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Tilemap"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tilemap"))
        {
            transform.parent = null;
        }
    }
}