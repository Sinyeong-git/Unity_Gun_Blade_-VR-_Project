using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class GunScript : MonoBehaviour
{
    // SteamVR 버튼 매핑
    public SteamVR_Action_Boolean GunTrigger;

    // SteamVR 핸드타입
    public SteamVR_Input_Sources handType;

    public Animator m_Animator; //Gun 에니메이터
    private Transform m_FireTransform; // 총구 위치 
    public ParticleSystem m_ShellEjectEffect; // 탄피배출 파티클
    public ParticleSystem m_MuzzleFlashEffect; // 총구화염 파티클

    public AudioSource m_GunAudioPlayer; //Gun 효과음 재생기
    public AudioClip m_ShotClip; // 발사 효과음
    public AudioClip m_ReloadClip; //재장전 효과음

    public GameObject m_ImapctPrefab; // 피탄 파티클

    public LineRenderer m_BulletLineRenderer; // 총 궤적 렌더러

    public Text m_AmmoText; // 장탄수 UI

    public int m_MaxAmmo = 10; //최대 장탄 수
    public float m_TimeBetFire = 0.05f; //발사와 발사 사이의 시간 간격
    public float m_Damage = 1;
    public float m_ReloadTime = 2.0f;
    public float m_FireDistance = 100f;

    private enum State { Ready, Empty };

    private State m_CurrentState = State.Empty; //현재 총 상태

    private float m_LastFireTime; // 총 마지막 발사 시간

    private int m_currentAmmo = 0; // 현 장탄 수

    Vector3 hitPosition;

    // Start is called before the first frame update
    void Start()
    {
        //GunTrigger 버튼매핑 (트리거) 
        GunTrigger.AddOnStateDownListener(TryFire, handType);

        m_CurrentState = State.Ready; // 초기 총 상태 초기화
        m_currentAmmo = m_MaxAmmo;

        m_MaxAmmo = 10; //최대 장탄 수
        m_TimeBetFire = 0.05f; //발사와 발사 사이의 시간 간격
        m_Damage = 1;
        m_ReloadTime = 2.0f;
        m_FireDistance = 100f;

        m_FireTransform = transform.Find("FirePosition").transform;


        m_LastFireTime = 0; //마지막 발사 시점 초기화

        m_BulletLineRenderer.positionCount = 2; //라인 렌더러가 사용할 정점을 두개로 지정
        m_BulletLineRenderer.enabled = false; // 라인 렌더러 끄기

        UpdateUI(); //UI 갱신



        /*
            왼손 오른손 바꿈 처리를 하고 싶을때 사용하면 될듯
            handType = SteamVR_Input_Sources.RightHand;
        */

    }



    // Update is called once per frame
    void Update()
    {

    }

    //GunTrigger 처리
    public void TryFire(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //발사 가능 상태일때
        if (m_CurrentState == State.Ready && Time.time >= m_LastFireTime + m_TimeBetFire)
        {
            m_LastFireTime = Time.time; // 마지막 총 쏜 시점 현재 시점으로 갱신
            Fire();
            UpdateUI();
        }
    }

    //발사 처리
    public void Fire()
    {
        RaycastHit hit; // 레이케스트 정보를 저장하는, 충돌 정보 컨테이너

       
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("AvoidCube"));  // Everything에서 Player 레이어만 제외하고 충돌 체크함
            
        //총 피탄 위치 : 처음 총구 위치 + 총구 위치 앞쪽 방향 * 사정거리
        hitPosition = m_FireTransform.position + m_FireTransform.forward * m_FireDistance;

        //레이케스트(시작지점, 방향 , 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(m_FireTransform.position, m_FireTransform.forward, out hit, m_FireDistance , layerMask))
        {

            //상대방 IdamagAble 인터페이스 가져오기
            IDamageAble target = hit.collider.GetComponent<IDamageAble>();

            IMenuAble targetM = hit.collider.GetComponent<IMenuAble>();

            if(targetM != null)
            {
                targetM.OnDamage();
            }

            //IDamageAble 인터페이스 존재시 데미지 계산
            if (target != null)
            {
                target.OnDamageGun(m_Damage);
            }

            //충돌 위치 가져오기
            hitPosition = hit.point;

            //피탄 위치에 파티클 생성, 충돌지점에 생성, 충돌 표면의 방향으로 생성
            GameObject decal = Instantiate(m_ImapctPrefab, hitPosition, Quaternion.LookRotation(hit.normal));
            //충돌 파티클을 충돌 물체의 자식으로
            decal.transform.SetParent(hit.collider.transform);
        }

        //발사이펙트 코루틴 재생 시작
        StartCoroutine(FireEffect(hitPosition));
        //남은 탄환의 수 -1
        m_currentAmmo--;

        //장탄수가 0일때 상태,에니메이션 전환
        if (m_currentAmmo <= 0)
        {
            m_CurrentState = State.Empty;
            m_Animator.SetTrigger("NoAmmo"); //NoAmmo 에니메이션 트리거
        }

    }

    //발사 이펙트 , 총알 궤적 표시 코루틴
    private IEnumerator FireEffect(Vector3 hitPosition)
    {
        //총 발사 에니메이션 트리거 작동
        m_Animator.SetTrigger("Fire");

        // 총알 궤적 LineRenderer 키기
        m_BulletLineRenderer.enabled = true;

        //line 시작 점 = 총구 위치
        m_BulletLineRenderer.SetPosition(0, m_FireTransform.position);
        //line 끝 점 = 입력으로 받은 피탄 위치
        m_BulletLineRenderer.SetPosition(1, hitPosition);

        //이펙트 재생
        m_MuzzleFlashEffect.Play(); // 머즐 플래시 파티클 재생
        m_ShellEjectEffect.Play(); // 탄피 배출 파티클 재생


        //이미 총 발사 소리일경우 생략
        if (m_GunAudioPlayer.clip != m_ShotClip)
        {
            m_GunAudioPlayer.clip = m_ShotClip; // 총 발사 소리로 설정
        }
        m_GunAudioPlayer.Play();//총격 소리 재생

        yield return new WaitForSeconds(0.07f); // 처리를 잠시 쉬는 시간

        //대기 후 궤적 해제
        m_BulletLineRenderer.enabled = false;

    }

    //장탄 UI 갱신
    private void UpdateUI()
    {
        if (m_CurrentState == State.Empty)
        {
            m_AmmoText.text = "EMPTY";
        }
        else
        {
            m_AmmoText.text = m_currentAmmo.ToString();
        }
    }

    //재장선 시도
    public void TryReload()
    {
        if (m_currentAmmo != m_MaxAmmo)
        {
            Reload();
        }
    }

    //재장전 처리
    private void Reload()
    {
        Debug.Log("리로드");

        m_CurrentState = State.Ready;
        m_GunAudioPlayer.clip = m_ReloadClip; // 오디오 소스 장전 소리로 설정
        m_GunAudioPlayer.Play(); //재장전 소리 재생
        m_Animator.Play("GunIdle");
        m_currentAmmo = m_MaxAmmo;
        UpdateUI();
    }
}
