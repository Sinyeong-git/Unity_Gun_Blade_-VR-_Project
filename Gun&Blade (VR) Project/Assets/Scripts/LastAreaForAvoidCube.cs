﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAreaForAvoidCube : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "AvoidCube")
        {
            Destroy(col.gameObject);         
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
