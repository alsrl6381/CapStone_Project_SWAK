using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Offset")]
    public float speed;

    [Header("Input")]
    private float hAxis;
    private float vAxis;
    private bool wDown;
    private bool mClick;
    private bool IWork;
 //   private bool lAttack;

    [Header("Ray")]
    private Ray ray;
    public RaycastHit hit;
    private Ray ray_spacebar;
    private RaycastHit hit_spacebar;

    [Header("Class")]
    public GameManager gm;
    public GameObject NPC;
    private QuestManager questManager;

    [Header("Animation")]
    private Animator anim;

    [Header("Vector")]
    private Vector3 moveVec;
    private Vector3 dirVec;

    Rigidbody rigid;
    GameObject scanObject;
    


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        questManager = GameObject.Find("QuestInfo").GetComponent<QuestManager>();
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Inventory.inventoryActivated || GameManager.isAction))
        {
            Input_value();
            Move();
            Turn();
            Attack();
            Comunication(); 
            dirVector();          
        }
        Dialog();
        Comunication_spacebar();
    }
            
        
    

    void Input_value()
    {
        //프로젝트 셋팅에 인풋 매니저에 값이 정해져 있음
        hAxis = Input.GetAxisRaw("Horizontal"); //키보드 왼쪽 오른쪽 눌렀을 시 반응
        vAxis = Input.GetAxisRaw("Vertical"); //키보드 위 아래 눌렀을 시 반응
        wDown = Input.GetButton("Run"); //인풋 매니저에서 Walk 추가하여 왼쪽 시프트 눌렀을 시 반응
        mClick = Input.GetMouseButtonDown(0);
        IWork = Input.GetMouseButtonDown(0);
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //대각선을 누르면 루트2의 값만큼 앞으로 가기 때문에 노말라이즈드를 선언하여 속도는 1로 고정

        transform.position += moveVec * speed * (wDown ? 0.5f : 0.3f) * Time.deltaTime; // 현재 캐릭터의 포지션을 사용자가 입력한 방향*속도*누른 시간 만큼 캐릭터의 위치를 변경

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isRun", wDown);

    }
   
    void Dialog()
    {
        if(GameManager.isAction == true) { 
        anim.SetBool("isAttack", false);
        }
    }
    void Attack()
    {
        
        anim.SetBool("isWork", IWork);
        
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1f);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); //나아가는 방향으로 각도를 틀어주는 함수
    }

    void Comunication()
    {
        if (mClick)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && Vector3.Distance(transform.position, hit.transform.position) < 5f)
            {
                IfClickNPC();
            }

        }
    }
    void Comunication_spacebar() //스페이스 바로 상호작용하기
    {
       
        Debug.DrawRay(rigid.position, dirVec * 7f, new Color(2, 1, 3));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(rigid.position + new Vector3(0f, 1f, 0), dirVec,out hit_spacebar,0.7f, LayerMask.GetMask("Object")))
            {
                if (hit_spacebar.collider != null)
                {
                    scanObject = hit_spacebar.collider.gameObject;
                    gm.Action(scanObject);
                }
                else
                    scanObject = null;
            }
        }
    }
    void dirVector() //보는 방향을 정해줌 스페이스바로 상호작용 시 필요
    {

        if ((hAxis == 1 || hAxis == -1) && vAxis==0)
        {
            dirVec = new Vector3(hAxis, 0, 0);
        }
        else if ((vAxis == 1 || vAxis == -1)&&hAxis==0)
        {
            dirVec = new Vector3(0, 0, vAxis);
        }
        else if ((vAxis != 0) && (hAxis != 0))
        {
            dirVec = new Vector3(hAxis, 0, vAxis);
        }
    }     

    void IfClickNPC() 

    {
        if (hit.transform.gameObject.tag == "NPC") //NPC를 왼쪽 클릭했을 때
        {
            questManager.ShowQuestMenu();;

        }
    }

    
   

}


