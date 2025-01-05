using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : Spawner
{
   

    public override void Spawn() //비활성화 상태인 오브젝트를 찾아서 활성화
    {
        foreach (GameObject instance in enemies)
        {
            if (!instance.activeInHierarchy)
            {
                instance.SetActive(true);
                break;
            }
        }
    }

}