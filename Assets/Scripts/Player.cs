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

    public QuestData quest; //����Ʈ�� ������

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
        //������Ʈ ���ÿ� ��ǲ �Ŵ����� ���� ������ ����
        hAxis = Input.GetAxisRaw("Horizontal"); //Ű���� ���� ������ ������ �� ����
        vAxis = Input.GetAxisRaw("Vertical"); //Ű���� �� �Ʒ� ������ �� ����
        wDown = Input.GetButton("Walk"); //��ǲ �Ŵ������� Walk �߰��Ͽ� ���� ����Ʈ ������ �� ����
        mClick = Input.GetMouseButtonDown(0);
        lAttack = Input.GetMouseButtonDown(0);
    }
  void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //�밢���� ������ ��Ʈ2�� ����ŭ ������ ���� ������ �븻������带 �����Ͽ� �ӵ��� 1�� ����

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // ���� ĳ������ �������� ����ڰ� �Է��� ����*�ӵ�*���� �ð� ��ŭ ĳ������ ��ġ�� ����

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

    }
    void Attack()
    {
        anim.SetBool("isAttack", lAttack);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); //���ư��� �������� ������ Ʋ���ִ� �Լ�
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
            else Debug.Log("�ƹ��͵� �浹���� ����");
        }
    }
}

