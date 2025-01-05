using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearSign : MonoBehaviour
{
    [SerializeField]
    private Image image; //곰 경고판 이미지지
    private Coroutine fadeCoroutine; // 코루틴을 저장할 변수

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    //활성화 되면 페이드인아웃 코루틴 시작작
    void OnEnable()
    {
        // 이미 코루틴이 실행 중이면 새로 시작하지 않도록
        if (fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(FadeInOut()); // 코루틴 시작
        }
    }

    void OnDisable()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine); // 코루틴 중지
            fadeCoroutine = null; // 코루틴 변수 초기화
        }
    }

    // 투명도 0부터 1로 왔다 갔다 하는 코루틴
    private IEnumerator FadeInOut()
    {
        float duration = 1.0f; // 한 주기의 시간 (초)
        float elapsedTime = 0f; // 경과 시간

        // 처음 투명도 0으로 설정
        Color initialColor = image.color;
        initialColor.a = 0;
        image.color = initialColor;

        while (true)
        {
            elapsedTime += Time.deltaTime; // 경과 시간 업데이트
            float lerpValue = Mathf.PingPong(elapsedTime / duration, 1); // 0과 1 사이에서 왔다 갔다

            // 투명도만 업데이트
            Color currentColor = image.color;
            currentColor.a = lerpValue;
            image.color = currentColor;

            yield return null; // 한 프레임 대기
        }
    }
}