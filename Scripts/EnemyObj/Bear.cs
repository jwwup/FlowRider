using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bear : MonoBehaviour
{
    public float speed; //곰의 속도
    [SerializeField]
    private float plusSpeed; //곰의 속도 증가량

    [SerializeField]
    private Text bearSpeedText; //곰의 속도 텍스트 UI

    

    void Start()
    {
        speed = 3.0f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.reachedCertainScore += increaseSpeed; //특정 점수에 도달하는 이벤트에 등록
        }
      
    }

    private void increaseSpeed(int score) //일정 점수를 초과 할때마다 속도 증가
    {
        if(speed<15f)
        {
            speed+=plusSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right*Time.deltaTime*speed,Space.World); //오른쪽으로 이동
        bearSpeedText.text="Bear speed:" + speed.ToString("0.00"); //소수점 둘째 자리까지 출력
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.reachedCertainScore -= increaseSpeed; //파괴 시 등록 해제 
        }
    }
}
