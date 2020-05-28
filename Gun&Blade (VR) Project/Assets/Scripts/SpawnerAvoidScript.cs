using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAvoidScript : MonoBehaviour
{
    public GameObject Type;
    public Transform ParentPoint;
    
    //스폰타이밍
    public float AvoidSpawnTime;

    //스폰타이머
    private float AvoidSpawnTimer;
  


    // Start is called before the first frame update
    void Start()
    {
        AvoidSpawnTime = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (AvoidSpawnTimer > AvoidSpawnTime)
        {
            //부모 오브젝트에 위치에 생성
            GameObject AvoidCube = Instantiate(Type, ParentPoint);

            //spawnPosition 랜덤값 생성 => 로컬포지션에 대입
            int temp = Random.Range(1, 4);
            Vector3 spawnPosition = new Vector3();
            if (temp == 1)
            {
                spawnPosition = new Vector3(0, 2.5f, -40);
            }
            if (temp == 2)
            {
                spawnPosition = new Vector3(-2.39f, 0, -40);
            }
            if (temp == 3)
            {
                spawnPosition = new Vector3(2.39f, 0, -40);
            }

            AvoidCube.transform.localPosition = spawnPosition;

            //타이머 초기화
            AvoidSpawnTimer -= AvoidSpawnTime;

        }


        AvoidSpawnTimer += Time.deltaTime;
    }
}
