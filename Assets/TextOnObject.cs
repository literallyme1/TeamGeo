using TMPro;
using UnityEngine;

public class TextOnObject : MonoBehaviour
{
    public GameObject cube;
    //private TextMeshPro textMeshPro; //���⼭�� ���
    void Start()
    {
        //TextMeshPro textMeshPro = cube.AddComponent<TextMeshPro>(); //textmeshpro�� �������
        //TextMeshPro textMeshPro = new GameObject("NameText").AddComponent<TextMeshPro>(); // ���� text object ����
        //textMeshPro.text = "that's gonna be your name";

        //textMeshPro.fontSize = 10;
       // textMeshPro.color = Color.black;

       // textMeshPro.transform.parent = cube.transform; //text�� �θ� cube�� ����

        //UpdateFontSize();

        //float cubeSize = Mathf.Max(cube.transform.localScale.x, cube.transform.localScale.y, cube.transform.localScale.z);//cube�� x,y,z �� �� ����
       // textMeshPro.fontSize = Mathf.RoundToInt(cubeSize * 0.2f);// �� ������ 1/5�� ��Ʈ ����� ��.

      //  textMeshPro.transform.localPosition = Vector3.up * cube.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
       // UpdateFontSize();
    }
    /*
    void UpdateFontSize()
    {
        float cubeSize = Mathf.Max(cube.transform.localScale.x, cube.transform.localScale.y, cube.transform.localScale.z);//cube�� x,y,z �� �� ����
        this.textMeshPro.fontSize = Mathf.RoundToInt(cubeSize * 0.2f);// �� ������ 1/5�� ��Ʈ ����� ��.
    }*/
}
