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

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);

        #region Carry-old

        if (Input.GetKeyDown(KeyCode.Z))
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
        }

        #endregion

        if (Physics2D.OverlapCircle(groundPos.position, 0.05f, ground))
        {
            if (land == false)
            {
               Vector3Int t = tilemap.WorldToCell(transform.position);
                Manager.Drift(t + Vector3Int.down);
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
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }
}