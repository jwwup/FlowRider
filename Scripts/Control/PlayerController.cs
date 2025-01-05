using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    [SerializeField]
    private float plusSpeed; //속도 증가량
    [SerializeField]
    private float minusSpeed; //속도 감소량


    public bool grounded; //땅에 닿고 있는지를 확인하는 변수
    public bool jumped; //점프한 상태인지를 확인하는 변수(더블점프)
    [SerializeField]
    private Text speedText; //플레이어의 속도를 나타낼 택스트 UI
    
    public Animator animator;
    public bool isSlowButtonPressed = false; //속도 감소여부 체크

    private Command leftButton; //왼쪽 버튼이 참조할 커맨드
    private Command rightButton; //오른쪽 버튼이 참조할 커맨드

    
    

    private void Awake()
    {
        grounded = false;
        jumped = false;
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        if(GameManager.Instance.originalCommandPosition) //게임매니저에 설정되어 있는 커맨드 포지션 값에 따라 각 버튼이 참조하는 커맨드 변경
        {
            rightButton = GetComponent<JumpCommand>();
            leftButton = GetComponent<SlowDownCommand>();
        }
        else
        {
            rightButton = GetComponent<SlowDownCommand>();
            leftButton = GetComponent<JumpCommand>();
        }

    }

    void Update()
    {

        transform.Translate(Vector2.right * Time.deltaTime * speed, Space.World); //플레이어를 오른쪽으로 이동

        speed += plusSpeed * Time.deltaTime; //속도 증가

        if (isSlowButtonPressed || Input.GetKey(KeyCode.A)) // Slow 커맨드의 버튼이 눌리면 실행 (A키는 테스트용으로 버튼 클릭 대신 사용)
        {
            if(speed >= 1)
            {
                speed -= minusSpeed * Time.deltaTime; //속도 감소
                animator.SetBool("Brake", true);
            }
            
        }
        else
        {
            animator.SetBool("Brake", false);
        }

        if (Input.GetKeyDown(KeyCode.S))  //S키는 테스트용으로 버튼 클릭 대신 사용
        {
            OnRightButtonClick();
        }   

        if (speedText != null)
        {
            speedText.text = "Player speed: " + speed.ToString("0.00"); //속도를 소수점 2자리까지 출력
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            GameManager.Instance.GameOver(); //Enemy와의 충돌 시 게임매니저의 게임오버 메서드 호출
            speed=0f; plusSpeed=0f;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //바닥과 충돌시 관련 변수 값 설정
            animator.SetBool("Jump", false);
            grounded = true;
            jumped = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver(); //Enemy와의 충돌 시 게임매니저의 게임오버 메서드 호출
            speed=0f; plusSpeed=0f;
        }
        
        if(collision.gameObject.CompareTag("Snow")) 
        {
            speed=speed*0.5f; //snow와 충돌 시 속도 감소
        }

    }

    // 오른쪽 버튼 이벤트에 등록할 메서드 (버튼이 참조하고 있는 커멘드 실행)
    public void OnRightButtonClick()
    {
        rightButton.Execute();
    }

    public void OnRightButtonPointerDown()
    {
        rightButton.PointerDown();
    }

    public void OnRightButtonPointerUp()
    {
        rightButton.PointerUp();
    }


    //왼쪽 버튼 이벤트에 등록할 메서드 (버튼이 참조하고 있는 커멘드 실행)
    public void OnLeftButtonClick()
    {
        leftButton.Execute();
    }

    public void OnLeftButtonPointerDown()
    {
        leftButton.PointerDown();
    }

    public void OnLeftButtonPointerUp()
    {
        leftButton.PointerUp();
    }



}