using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed; //속도 

    public float lifetime = 3f; //생존 시간
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left*Time.deltaTime*speed,Space.World); //왼쪽으로 이동
         
    }


    private void OnEnable()
    {
    
        StartCoroutine(DisableAfterTime());
        
    }
    
     private IEnumerator DisableAfterTime() // 설정된 생존 시간이 지난 후 비활성화
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

      void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("End")) // 맵의 끝 부분과 충돌하면 비활성화
        {
            gameObject.SetActive(false);
        }
    }
}
