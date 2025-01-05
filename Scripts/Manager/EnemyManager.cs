
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Spawner[] spawners; //enemy를 생성하는 오브젝트 배열

    private Coroutine spawnCoroutine; //enemy를 생성하는 코루틴 참조하는 변수

    void Start()
    {
        spawnCoroutine=StartCoroutine(spawnEnemies(1,2f,3f));
        if (GameManager.Instance != null)
        {
            GameManager.Instance.reachedCertainScore += spawnDifferentEnemy; //특정 점수에 도달하는 이벤트에 등록
        }
    }

    private void spawnDifferentEnemy(int score) //현재 점수에 따라 생성하는 오브젝트의 종류, 생성 주기를 다르게 설정
    {
        if (score == 30)
        {
            ChangeSpawnerCoroutine(2, 2f, 3f);
        }
        else if (score == 50)
        {
            ChangeSpawnerCoroutine(3, 2f, 3f);
        }
        else if (score == 70)
        {
            ChangeSpawnerCoroutine(3, 1.7f, 2.7f);
        }

    }

    private void ChangeSpawnerCoroutine(int spawnerRange, float minCycle, float maxCycle) //생성할 오브젝트의 종류와 생성 주기를 받아서 코루틴 시작
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);  // 이전 코루틴 중단
        }

        spawnCoroutine = StartCoroutine(spawnEnemies(spawnerRange, minCycle, maxCycle));
    }   

    IEnumerator spawnEnemies(int spawnerRange, float minSpawnCycle, float maxSpawnCycle) //입력 받은 값에 따라 Spawner의 spawn메서드 호출
    {
        while(true)
        {
            int randomValue = Random.Range(0,spawnerRange);
            spawners[randomValue].Spawn();
            yield return new WaitForSeconds(Random.Range(minSpawnCycle, maxSpawnCycle));
        }
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.reachedCertainScore -= spawnDifferentEnemy; // 파괴시 이벤트 등록 해제 
        }
    }
    

}