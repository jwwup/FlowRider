using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollspeed = 0.2f; //배경을 스크롤 하는 속도
    Material myMaterial; //배경에 적용된 Material

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        float newOffSetX = myMaterial.mainTextureOffset.x + scrollspeed * Time.deltaTime; // 현재 텍스처 오프셋 값에 스크롤 속도를 기반으로 새로운 x 오프셋 값을 추가
        Vector2 newOffset = new Vector2(newOffSetX, 0); //y값은 그대로 두고 x값만 변경된 새로운 Vector2 생성
        myMaterial.mainTextureOffset = newOffset; //변경된 오프셋 값을 적용
    }
}