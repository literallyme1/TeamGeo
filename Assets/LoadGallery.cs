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
        //파일이 하나라도 있으면 다시 앱을 킬 때 load 됨.
        if (File.Exists(Application.persistentDataPath + "/Image"))
        {
            byte[] fileData = File.ReadAllBytes(Application.persistentDataPath + "/Image");
            /* chat gpt
    
            byte[] fileData = File.ReadAllBytes(Application.persistentDataPath + "/Image");
            // 파일 데이터를 Texture2D로 변환
           Texture2D tex = new Texture2D(0, 0);
           tex.LoadImage(fileData);


            // 큐브의 Material에 텍스처를 적용
           cube.GetComponent<Renderer>().material.mainTexture = tex;
            //cube.GetComponentInChildren<RawImage>().texture = tex;
            */

            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(fileData);

            // 큐브의 Material에 텍스처를 적용
            cube.GetComponentInChildren<RawImage>().texture = tex;
        }
    }

    public void OnClickImageLoad()
    { //video 도 가능
        NativeGallery.GetImageFromGallery((file) =>
        {
            //file 정보 불러옴
            FileInfo selected = new FileInfo(file);

            //용량 제한 50mb
            if (selected.Length > 50000000)
            {
                return;
            }

            //불러오기
            if (!string.IsNullOrEmpty(file))
            {
                StartCoroutine(LoadImage(file));
            }
        });

    }

    IEnumerator LoadImage(string path)
    {
        yield return null;
        //핸드폰에 저장한 후 다시 가지고 옴. (이미지 데이터 저장)
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/Image";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath); //저장된게 없으면 경로 생성
        }

        File.WriteAllBytes(savePath + fileName + ".png", fileData); //무조건 png로 데이터 저장

        var temp = File.ReadAllBytes(savePath + fileName + ".png"); //저장된 거 읽어옴.

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp); //제공 함수, temp를 바이트에서 텍스쳐로 변경
        img.texture = tex; //이미지가 들어감.
        cube.GetComponentInChildren<RawImage>().texture = tex;
        

        /*
        cube.GetComponent<Renderer>().material.mainTexture = tex;
        //img.SetNativeSize(); //원본 사이즈 이미지가 표시됨.
        //ImageSizeSetting(img, 1000, 1000);
        */

    }
    /*
    //이미지 사이즈를 화면에 맞춤
    void ImageSizeSetting(RawImage img, float x, float y) 
    {
        var imgX = img.rectTransform.sizeDelta.x;
        var imgY = img.rectTransform.sizeDelta.y;

        if (x / y > imgX / imgY) //이미지의 세로길이가 화면보다 길다면 
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
