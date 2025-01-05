using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField]
    private float speed; //늑대의 속도
    [SerializeField]
    private float jumpHeight; // 점프 높이
    public float lifetime = 10f; // 생존 시간

    [SerializeField]
    private GameObject spawner; 
    [SerializeField]
    private AudioClip attackSound;

    private bool hasDetectedPlayer = false; //플레이어 감지를 확인하는 변수

    private Rigidbody2D body;
    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.right * Time.deltaTime * speed, Space.World); // 오른쪽으로 이동

        if (!hasDetectedPlayer)  //한번도 플레이어를 감지하지 않은 상태면 래이캐스트 사용
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 10.0f,LayerMask.GetMask("Player"));
        
            if (hit.collider != null)
            {
                //플레이어를 감지하면 플레이어의 속도를 참조하여 attack 메서드 호출
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                attack(player.speed);
                hasDetectedPlayer = true; //플레이어를 감지한 상태로 설정
            }
          
            Debug.DrawRay(transform.position, Vector2.right * 10.0f, Color.red);
        }
        
    }

    private void attack(float playerSpeed) //플레이어의 속도를 받아서 더 빠르게 플레이어를 향하여 점프
    {
        animator.SetTrigger("Jump");
        SoundManager.instance.PlaySound(attackSound);
        speed=playerSpeed+7;
        body.velocity = Vector2.up * jumpHeight;
        StartCoroutine(DisableAfterJump());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("End")) // 맵의 끝 부분과 충돌하면 비활성화
        {
            gameObject.SetActive(false);
            
        }
    }

    private void OnEnable() //활성화되었을 때 초기화 작업 수행
    {
        hasDetectedPlayer = false;
        speed=2;
        if (spawner != null)
        {
            transform.position = spawner.transform.position;
        }
   
    }
   
    private IEnumerator DisableAfterJump()  //점프를 하고 일정 시간 이후 비활성화
    {
        yield return new WaitForSeconds(lifetime-5f);
        gameObject.SetActive(false);
    }
}