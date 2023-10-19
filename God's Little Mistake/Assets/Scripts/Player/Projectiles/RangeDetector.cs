using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : GameBehaviour
{
    public float range;
    public Vector3 positionShotFrom;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, positionShotFrom) >= range)
        {
            //print("Range is " + range + ". Out of range at " + Vector3.Distance(transform.position, _PC.transform.position));
            Destroy(gameObject);
        }

    }
}