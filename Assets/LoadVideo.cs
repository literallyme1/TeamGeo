using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Video;


public class LoadViedo : MonoBehaviour
{
    public GameObject plane;
//    public RawImage img;
    private VideoPlayer videoPlayer;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        //������ �ϳ��� ������ �ٽ� ���� ų �� load ��.
        if (File.Exists(Application.persistentDataPath + "/Video"))
        {
            byte[] fileData = File.ReadAllBytes(Application.persistentDataPath + "/Video");

        }
    }

    public void OnClickVideoLoad()
    { //video �� ����
        NativeGallery.GetVideoFromGallery((file) =>
        {
            //file ���� �ҷ���
            FileInfo selected = new FileInfo(file);

            //�뷮 ���� 50mb
            if (selected.Length > 400000000)
            {
                return;
            }

            //�ҷ�����
            if (!string.IsNullOrEmpty(file))
            {
                StartCoroutine(LoadV(file));
            }
        });

    }

    IEnumerator LoadV(string path)
    {
        yield return null;
        //�ڵ����� ������ �� �ٽ� ������ ��.
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/Video";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath); //����Ȱ� ������ ��� ����
        }

        File.WriteAllBytes(savePath + fileName + ".mp4", fileData); //������ png�� ������ ����

        videoPlayer = plane.GetComponentInChildren<VideoPlayer>();
        videoPlayer.url = savePath;
        videoPlayer.Prepare();

        videoPlayer.SetDirectAudioMute(0, true); //�Ҹ� mute
        videoPlayer.Play();

 //       img.texture = videoPlayer.targetTexture;
 //       cube.GetComponentInChildren<RawImage>().texture = videoPlayer.targetTexture;


    }

}
