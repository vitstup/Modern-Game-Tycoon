using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShotsScript : MonoBehaviour
{

    // camera pos -27.4  23.03  -27.4       Rotation  30  45  0

    public GameObject[] objects;

    private GameObject currentObj;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        Camera.main.transform.position = new Vector3(-27.4f,  23.03f, - 27.4f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(TakeAndSaveScreenshot());
    }

    IEnumerator TakeAndSaveScreenshot()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            StartCoroutine(Shot(i));

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private IEnumerator Shot(int i)
    {
        currentObj = Instantiate(objects[i]);
        currentObj.transform.position = new Vector3(0, 0, 0);
        currentObj.transform.rotation = Quaternion.identity;

        yield return new WaitForEndOfFrame();

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Sprites/Buildings/ShopImg" + i + ".png", imageBytes);


       Debug.Log("Screenhot " + i + " Done");

        currentObj.SetActive(false);
    }
}