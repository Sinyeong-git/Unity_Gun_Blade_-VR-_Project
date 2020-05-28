using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour, IDamageAble
{
    public GameObject Type;
    public Transform ParentPoint;

    // HMD 콜라이더 위치 담겨있는 게임오브젝트
    public GameObject HmdManager;

    //HMD Vector3 포지션
    private Vector3 Hmd_Position;

    //큐브 이동 속도
    public float speed = 1.0f;

    //큐브 HP
    public float health = 1;

    //무적상태 On,Off
    private enum State {HitOn, HitOff};
    private State hit_state = State.HitOn;

    private bool moveWhere = true;

    public float readyFire;

    public void OnDamageSaber(float damageAmount)
    {
        if (hit_state == State.HitOn) {

            health -= damageAmount;

            if (health <= 0)
                {
                    Destroy(gameObject);
                }
        }
    }

    public void OnDamageGun(float damageAmount)
    {
        if (hit_state == State.HitOn)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator GodTime()
    {
        hit_state = State.HitOff;

        yield return new WaitForSeconds(0.5f);

        hit_state = State.HitOn;
    }

    // Start is called before the first frame update
    void Start()
    {
        readyFire = 5f;

        speed = 0.5f;

        //때릴수 있는 상태로 초기화
        hit_state = State.HitOn;


        int temp = Random.Range(1, 3);

        if(temp == 1)
        {
           
            moveWhere = true;
        }
        else
        {
            moveWhere = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (readyFire < 0)
        {
            readyFire = 5;

            //부모 오브젝트에 위치에 생성
            GameObject DroneBullet = Instantiate(Type, ParentPoint);
            DroneBullet.transform.parent = null;
        }

        if(moveWhere == true){
            transform.position = transform.position + Vector3.right * speed * 0.05f;
        }
        else if(moveWhere == false)
        {
            transform.position = transform.position + Vector3.left * speed * 0.05f;
        }

        if (this.transform.position.x > 10)
        {
            moveWhere = false;  
        }
        else if (this.transform.position.x < -10)
        {
            moveWhere = true;
        }


        readyFire -= Time.deltaTime;
    }



}

