using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool demoGround;

    public Interval[] intervals;
    public float counter;
    public int current = 0;
    public float rotLaval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter < 0)
        {
            counter = intervals[current].time;

            if (demoGround)
            {
                if (transform.rotation.z > 45)
                {
                    transform.rotation = Quaternion.Euler(0, 0, rotLaval);
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, intervals[current].rotation));
                }
            }
            else
            {
                while(transform.rotation.z < intervals[current].rotation)
                    transform.Rotate(new Vector3(0, 0, 1));
            }

            if (current < intervals.Length - 1)
            {
                current++;
            }
            else
            {
                current = intervals.Length - 1;
            }
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }
}

[System.Serializable]
public class Interval
{
    public float time;
    public float rotation;
}