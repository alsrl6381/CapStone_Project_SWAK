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
        //������Ʈ ���ÿ� ��ǲ �Ŵ����� ���� ������ ����
        hAxis = Input.GetAxisRaw("Horizontal"); //Ű���� ���� ������ ������ �� ����
        vAxis = Input.GetAxisRaw("Vertical"); //Ű���� �� �Ʒ� ������ �� ����
        wDown = Input.GetButton("Run"); //��ǲ �Ŵ������� Walk �߰��Ͽ� ���� ����Ʈ ������ �� ����
        mClick = Input.GetMouseButtonDown(0);
        IWork = Input.GetMouseButtonDown(0);
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //�밢���� ������ ��Ʈ2�� ����ŭ ������ ���� ������ �븻������带 �����Ͽ� �ӵ��� 1�� ����

        transform.position += moveVec * speed * (wDown ? 0.5f : 0.3f) * Time.deltaTime; // ���� ĳ������ �������� ����ڰ� �Է��� ����*�ӵ�*���� �ð� ��ŭ ĳ������ ��ġ�� ����

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
        transform.LookAt(transform.position + moveVec); //���ư��� �������� ������ Ʋ���ִ� �Լ�
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
    void Comunication_spacebar() //�����̽� �ٷ� ��ȣ�ۿ��ϱ�
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
    void dirVector() //���� ������ ������ �����̽��ٷ� ��ȣ�ۿ� �� �ʿ�
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
        if (hit.transform.gameObject.tag == "NPC") //NPC�� ���� Ŭ������ ��
        {
            questManager.ShowQuestMenu();;

        }
    }

    
   

}


