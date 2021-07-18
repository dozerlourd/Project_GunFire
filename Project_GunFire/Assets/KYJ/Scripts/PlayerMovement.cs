using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // 사용자의 키 입력에 따라 이동하고 싶다
    // 필요 요소 : 이동속도,이동방향
    float finalMoveSpeed;
    [SerializeField] float moveSpeed = 5;

    // * 속력 조절 (?)
    public float speedAdjust = 2f;
    [SerializeField] Vector3 dir;

    //중력
    private float gravity = -9.81f;

    float dir_y;

    //점프
    public float jumpPower = 10f;

    public bool isMovingFast = false;

    CharacterController cc;

    Coroutine Co_InitSpeed;
    [SerializeField] float dashSpeed;
    WaitForSeconds initSpeedTime;

    public float DashCooltime => dashCooltime;
    [SerializeField] float dashCooltime;

    public float CurrDashCooltime => currDashCooltime;
    float currDashCooltime = 0;

    bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        isMovingFast = false;
        finalMoveSpeed = moveSpeed;
        initSpeedTime = new WaitForSeconds(dashSpeed);
        StartCoroutine(DashCheck());
        StartCoroutine(MoveSound());
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }

    // 플레이어 이동
    void playerMove()
    {
        // 사용자의 키 입력(w,s,a,d)에 따라 이동
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = transform.TransformDirection(dir);

        isMove = h != 0 || v != 0 ? true : false;
        Gravity();

        finalMoveSpeed = isMovingFast ? (moveSpeed * speedAdjust) : moveSpeed;

        // 이동한다
        cc.Move(new Vector3(dir.x * finalMoveSpeed, dir_y, dir.z * finalMoveSpeed) * Time.deltaTime);
    }

    // 중력구현
    void Gravity()
    {
        // 만일, 바닥에 닿아 있으면..
        if (cc.isGrounded)
        {
            // y축에 중력을 가하고.
            dir_y += gravity * Time.deltaTime;

            // 점프가 가능
            Jump();


        }
        // 바닥에 닿아있지 않다면..
        else
        {
            // y축에 중력만 가한다.
            dir_y += gravity * Time.deltaTime;

        }

        dir.y = dir_y * Time.deltaTime;
    }

    // 점프구현.
    void Jump()
    {
        // 만일, 스페이스바를 누르면
        if (Input.GetButtonDown("Jump"))
        {
            // 점프한다
            dir_y = jumpPower;
        }
    }

    IEnumerator DashCheck()
    {
        StartCoroutine(CheckDashCooltime());
        while (true)
        {
            yield return new WaitUntil(() => currDashCooltime >= dashCooltime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
            isMovingFast = true;
            currDashCooltime = 0;
            if (Co_InitSpeed != null) StopCoroutine(Co_InitSpeed);
            Co_InitSpeed = StartCoroutine(InitSpeed());
        }
    }

    IEnumerator CheckDashCooltime()
    {
        while (true)
        {
            if (currDashCooltime < dashCooltime)
            {
                currDashCooltime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            else yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator InitSpeed()
    {
        yield return initSpeedTime;
        isMovingFast = false;
    }

    [SerializeField] AudioClip[] footstepClip;

    IEnumerator MoveSound()
    {
        while (true)
        {
            float seconds = isMovingFast ? 0.15f : 0.3f;
            yield return new WaitForSeconds(seconds);
            yield return new WaitUntil(() => isMove && cc.isGrounded);
            SoundManager.Instance.PlayOneShot(footstepClip[UnityEngine.Random.Range(0, footstepClip.Length)], 0.4f);
        }
    }
}