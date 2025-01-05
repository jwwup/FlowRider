using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSpawner : Spawner
{
    private Queue<GameObject> crowQueue = new Queue<GameObject>();

    private void Start() 
    {
        foreach (GameObject crow in enemies) //enemies배열에 있는 오브젝트를 큐에 삽입
        {
            crowQueue.Enqueue(crow);
        }
    }

    public override void Spawn() //큐에서 방출 시켜서 각각 설정값이 다른 crow들 활성화하고 다시 삽입
    {
        if (crowQueue.Count > 0)
        {
            GameObject crowToActivate = crowQueue.Dequeue();
            crowToActivate.SetActive(true);
            crowQueue.Enqueue(crowToActivate); 
        }
    }
}