using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
#region Editor

#if UNITY_EDITOR

using UnityEditor;

#endif

#endregion

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Animator screenWipe;
    [SerializeField] private AudioMixer mainMixer;

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

        yield return new WaitForSeconds(2f);

        screenWipe.SetTrigger("WipeOut");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#endif

        Application.Quit();
    }
}
