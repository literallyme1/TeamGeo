using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class LoadGallery : MonoBehaviour
{
    public RawImage img;
    public GameObject cube;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        //������ �ϳ��� ������ �ٽ� ���� ų �� load ��.
        if (File.Exists(Application.persistentDataPath + "/Image"))
        {
            byte[] fileData = File.ReadAllBytes(Application.persistentDataPath + "/Image");
            /* chat gpt
    
            byte[] fileData = File.ReadAllBytes(Application.persistentDataPath + "/Image");
            // ���� �����͸� Texture2D�� ��ȯ
           Texture2D tex = new Texture2D(0, 0);
           tex.LoadImage(fileData);


            // ť���� Material�� �ؽ�ó�� ����
           cube.GetComponent<Renderer>().material.mainTexture = tex;
            //cube.GetComponentInChildren<RawImage>().texture = tex;
            */

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(fileData);

            // ť���� Material�� �ؽ�ó�� ����
            cube.GetComponentInChildren<RawImage>().texture = tex;
        }
    }

    public void OnClickImageLoad()
    { //video �� ����
        NativeGallery.GetImageFromGallery((file) =>
        {
            //file ���� �ҷ���
            FileInfo selected = new FileInfo(file);

            //�뷮 ���� 50mb
            if (selected.Length > 50000000)
            {
                return;
            }

            //�ҷ�����
            if (!string.IsNullOrEmpty(file))
            {
                StartCoroutine(LoadImage(file));
            }
        });

    }

    IEnumerator LoadImage(string path)
    {
        yield return null;
        //�ڵ����� ������ �� �ٽ� ������ ��. (�̹��� ������ ����)
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/Image";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath); //����Ȱ� ������ ��� ����
        }

        File.WriteAllBytes(savePath + fileName + ".png", fileData); //������ png�� ������ ����

        var temp = File.ReadAllBytes(savePath + fileName + ".png"); //����� �� �о��.

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp); //���� �Լ�, temp�� ����Ʈ���� �ؽ��ķ� ����
        img.texture = tex; //�̹����� ��.
        cube.GetComponentInChildren<RawImage>().texture = tex;
        

        /*
        cube.GetComponent<Renderer>().material.mainTexture = tex;
        //img.SetNativeSize(); //���� ������ �̹����� ǥ�õ�.
        //ImageSizeSetting(img, 1000, 1000);
        */

    }
    /*
    //�̹��� ����� ȭ�鿡 ����
    void ImageSizeSetting(RawImage img, float x, float y) 
    {
        var imgX = img.rectTransform.sizeDelta.x;
        var imgY = img.rectTransform.sizeDelta.y;

        if (x / y > imgX / imgY) //�̹����� ���α��̰� ȭ�麸�� ��ٸ� 
        {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imgX *( y / imgY));
        }
        else 
        {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgY * (x / imgX));
        }
    }
    */
}
