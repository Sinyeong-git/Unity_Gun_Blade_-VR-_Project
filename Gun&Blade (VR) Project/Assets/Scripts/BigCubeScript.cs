using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigCubeScript : MonoBehaviour, IDamageAble
{
    // HMD 콜라이더 위치 담겨있는 게임오브젝트
    public GameObject HmdManager;

    //HMD Vector3 포지션
    private Vector3 Hmd_Position;

    //큐브 이동 속도
    public float speed = 1.0f;

    //큐브 HP
    public float health = 20;

    //무적상태 On,Off
    private enum State {HitOn, HitOff};
    private State BigCube_State = State.HitOn;

    Vector3 CubeMovemore;

    public void OnDamageSaber(float damageAmount)
    {
        if (BigCube_State == State.HitOn) {

            health -= damageAmount;
            CubeMovemore = this.transform.position;
            CubeMovemore.z -= 2;

            transform.DOMove(CubeMovemore, 0.5f);

            if (health <= 0)
                {
                    Destroy(gameObject);
                }
        }
    }

    public void OnDamageGun(float damageAmount)
    {

        if (BigCube_State == State.HitOn)
        {

            health -= damageAmount;
            CubeMovemore = this.transform.position;
            CubeMovemore.z -= 0.01f;

            transform.DOMove(CubeMovemore, 0.5f);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator GodTime()
    {
        BigCube_State = State.HitOff;

        yield return new WaitForSeconds(0.5f);

        BigCube_State = State.HitOn;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.7f;

        //때릴수 있는 상태로 초기화
        BigCube_State = State.HitOn;

        //HMDManager Gameobject 찾고, 위치값 가져오기
        HmdManager = GameObject.Find("HeadCollider");
        Hmd_Position = HmdManager.transform.position;

        //원래 HMD 위치보다 조금 더 간 위치까지 보내기 위한 계산
        Hmd_Position += (Hmd_Position - gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Hmd_Position, speed * Time.deltaTime);
        transform.position = transform.position + Vector3.forward * speed*0.05f;
    }



}

