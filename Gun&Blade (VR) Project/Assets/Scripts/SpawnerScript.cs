using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] Type;
    public Transform ParentPoint;
    
    //스폰타이밍
    public float SmallCubeSpawnTime;
    public float BigCubeSpawnTime;

    //스폰타이머
    private float SmallCubeSpawnTimer;
    private float BigCubeSpawnTimer;


    // Start is called before the first frame update
    void Start()
    {
        SmallCubeSpawnTime = 1.0f;
        BigCubeSpawnTime = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SmallCubeSpawnTimer > SmallCubeSpawnTime)
        {
            //부모 오브젝트에 위치에 생성
            GameObject SmallCube = Instantiate(Type[0], ParentPoint);
            
            //spawnPosition 랜덤값 생성 => 로컬포지션에 대입
            Vector3 spawnPosition = new Vector3(Random.Range(-4, 4), Random.Range(0, 2), 0);           
            SmallCube.transform.localPosition = spawnPosition;

            //타이머 초기화
            SmallCubeSpawnTimer -= SmallCubeSpawnTime;      
        }

        
        if (BigCubeSpawnTimer > BigCubeSpawnTime)
        {
            //부모 오브젝트에 위치에 생성
            GameObject BigCube = Instantiate(Type[1], ParentPoint);

            
            Vector3 spawnPosition = new Vector3();
            spawnPosition = new Vector3(0, 0, 0);
 


            BigCube.transform.localPosition = spawnPosition;

            //타이머 초기화
            BigCubeSpawnTimer -= BigCubeSpawnTime;      
        }
        

        SmallCubeSpawnTimer += Time.deltaTime;
        BigCubeSpawnTimer += Time.deltaTime;

    }
}
