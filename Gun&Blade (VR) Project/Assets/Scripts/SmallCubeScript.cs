using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCubeScript : MonoBehaviour , IDamageAble
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


        /*
         
        자식 오브젝트로 실린더 큐브라인으로 날라오는 라인을 그려볼려 했지만
        쉐이더 문제로 일단 패스 
        Cull OFF 를 이용해 내부를 렌더링 해야 하는데 이 부분이 막혀서 일단은 없이 진행
       
        Color tempColor = mr.material.color;
        tempColor.a = 0f;
        mr.material.color = tempColor;
        StartCoroutine(FadeInOut(mr));     
         */
    }

   /*
    큐브 라인 렌더링 처리를 하려 했었던 부분
    private IEnumerator FadeInOut(MeshRenderer mr)
    {
        float fadeSpeed = 0.1f;

        Color tempColor = mr.material.color;

        while (tempColor.a < 0.5f)
        {
            tempColor.a += Time.deltaTime * fadeSpeed;
            mr.material.color = tempColor;
        }

        yield return new WaitForSeconds(1f);

        while (tempColor.a > 0)
        {
            tempColor.a -= Time.deltaTime * fadeSpeed;
            mr.material.color = tempColor;
        }

        yield return null; 
    }
    */


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Hmd_Position, speed * Time.deltaTime);
    }
}
