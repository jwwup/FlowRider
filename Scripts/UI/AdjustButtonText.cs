using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustButtonText : MonoBehaviour
{
    //게임 매니저의 커맨드 위치 설정값에 따라서 텍스트 변경경
    [SerializeField]
    private Text leftButtonText; 
    [SerializeField]
    private Text rightButtonText;

    private string slowDown;
    private string jump;

    void Awake()
    {
        jump="Jump";
        slowDown="Slow \n Down";
    }


    void Start()
    {
        if(GameManager.Instance.originalCommandPosition) //게임 매니저의 설정값에 맞게 문자열 수정
        {
            rightButtonText.text = jump;
            leftButtonText.text = slowDown;
        }
        else
        {
            rightButtonText.text = slowDown;
            leftButtonText.text = jump;
        }

    }


}
