using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    // �浹�� ������Ʈ�� �ݶ��̴�
    private List<Collider> colliderList = new List<Collider>();

    [SerializeField]
    private int layerGround; // ���� ���̾�
    private const int IGNORE_RAYCAST_LAYER = 2;

    [SerializeField]
    private Material green;
    [SerializeField]
    private Material red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("red");
            SetColor(red);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("green");
            SetColor(green);
        }
            
    }

     private void ChangeColor()
    {
        if (colliderList.Count > 0)
            SetColor(red);
        else
            SetColor(green);
    }

    private void SetColor(Material mat)
    {
        var newMaterials = new Material[transform.GetComponent<MeshRenderer>().materials.Length];

        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = mat;
        }

        transform.GetComponent<MeshRenderer>().materials = newMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
            colliderList.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
            colliderList.Remove(other);
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }

}
