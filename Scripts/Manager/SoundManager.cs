using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // SoundManager의 싱글톤 인스턴스
    public static SoundManager instance { get; private set; }

    // 오디오 소스들
    public AudioSource source;  // 효과음 재생을 위한 오디오 소스
    private AudioSource audioSource;  // 배경음악을 위한 오디오 소스스

    // 싱글톤 설정 및 초기화
    private void Awake()
    {
        // 이미 인스턴스가 존재하면 삭제
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;  // 인스턴스를 설정
        DontDestroyOnLoad(gameObject);  // 씬 전환 시 오브젝트를 파괴하지 않도록 설정

        source = GetComponent<AudioSource>();
        audioSource = Camera.main?.GetComponent<AudioSource>();
    }

    // 씬이 로드될 때마다 호출되는 이벤트 등록
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    // 씬 로드 이벤트 해제
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }

    // 씬 로드 후 오디오 소스 업데이트트
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(UpdateAudioSource());  
    }

    private IEnumerator UpdateAudioSource()
    {
        yield return null;  
        audioSource = Camera.main?.GetComponent<AudioSource>();
    }

    // 특정 사운드를 재생하는 메서드
    public void PlaySound(AudioClip _sound)
    {
        if (_sound == null)
        {
            return;  
        }

        // 사운드를 한 번만 재생
        source.PlayOneShot(_sound);
    }


    private void Update()
    {
        // 게임 매니저의 배경음악 설정값에 따라 배경음악 음소거 처리리
        if (GameManager.Instance.music)
        {
            if (audioSource != null) audioSource.mute = false;  // 음악 켜짐
        }
        else
        {
            if (audioSource != null) audioSource.mute = true;  // 음악 꺼짐
        }

        // 게임 매니저의 효과음 설정값에 따라 음소거 처리리
        if (GameManager.Instance.soundEffect)
        {
            if (source != null) source.mute = false;  // 효과음 켜짐
        }
        else
        {
            if (source != null) source.mute = true;  // 효과음 꺼짐
        }
    }
}
