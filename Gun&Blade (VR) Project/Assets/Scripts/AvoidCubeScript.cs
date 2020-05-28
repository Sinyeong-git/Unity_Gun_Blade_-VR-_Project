using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCubeScript : MonoBehaviour
{

    //큐브 이동 속도
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Hmd_Position, speed * Time.deltaTime);
        transform.position = transform.position + Vector3.forward * speed*0.05f;
    }



}

