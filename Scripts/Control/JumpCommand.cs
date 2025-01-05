using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command  //점프 명령을 구현하는 클래스
{   
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private float jumpHeight;  
    [SerializeField]
    public AudioClip jumpSound;
    private Rigidbody2D body;

    //버튼을 클릭 시 플레이어 점프
    public override void Execute()
    {
        if(player.grounded) //기본 점프
        {
            body.velocity = Vector2.up * jumpHeight; //속도를 위쪽으로 설정하여 플레이어가 점프
            player.grounded = false;
            player.jumped = true;

            SoundManager.instance.PlaySound(jumpSound);
            player.animator.SetBool("Jump", true);
        }
        else if (!player.grounded && player.jumped) //더블 점프
        {
            body.velocity = Vector2.up * jumpHeight;
            player.grounded = false;
            player.jumped = false;

            SoundManager.instance.PlaySound(jumpSound);    
        }
    }

    //점프 명령어는 버튼의 pointerDown,Up에 동작하지 않음
    public override void PointerDown(){}
    public override void PointerUp(){}


    private void Awake()
    {
        player=GetComponent<PlayerController>();
        body = GetComponent<Rigidbody2D>();
    }


}
