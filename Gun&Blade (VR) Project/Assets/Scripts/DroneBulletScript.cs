﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBulletScript : MonoBehaviour , IDamageAble
{
    // HMD 콜라이더 위치 담겨있는 게임오브젝트
    public GameObject HmdManager;

    //HMD Vector3 포지션
    private Vector3 Hmd_Position;

    //큐브 이동 속도
    public float speed = 10.0f;

    //큐브 HP
    public float health = 1;


   
    public void OnDamageGun(float damageAmount)
    {
        // Debug.Log("Got Damage!");
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void OnDamageSaber(float damageAmount)
    {
        // Debug.Log("Got Damage!");
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        speed = 8.0f;

        //HMDManager Gameobject 찾고, 위치값 가져오기
        HmdManager = GameObject.Find("HeadCollider");
        Hmd_Position = HmdManager.transform.position;

        //원래 HMD 위치보다 조금 더 간 위치까지 보내기 위한 계산
        Hmd_Position += (Hmd_Position - gameObject.transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Hmd_Position, speed * Time.deltaTime);
    }
}
