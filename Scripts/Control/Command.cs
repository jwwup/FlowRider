using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour //각 명령을 구현할 추상 클래스
{
    public abstract void  Execute();  //버튼의 Onclick 이벤트와 연결

    public abstract void  PointerDown(); //버튼의 PointerDown 이벤트와 연결

    public abstract void  PointerUp(); // 버튼의 PointerUp 이벤트와 연결
    
}
