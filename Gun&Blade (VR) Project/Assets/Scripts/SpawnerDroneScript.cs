using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDroneScript : MonoBehaviour
{
    public GameObject[] Type;
    public Transform ParentPoint;
    
    //스폰타이밍
    public float DronSpawnTime;

    //스폰타이머
    private float DronSpawnTimer;


    // Start is called before the first frame update
    void Start()
    {
        DronSpawnTime = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (DronSpawnTimer > DronSpawnTime)
        {
            //부모 오브젝트에 위치에 생성
            GameObject Drone = Instantiate(Type[0], ParentPoint);
            
            //spawnPosition 랜덤값 생성 => 로컬포지션에 대입
            Vector3 spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(0, 2), 0);
            Drone.transform.localPosition = spawnPosition;

            //타이머 초기화
            DronSpawnTimer -= DronSpawnTime;      
        }

        DronSpawnTimer += Time.deltaTime;

    }
}
