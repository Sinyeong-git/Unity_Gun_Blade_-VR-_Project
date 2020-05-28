using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberScript : MonoBehaviour
{
    
    //히트박스 레이어 마스크
    public LayerMask HitBox_layer;

    public Vector3 previousPos;

    public float Contrlloer_velocity;

    public int Saber_Damage = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.tag == "Shootable")
        {
            //가속도 값이 X 이상이면 블럭 파괴로 인식
            //그냥 가까이 대고 있는건 일단 파괴 안함
            if (Contrlloer_velocity > 2)
            {
                IDamageAble target = col.GetComponent<IDamageAble>();
                //IDamageAble 인터페이스 존재시 데미지 계산
                if (target != null)
                {
                    target.OnDamageSaber(Saber_Damage);
                }
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        //컨트롤러의 가속도 값을 구하기 위한 공식
        Contrlloer_velocity = ((transform.position - previousPos).magnitude) / Time.deltaTime;


        //이전 컨틀로러 위치 값 대입
        previousPos = transform.position;
        
    }
}
