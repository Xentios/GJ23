using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
   
    [SerializeField] private GameObject loadingFinished;
    [SerializeField] private Animator screenWipe;
    [SerializeField] private AudioMixer mainMixer;
    private AsyncOperation asyncLoad;
    private bool fakeFlag;

    public void SetMusicVolume(float sliderValue)
    {
        mainMixer.SetFloat("MainMusic", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSoundsVolume(float sliderValue)
    {
        mainMixer.SetFloat("MainSounds", Mathf.Log10(sliderValue) * 20);
    }

    public void ScreenWipeIn()
    {
        StartCoroutine(ScreenWipe_Coroutine());
    }

    private IEnumerator ScreenWipe_Coroutine()
    {
        screenWipe.SetTrigger("WipeIn");

       // yield return new WaitForSeconds(2f);
        yield return LoadAsyncScene();
        screenWipe.SetTrigger("WipeOut");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.ExitPlaymode();

#endif

        Application.Quit();
    }

    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(0.1f);
        asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
       // asyncLoad.completed += LoadingDone;
        //wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //scene has loaded as much as possible,
            // the last 10% can't be multi-threaded
            if (asyncLoad.progress >= 0.9f)
            {
                LoadingDone();
            }
            yield return null;
        }
        //while (fakeFlag == false)
        //{
        //    yield return null;
        //}
        //asyncLoad.allowSceneActivation = true;
    }

    private void LoadingDone()
    {
       
        loadingFinished.SetActive(true);
        fakeFlag = true;
    }

    [ContextMenu("Allow Loading")]
    public void AllowLevelLoading()
    {
        if (asyncLoad == null) return;
        asyncLoad.allowSceneActivation = true;       
    }
}
