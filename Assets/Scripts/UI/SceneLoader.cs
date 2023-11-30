using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{    
    [SerializeField]
    private SceneField sceneField;
    
    // Start is called before the first frame update
   public void LoadScene()
    {         
        SceneManager.LoadScene(sceneField.SceneName);
    }
      
}
