using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : Spawner
{
    
    
        public override void Spawn()
        {
              int index = Random.Range(0, enemies.Length); //랜덤 인덱스 설정

                if (!enemies[index].activeInHierarchy) //비활성화 상태인 배열요소 찾기 
                {
                    enemies[index].transform.position = transform.position;
                    Vector3 localPosition = enemies[index].transform.localPosition;
                    //각 인덱스에 있는 오브젝트와 알맞은 위치 설정
                    if (index == 0)
                    {
                        localPosition.y = 7.84f;
                    }
                    else if (index == 1)
                    {
                        localPosition.y = 7.6f;
                    }
                    else if (index == 2)
                    {
                        localPosition.y = 7.73f;
                    }
                    else if (index == 3)
                    {
                        localPosition.y = 8.1f;
                    }
                    else if (index ==4)
                    {
                        localPosition.y=7.0f;
                    }
                    enemies[index].transform.localPosition = localPosition;            
                    enemies[index].SetActive(true);  //선택된 오브젝트 활성화
                    
                }  
            
        }


}