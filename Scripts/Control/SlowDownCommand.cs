using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownCommand : Command
{
    [SerializeField]
    private PlayerController player;

    public override void Execute(){} //slow down은 OnClick에 동작하지 않음

    public override void PointerDown() //버튼을 누르고 있으면 속도감소 여부를 확인하는 변수 true로 설정
    {
        player.isSlowButtonPressed=true; 
    }
    public override void PointerUp() //버튼을 떼면 속도감소 여부를 확인하는 변수 false로 설정
    {
        player.isSlowButtonPressed=false;
    }

    void Start()
    {
        player=GetComponent<PlayerController>();
    }
}
