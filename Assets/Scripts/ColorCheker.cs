
using UnityEngine;

public class ColorCheker : MonoBehaviour
{
     
    [SerializeField]
    private bool saveTextureForDebugging;

    public float CalculateColorArea(MeshRenderer meshRenderer, Color targetColor)
    {
        Texture2D targetTexture = ConvertRenderTextureWithTemporary(meshRenderer.material.mainTexture as RenderTexture, 128, 128);
        int width = targetTexture.width;
        int height = targetTexture.height;
        int matchingPixels = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = targetTexture.GetPixel(x, y);
                if (AreColorsCloseRGBA(pixelColor, targetColor, 0.05f))
                {
                    matchingPixels++;
                }
            }
        }
        var result = (float) matchingPixels / (width * height);
        Debugger.Log("Area occupied by target color: " + matchingPixels, Debugger.PriorityLevel.High);
        Debugger.Log("Total Area is : " + (width * height), Debugger.PriorityLevel.High);
        Debugger.Log("Area occupied by target color as %: " + (result).ToString("F5"), Debugger.PriorityLevel.MustShown);
        if (saveTextureForDebugging == true)
        {
            SaveTextureToPNG("Texture2D", targetTexture);
        }
        return result;
    }
    private void CalculateColorArea()
    {
        //Texture2D targetTexture = ConvertRenderTextureWithTemporary(targetObject.GetComponent<MeshRenderer>().material.mainTexture as RenderTexture,128,128);

        //var targetColor = shopRequest.Color;
        //int width = targetTexture.width;
        //int height = targetTexture.height;
        //int matchingPixels = 0;

        //for (int y = 0; y < height; y++)
        //{
        //    for (int x = 0; x < width; x++)
        //    {
        //        Color pixelColor = targetTexture.GetPixel(x, y);              
        //      if(AreColorsCloseRGBA(pixelColor,targetColor,0.05f))
        //        {
        //            matchingPixels++;
        //        }
        //    }
        //}
        //Debugger.Log("Area occupied by target color: " + matchingPixels,Debugger.PriorityLevel.High);
        //Debugger.Log("Total Area is : " + (width * height), Debugger.PriorityLevel.High);
        //Debugger.Log("Area occupied by target color as %: " + ((float) matchingPixels/(width*height)).ToString("F5"), Debugger.PriorityLevel.MustShown);


        //if (saveTextureForDebugging == true)
        //{
        //    SaveTextureToPNG("Texture2D", targetTexture);
        //}
    }

  

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(100, 100, 150, 300), "Check Color"))
    //    {           
    //        CalculateColorArea();
    //    }
    //}

    private void SaveTextureToPNG(string textureName, Texture2D texture2D)
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
        if (path.Length != 0)
        {
            byte[] pngData = texture2D.EncodeToPNG();
            if (pngData != null)
            {
                System.IO.File.WriteAllBytes(path, pngData);
                UnityEditor.AssetDatabase.Refresh();
                var importer = UnityEditor.AssetImporter.GetAtPath(path) as UnityEditor.TextureImporter;
            }
            Debug.Log(path);
        }
#endif
    }

  

    private Texture2D ConvertRenderTexture(RenderTexture renderTexture, int scaledWidth, int scaledHeight)
    {
        var activeRT = RenderTexture.active;
        
        RenderTexture scaledRT = new RenderTexture(scaledWidth, scaledHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        scaledRT.filterMode = FilterMode.Bilinear;

  
        RenderTexture.active = scaledRT; 
        Graphics.Blit(renderTexture, scaledRT);
      
        Texture2D convertedTexture = new Texture2D(scaledWidth, scaledHeight);
        convertedTexture.ReadPixels(new Rect(0, 0, scaledWidth, scaledHeight), 0, 0);
        convertedTexture.Apply();
   
        RenderTexture.active = activeRT;
        
        Destroy(scaledRT);

        return convertedTexture;
    }

    private Texture2D ConvertRenderTextureWithTemporary(RenderTexture renderTexture, int scaledWidth, int scaledHeight)
    {
        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture scaledRT = RenderTexture.GetTemporary(scaledWidth, scaledHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        scaledRT.filterMode = FilterMode.Bilinear;


       
        Graphics.Blit(renderTexture, scaledRT);
        RenderTexture.active = scaledRT;

        Texture2D convertedTexture = new Texture2D(scaledWidth, scaledHeight);
        convertedTexture.ReadPixels(new Rect(0, 0, scaledWidth, scaledHeight), 0, 0);
        convertedTexture.Apply();

        RenderTexture.active = currentActiveRT;
        RenderTexture.ReleaseTemporary(scaledRT);

        return convertedTexture;

 
    }

    public  bool AreColorsCloseRGBA(Color color1, Color color2,float ColorTolerance)
    {
        // Check the absolute difference for each RGB component
        float redDiff = Mathf.Abs(color1.r - color2.r);
        float greenDiff = Mathf.Abs(color1.g - color2.g);
        float blueDiff = Mathf.Abs(color1.b - color2.b);
        float alphaDiff = Mathf.Abs(color1.a - color2.a);

        // If all differences are within tolerance, colors are considered close
        return redDiff < ColorTolerance && greenDiff < ColorTolerance && blueDiff < ColorTolerance && alphaDiff<ColorTolerance;
    }

    public  float CalculateColorDifference(Color color1, Color color2)
    {
        float redDiff = color1.r - color2.r;
        float greenDiff = color1.g - color2.g;
        float blueDiff = color1.b - color2.b;

        // Calculate Euclidean distance
        float colorDifference = Mathf.Sqrt(redDiff * redDiff + greenDiff * greenDiff + blueDiff * blueDiff);

        return colorDifference;
    }
}
    
