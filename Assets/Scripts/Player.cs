using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Offset")] 
    public float speed;

    [Header ("Input")]
    private float hAxis;
    private float vAxis;
    private bool wDown;
    private bool mClick;
    private bool lAttack;

    [Header ("Ray")]
    private Ray ray;
    private RaycastHit hit;
    private float Max_Distance = 3f;

    public GameManager gm;

    private Vector3 moveVec;
    private Animator anim;

    public GameObject NPC;

    public QuestData quest; //퀘스트를 가져옴

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Input_value();
        Move();
        Turn();
        Attack();
        Comunication();
        

    }

    void Input_value()
    {
        //프로젝트 셋팅에 인풋 매니저에 값이 정해져 있음
        hAxis = Input.GetAxisRaw("Horizontal"); //키보드 왼쪽 오른쪽 눌렀을 시 반응
        vAxis = Input.GetAxisRaw("Vertical"); //키보드 위 아래 눌렀을 시 반응
        wDown = Input.GetButton("Walk"); //인풋 매니저에서 Walk 추가하여 왼쪽 시프트 눌렀을 시 반응
        mClick = Input.GetMouseButtonDown(0);
        lAttack = Input.GetMouseButtonDown(0);
    }
  void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //대각선을 누르면 루트2의 값만큼 앞으로 가기 때문에 노말라이즈드를 선언하여 속도는 1로 고정

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // 현재 캐릭터의 포지션을 사용자가 입력한 방향*속도*누른 시간 만큼 캐릭터의 위치를 변경

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

    }
    void Attack()
    {
        anim.SetBool("isAttack", lAttack);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); //나아가는 방향으로 각도를 틀어주는 함수
    }

    void Comunication()
    {
        if(mClick)
        {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,Max_Distance))
            {
                if (hit.transform.gameObject.tag == "NPC")
                {
                    NPC = GameObject.FindWithTag("NPC"); 
                    NPC.GetComponent<Dialog_NPC>().Show_Hide(true);
                }
                    
            }
            else Debug.Log("아무것도 충돌하지 않음");
        }
    }
}

