using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform target; //플레이어(카메라가 추적할 오브젝트)
    private Vector3 offset;  // 카메라와 대상 간의 거리
    private Vector3 fixedY; // 카메라의 y축 고정 값

    void Start()
    {
        if (target == null)
        {
            return;
        }
        
        offset = transform.position - target.position;
        fixedY.y = transform.position.y;  
    }

    void LateUpdate()  // 플레이어의 움직임이 먼저 반영된 후 카메라가 따라가도록
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;  //대상의 위치와 초기 오프셋을 더해 카메라의 목표 위치 계산
            targetPosition.y = fixedY.y; //y축 위치 고정
            transform.position = targetPosition; //목표위치로 위치 변경

        }
    }

}       