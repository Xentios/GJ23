using Es.InkPainter;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ColorCheker : MonoBehaviour
{
    // Start is called before the first frame update
    public Color targetColor;

    public GameObject targetObject;

    void Start()
    {
        // CalculateColorArea();
    }

    void CalculateColorArea()
    {
        MeshFilter meshFilter = targetObject.GetComponent<MeshFilter>();

        Texture2D targetTexture = ConvertRenderTextureLast(targetObject.GetComponent<MeshRenderer>().material.mainTexture as RenderTexture,128,128);


        int width = targetTexture.width;
        int height = targetTexture.height;
        int matchingPixels = 0;
        // Loop through each pixel in the texture
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Get the color of the current pixel
                Color pixelColor = targetTexture.GetPixel(x, y);

                if (pixelColor == targetColor)
                {
                    matchingPixels++;
                }

            }
        }
        Debugger.Log("Area occupied by target color: " + matchingPixels,Debugger.PriorityLevel.High);
        Debugger.Log("Total Area is : " + (width * height), Debugger.PriorityLevel.High);
        Debugger.Log("Area occupied by target color as %: " + (float) matchingPixels/(width*height), Debugger.PriorityLevel.MustShown);

        //if (meshFilter != null)
        //{
        //    Mesh mesh = meshFilter.mesh;
        //    Vector3[] vertices = mesh.vertices;
        //    int[] triangles = mesh.triangles;

        //    int matchingPixels = 0;

        //    for (int i = 0; i < triangles.Length; i += 3)
        //    {
        //        Color pixelColor = GetAverageColor(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]);

        //        if (pixelColor == targetColor)
        //        {
        //            matchingPixels++;
        //        }
        //    }

        //    Debug.Log("Area occupied by target color: " + matchingPixels);
        //}
        //else
        //{
        //    Debug.LogError("MeshFilter not found.");
        //}
        //SaveRenderTextureToPNG("asdasdsadsa", targetObject.GetComponent<MeshRenderer>().material.mainTexture as RenderTexture);
        //SaveTextureToPNG("Texture2D", targetTexture);
    }

  

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 150, 300), "Check Color"))
        {
            // This code will be executed when the button is clicked
            CalculateColorArea();
        }
    }

    Texture2D TextureToTexture2D(Texture texture)
    {
        // Create a new Texture2D
        Texture2D texture2D = new Texture2D(texture.width, texture.height);

        // Read the pixels from the original texture into the new Texture2D
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = currentActiveRT;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture2D;
    }

    private Texture2D ConvertRenderTexture(RenderTexture renderTexture, int scaledWidth, int scaledHeight)
    {
        // Set the active RenderTexture
        RenderTexture.active = renderTexture;

        // Create a new Texture2D with the specified dimensions
        Texture2D convertedTexture = new Texture2D(scaledWidth, scaledHeight, TextureFormat.RGBA32, false);

        // Read pixels from the active RenderTexture into the new Texture2D
        convertedTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        convertedTexture.Apply();

        // Reset the active RenderTexture to the default render target
        RenderTexture.active = null;

        return convertedTexture;
    }

    private Texture2D ConvertRenderTextureDELETE(RenderTexture renderTexture,int scaledWidth, int scaledHeight)
    {


        //Texture2D newTex = new Texture2D(renderTexture.width, renderTexture.height);

        //RenderTexture currentActiveRT = RenderTexture.active;


        //RenderTexture rt = new RenderTexture(scaledWidth, scaledHeight, 0);
        //rt.filterMode = FilterMode.Bilinear;

        Texture2D newTex = new Texture2D(scaledWidth, scaledHeight);
        RenderTexture.active = renderTexture;
        newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        newTex.Apply();

        return newTex;
    }

    //public void asdsad()
    //{
    //     RenderTexture rt = new RenderTexture(scaledWidth, scaledHeight, 0);
    //        rt.filterMode = FilterMode.Bilinear;

    //        // Set the currently active RenderTexture to the new one
    //        RenderTexture.active = rt;

    //        // Create a new Texture2D and read the pixels from the original texture into it
    //        Texture2D scaledTexture = new Texture2D(scaledWidth, scaledHeight);
    //    Graphics.Blit(originalTexture, rt);
    //        scaledTexture.ReadPixels(new Rect(0, 0, scaledWidth, scaledHeight), 0, 0);
    //        scaledTexture.Apply();

    //        // Reset the active RenderTexture
    //        RenderTexture.active = null;
    //}

    private void SaveRenderTextureToPNG(string textureName, RenderTexture renderTexture)
    {
        string path = UnityEditor.EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
        if (path.Length != 0)
        {
            var newTex = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            newTex.Apply();

            byte[] pngData = newTex.EncodeToPNG();
            if (pngData != null)
            {
                File.WriteAllBytes(path, pngData);
                AssetDatabase.Refresh();
                var importer = AssetImporter.GetAtPath(path) as TextureImporter;
               
            }

            Debug.Log(path);
        }
    }

    private void SaveTextureToPNG(string textureName, Texture2D texture2D)
    {
        string path = UnityEditor.EditorUtility.SaveFilePanel("Save to png", Application.dataPath, textureName + "_painted.png", "png");
        if (path.Length != 0)
        {
            //var newTex = new Texture2D(renderTexture.width, renderTexture.height);
            //RenderTexture.active = renderTexture;
            //newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            //newTex.Apply();

            byte[] pngData = texture2D.EncodeToPNG();
            if (pngData != null)
            {
                File.WriteAllBytes(path, pngData);
                AssetDatabase.Refresh();
                var importer = AssetImporter.GetAtPath(path) as TextureImporter;

            }

            Debug.Log(path);
        }
    }

    private Texture2D ConvertRenderTextureLast(RenderTexture renderTexture, int scaledWidth, int scaledHeight)
    {
        // Create a new RenderTexture for the scaled texture
        RenderTexture scaledRT = new RenderTexture(scaledWidth, scaledHeight, 0);
        scaledRT.filterMode = FilterMode.Bilinear;

        // Set the currently active RenderTexture to the new one
        RenderTexture.active = scaledRT;

        // Use Graphics.Blit to scale the render texture into the new one
        Graphics.Blit(renderTexture, scaledRT);

        // Create a new Texture2D and read pixels from the active RenderTexture
        Texture2D convertedTexture = new Texture2D(scaledWidth, scaledHeight);
        convertedTexture.ReadPixels(new Rect(0, 0, scaledWidth, scaledHeight), 0, 0);
        convertedTexture.Apply();

        // Reset the active RenderTexture to the default render target
        RenderTexture.active = null;

        // Release the temporary render texture
        Destroy(scaledRT);

        return convertedTexture;
    }
}
    
