using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{

    public GameObject[] enemies; //생성할 enemy 배열

    public abstract void Spawn(); //enemy를 생성하는 메서드
    
        
    
}
