using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool demoGround;

    public Vector2 times;
    public Vector2 speedsPos;
    public Vector2 speedsNeg;
    public Vector2 accels;

    public float counter;
    public float speed;
    public float accel;

    public float currAccel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter < 0)
        {
            counter = Random.Range(times.x, times.y);

            if (Random.value > 0.5f)
            {
                speed = Random.Range(speedsNeg.x, speedsNeg.y);
            }
            else
            {
                speed = Random.Range(speedsPos.x, speedsPos.y);
            }
            accel = Random.Range(accels.x, accels.y);
        }
        else
        {
            counter -= Time.deltaTime;

            if (currAccel < speed)
            {
                currAccel += accel;
            }
            if (currAccel > speed)
            {
                currAccel -= accel;
            }
        }

        transform.Rotate(new Vector3(0, 0, currAccel));
    }
}