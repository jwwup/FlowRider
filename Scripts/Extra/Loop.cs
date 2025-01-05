using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] Grounds; //계속해서 위치를 변경할 2개의 바닥 오브젝트

    [SerializeField]
    private GameObject movingObj; //움직이는 모든 오브젝트들을 자식으로 하는 부모 오브젝트


    /* 플레이어가 오른쪽으로 이동하면서 왼쪽에 있던 Ground 오브젝트의 트리거를 벗어나면 
       오른쪽에 있던 Ground 오브젝트의 자식으로 설정하고, 
       두 Ground 오브젝트의 위치를 교환하여 무한 루프를 돌도록 구현 */
    void OnTriggerExit2D(Collider2D collision) 
    {

        if(collision.gameObject.activeInHierarchy) // 게임 오브젝트가 활성화된 상태일 때만 부모를 변경하도록 조건 추가
        {
            if (collision.gameObject.CompareTag("Background1")) 
            {
                movingObj.transform.SetParent(Grounds[1].transform);
                swapPosition(Grounds[0], Grounds[1]);
            }
            else if (collision.gameObject.CompareTag("Background2"))
            {
                movingObj.transform.SetParent(Grounds[0].transform);
                swapPosition(Grounds[1], Grounds[0]);
            }
        }
        
    }


    private void swapPosition(GameObject n1, GameObject n2) //두개의 오브젝트의 위치를 변경
    {
        Vector2 temp = n1.transform.position;
        n1.transform.position = n2.transform.position;
        n2.transform.position = temp;
    }

    void OnEnable() //활성화 되면 일단 첫번째 Ground의 자식으로 설정
    {
        if (movingObj != null && Grounds.Length > 0)  
        {
            movingObj.transform.SetParent(Grounds[0].transform);
        }
    }
}
