using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeather : MonoBehaviour
{
    [SerializeField]
    private GameObject snowEffect; //눈 효과 파티클
    [SerializeField]
    private GameObject sky; //하늘 오브젝트

    private SpriteRenderer spriteRenderer; //sky오브젝트의 스프라이트 렌더러
    //낮, 밤의 지정 색상
    [SerializeField]
    private Color day;
    [SerializeField]
    private Color night;


    void Start()
    {
        spriteRenderer = sky.GetComponent<SpriteRenderer>(); 
        StartCoroutine(ChangeDayNight());
        StartCoroutine(ChangeSnowing());
    }

    private IEnumerator ChangeDayNight() //낮,밤을 바꾸는 코루틴
    {
        while (true)
        {
           //일정 시간마다 컬러를 변경
            if (spriteRenderer.color == day)
            {
                spriteRenderer.color = night;
            }
            else
            {
                spriteRenderer.color = day;
            }
            yield return new WaitForSeconds(24f);
            
        }
    }

    private IEnumerator ChangeSnowing() //랜덤한 시간마다 눈 효과 오브젝트 활성화
    {
        while (true)
        {
            snowEffect.SetActive(!snowEffect.activeSelf);
            
            yield return new WaitForSeconds(Random.Range(17f, 23f));
        }
    }


}
