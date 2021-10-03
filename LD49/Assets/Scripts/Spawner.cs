using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] toSpawn;
    public Vector2 radius;
    public Vector2 time;
    public float counter;
    public Vector4 speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 0)
        {
            Rigidbody2D rb = Instantiate(toSpawn[Random.Range(0, toSpawn.Length - 1)], transform.position + new Vector3(Random.Range(radius.x, -radius.x), Random.Range(radius.y, -radius.y)), Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = (new Vector3(Random.Range(speed.x, speed.y), Random.Range(speed.z, speed.w)));

            counter = Random.Range(time.x, time.y);
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }
}
