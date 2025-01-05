using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixYPos : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;

    private float[] initialYPositions; // 초기 Y 위치를 저장할 배열
    private Quaternion[] initialRotations; // 초기 회전 값을 저장할 배열


    void Start()
    {
        initialYPositions = new float[objects.Length]; // 배열 크기 설정
        initialRotations = new Quaternion[objects.Length]; // 배열 크기 설정

        // 각 오브젝트의 초기 Y 위치와 회전 값 저장
        for (int i = 0; i < objects.Length; i++)
        {
            initialYPositions[i] = objects[i].transform.position.y;
            initialRotations[i] = objects[i].transform.rotation;
        }
    }


    void Update()
    {
        // 각 오브젝트의 Y 위치와 회전 값을 처음 저장한 값으로 고정
        for (int i = 0; i < objects.Length; i++)
        {
            Vector3 currentPosition = objects[i].transform.position;
            currentPosition.y = initialYPositions[i]; // Y 값만 초기 값으로 설정
            objects[i].transform.position = currentPosition;
            
            objects[i].transform.rotation = initialRotations[i]; // 회전 값을 초기 값으로 설정
        }
    }
}