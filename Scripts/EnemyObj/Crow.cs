using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
   
    [SerializeField]
    private float speed = 5f; //까마귀의 속도 

    [SerializeField]
    private float amplitude = 1f; // 까마귀가 이동하는 최대 높이 (진폭)
    [SerializeField]
    private float frequency = 1f;  // 까마귀의 이동 주기 (y축 진동 속도)

    [SerializeField]
    private AudioClip crowSound;
    [SerializeField]
    private GameObject spawner;

    private Vector3 startPos; // 까마귀의 시작 위치
    private float elapsedTime = 0f; // 경과 시간 (x축 이동 및 y축 진동 계산에 사용)

    public float lifetime = 5f; // 생존 시간

    void Start()
    {
        
    }

    void Update()  //사인함수의 모양으로 이동
    {
        elapsedTime += Time.deltaTime;  // 경과 시간 갱신
        
        Vector3 parentPos = spawner.transform.position;  // 스폰 위치 가져오기

        float x = parentPos.x - speed * elapsedTime;  // x축 위치는 시간이 지남에 따라 왼쪽으로 이동

        float y = parentPos.y + Mathf.Sin(elapsedTime * frequency) * amplitude; // y축 위치는 Sine 함수를 사용해 상하로 진동

        
        transform.position = new Vector3(x, y, parentPos.z); // 까마귀의 위치를 갱신
    }

    private void OnEnable()
    {
         // 활성화되었을 때 초기화 작업 수행
        if (spawner != null)  
        {
            startPos = spawner.transform.position;
            transform.position = startPos;
            elapsedTime = 0f; 
            StartCoroutine(DisableAfterTime());
        }

    }

    private IEnumerator DisableAfterTime() // 설정된 생존 시간이 지난 후 비활성화
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = spawner.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("End"))   // 맵의 끝 부분과 충돌하면 비활성화
        {
            gameObject.SetActive(false);
        }
    }

    void OnBecameVisible()  // 까마귀가 카메라에 보일 때 소리를 재생
    {
        SoundManager.instance.PlaySound(crowSound);
    }
}