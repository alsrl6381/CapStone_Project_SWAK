using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; // 이름
    public GameObject go_Prefab; // 실제 설치될 프리팹
    public GameObject go_previewPrefab; // 미리보기 프리팹
}


public class CraftManual : MonoBehaviour
{

    // 상태를 나타내는 변수
    private bool isActivated = false;
    private bool isPreviewActivated = false;

    public GameObject player;
    public GameObject prepabManager;

    [SerializeField]
    private GameObject go_BaseUI; // 기본 베이스 UI

    [SerializeField]
    private Craft[] craft_House; // 모닥불용 탭

    private GameObject go_Preview; // 미리보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수

    //RayCast 필요 변수 선언
    private RaycastHit hitinfo;
    [SerializeField]
    //private LayerMask layermask;
    private float range = 1f;
    private Ray ray;

    private Vector3 tr;

    public void SlotClick(int _slotNumber)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        go_Preview = Instantiate(craft_House[_slotNumber].go_previewPrefab, prepabManager.transform);
        go_Prefab = craft_House[_slotNumber].go_Prefab;
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            Window();
        }
        if (isPreviewActivated)
        {
            PreviewPositionUpdate();
        }

        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Build()
    {
        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            tr = go_Preview.transform.position;
            Instantiate(go_Prefab, tr, go_Prefab.transform.rotation);
            Destroy(go_Preview);
            isActivated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
        }
    }

    private void PreviewPositionUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dis = Vector3.Distance(player.transform.position, go_Prefab.transform.position);
        int layerMask = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(ray, out hitinfo, layerMask))
        {
            if (hitinfo.transform.tag == "Terrain")
            {
                Vector3 _location = hitinfo.point;
                go_Preview.transform.position = _location;
            }
        }
    }

    // 초기화
    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPreviewActivated = false;
        go_Preview = null;

        go_BaseUI.SetActive(false);
    }

    private void Window()
    {
        if (isActivated)
        {
            Debug.Log("close");
            CloseWindow();
        }
        else
        {
            Debug.Log("open");
            OpenWindow();
            
        }
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}
