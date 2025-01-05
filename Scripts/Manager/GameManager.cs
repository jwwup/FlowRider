using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 게임 매니저 싱글톤 인스턴스
    public static GameManager Instance;

    // UI 요소들
    [SerializeField]
    private Text gameScoreText;  // 현재 점수 표시
    [SerializeField]
    public GameObject gameoverUI;  // 게임 오버 UI
    [SerializeField]
    private AudioClip gameOverSound;  // 게임 오버 사운드

    // 게임 관련 오브젝트들
    [SerializeField]
    private GameObject player;  // 플레이어 오브젝트
    [SerializeField]
    private GameObject bear;  // 곰 오브젝트
    [SerializeField]
    private GameObject warningSign;  // 경고 표지
    [SerializeField]
    private float safeDistance;  // 안전 거리

    // 게임 상태 정보
    public int bestScore = 0;  // 최고 점수
    public int recentScore = 0;  // 최근 점수

    // 특정 점수에 도달했을 때 이벤트
    public event Action<int> reachedCertainScore;
    private int lastCheckedScore = 0;  // 마지막으로 확인한 점수

    // 게임 설정
    public bool originalCommandPosition = true;  // 조작키 위치 설정 
    public bool music = true;  // 음악 활성화
    public bool soundEffect = true;  // 소리 효과 활성화

    // 게임 매니저 초기화 및 싱글톤 설정
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 파괴
            return;
        }

        Instance = this;  // 싱글톤 설정
        DontDestroyOnLoad(gameObject);  // 씬 전환 시 오브젝트 유지
    }

    // 게임 시작 시 초기화 작업
    void Start()
    {
        StartCoroutine(IncreaseScoreOverTime(1f));  // 일정 시간마다 점수 증가

        SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 로드 시 이벤트 등록
        Initialize();  // 게임 초기화
    }

    // 매 프레임마다 호출되는 업데이트 함수
    void Update()
    {
        // 최고 점수 갱신
        if (recentScore >= bestScore)
        {
            bestScore = recentScore;
        }

        // 점수 UI 갱신
        if (gameScoreText != null)
        {
            gameScoreText.text = "Score: " + recentScore;
        }

        // 점수 증가 확인
        CheckIncreasedScore();

        // 플레이어와 곰의 거리 계산
        CheckBearAndPlayer();
    }

    // 일정 점수에 도달했을 때 이벤트 호출
    private void CheckIncreasedScore()
    {
        // 10의 배수일 때마다 이벤트 호출
        if (recentScore / 10 > lastCheckedScore)
        {
            lastCheckedScore = recentScore / 10;  // 마지막으로 확인한 10의 배수 갱신
            reachedCertainScore?.Invoke(recentScore);  // 이벤트 호출
        }
    }

    // 플레이어와 곰의 거리 계산 및 경고 표지 활성화/비활성화
    private void CheckBearAndPlayer()
    {
        if (player != null && bear != null && warningSign != null)
        {
            float distance = Vector3.Distance(player.transform.position, bear.transform.position);  

            // 안전 거리보다 가까우면 경고 표지 활성화
            if (distance < safeDistance)
            {
                warningSign.SetActive(true);
            }
            else
            {
                warningSign.SetActive(false);  
            }
        }
    }

    // 일정 시간마다 점수 증가 (코루틴)
    IEnumerator IncreaseScoreOverTime(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);  
            recentScore++;  
        }
    }

    // 게임 오버 처리
    public void GameOver()
    {
        if(gameoverUI == null)
        {
            FindGameOverUI();  // 게임 오버 UI 찾기
        }
        gameoverUI.SetActive(true);  // 게임 오버 UI 활성화
        bear.SetActive(false);  // 곰 비활성화

        SoundManager.instance.PlaySound(gameOverSound);  // 게임 오버 사운드 재생
    }

    // 게임 초기화
    public void Initialize()
    {
        recentScore = 0; 
        Time.timeScale = 1;  
        safeDistance = 10f;  
        FindGameScoreText();  // 점수 UI 찾기
        FindGameOverUI();  // 게임 오버 UI 찾기
        FindSceneObjects();  // 씬 내 오브젝트 찾기

        if (gameoverUI != null)
        {
            gameoverUI.SetActive(false);  
        }
    }

    // 씬 로드 시 호출되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();  // 씬 로드 후 게임 초기화
    }

    // 점수 UI 찾기
    private void FindGameScoreText()
    {
        if (gameScoreText == null)
        {
           gameScoreText = GameObject.Find("Score")?.GetComponent<Text>();  // 점수 텍스트 찾기
        }
    }

    // 게임 오버 UI 찾기
    private void FindGameOverUI()
    {
        gameoverUI = GameObject.Find("GameOverObject")?.transform.Find("GameOverUI")?.gameObject;  // 게임 오버 UI 찾기
    }

    // 씬 내 오브젝트를 찾는 메서드
    private void FindSceneObjects()
    {
        // 플레이어 오브젝트 찾기
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");  // 태그를 사용하여 찾기
        }

        // 곰 오브젝트 찾기
        if (bear == null)
        {
            bear = GameObject.Find("Bear");  // 이름을 사용하여 찾기
        }

        // 경고 표지 오브젝트 찾기
        if (warningSign == null)
        {
            warningSign = GameObject.Find("WarningSign");  // 이름을 사용하여 찾기
        }
    }
}
