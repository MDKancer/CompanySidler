using Sirenix.OdinInspector;
using UnityEngine;

public class Photokamera : MonoBehaviour
{
    public Camera camera;
    public int width= 32;
    public int height = 32;
    public bool TakeScreenshotnextFrame;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TakeScreenshot();
        }
    }
    [Button]
    private void TakeScreenshot()
    {
        camera.targetTexture = RenderTexture.GetTemporary(width,height);
        TakeScreenshotnextFrame = true;
        RenderTexture texture = camera.targetTexture;
        Texture2D renderResult = new Texture2D(texture.width,texture.height,TextureFormat.ARGB32,false);
        Rect rect = new Rect(0,0,texture.width,texture.height);
        renderResult.ReadPixels(rect,0,0);

        byte[] byteArray = renderResult.EncodeToPNG();
        string url = Application.dataPath+"/CameraScreenshot.png";
        System.IO.File.WriteAllBytes(url,byteArray);
            
        Debug.Log("Saved CameraScreenshot.png");
            
        RenderTexture.ReleaseTemporary(texture);
        camera.targetTexture = null;
        TakeScreenshotnextFrame = false;
    }
}
