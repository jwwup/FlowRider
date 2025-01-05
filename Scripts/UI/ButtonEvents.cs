using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject optionUI; //옵션 ui 오브젝트
    
    //ui 버튼의 텍스트
    [SerializeField]
    private Text UIbuttonText1;
    [SerializeField]
    private Text UIbuttonText2;

    [SerializeField]
    private AudioClip clickSound; //ui요소 클릭시 효과음
    

    private AudioSource audioSource;
    private AudioSource audioSource2;
  

    void Awake()
    {
        optionUI.SetActive(false);
        audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource2 = SoundManager.instance.source;
    
    }

    //게임 시작을을 눌렀을 때 실행될 메서드
    public void OnStartButtonClick(){
        SoundManager.instance.PlaySound(clickSound);
        SceneManager.LoadScene("Game2Scene");
    }

    //옵션 버튼을 눌렀을 때 실행될 메서드
    public void OnOptionButtonClick(){
        SoundManager.instance.PlaySound(clickSound);
        optionUI.SetActive(true);
    }

    //조작키 변경경 버튼을 눌렀을 때 실행될 메서드
    public void OnChangeControlButtonClick(){
        SoundManager.instance.PlaySound(clickSound);
        SwapButtonText();
        GameManager.Instance.originalCommandPosition=!GameManager.Instance.originalCommandPosition;
        
    }

    //음악 버튼을 눌렀을 때 실행될 메서드
    public void OnMusicButtonClick(){
        SoundManager.instance.PlaySound(clickSound);
        
        GameManager.Instance.music=!GameManager.Instance.music;
    }

    //효과음 버튼을 눌렀을 때 실행될 메서드
    public void OnSoundFxButtonClick()
    {
        SoundManager.instance.PlaySound(clickSound);

        GameManager.Instance.soundEffect=!GameManager.Instance.soundEffect;
        
    }

    //옵션 UI 밖의 화면을 눌렀을 경우 실행될 메서드
    public void OnBackgroundTouched(){
        SoundManager.instance.PlaySound(clickSound);
        optionUI.SetActive(false);
    }

    //옵션 ui에 있는 버튼의 텍스트 변경
    private void SwapButtonText()
    {
        string tempText;
        tempText = UIbuttonText1.text;
        UIbuttonText1.text = UIbuttonText2.text;
        UIbuttonText2.text = tempText;

    }
    

    
}
