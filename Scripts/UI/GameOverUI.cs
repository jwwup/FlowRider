using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //점수 관련 텍스트
    [SerializeField]
    private Text UIScoreText; 
    [SerializeField]
    private Text UIBestScoreText;

    public void OnRestartButtonClick() //재시작 버튼을 눌렀을 때 실행될 메서드
    {
        
        GameManager.Instance.Initialize(); 
        
        SceneManager.LoadScene("Game2Scene");  //게임 씬으로 이동
    }

    public void OnMenuButtonClick() //메뉴 버튼을 눌렀을 때 실행될 메서드
    {
        SceneManager.LoadScene("StartScene"); //시작 씬으로 이동
    }

    void OnEnable()
    {
        //게임 오버 UI가 활성화되면 점수 표시
       if (GameManager.Instance != null) 
        {
        UIScoreText.text = "Recent Score: " + GameManager.Instance.recentScore;
        UIBestScoreText.text = "Best Score: " + GameManager.Instance.bestScore;
        }
    }
}