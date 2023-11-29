using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClipBasedOnScore : MonoBehaviour
{
    [SerializeField]
    private AudioClip VictorySound;
    [SerializeField]
    private AudioClip LoseSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckWinCondition()
    {
        bool win = false;
        if (GameManager.Instance.currentShopResult.CutPercentage > 95f) win = true;
        if (GameManager.Instance.currentShopResult.PressPercentage > 95f) win = true;
        if (GameManager.Instance.currentShopResult.PaintPercentage > 95f) win = true;
        if (GameManager.Instance.currentShopResult.SpikePercentage > 95f) win = true;
        if (GameManager.Instance.currentShopResult.HammerPercentage >95f) win = true;
        var total = GameManager.Instance.currentShopResult.CutPercentage +
                    GameManager.Instance.currentShopResult.PressPercentage +
                    GameManager.Instance.currentShopResult.PaintPercentage +
                    GameManager.Instance.currentShopResult.SpikePercentage +
                    GameManager.Instance.currentShopResult.HammerPercentage;

        total /= 5;
        if (total > 80f) win = true;

        if (win == true)
        {
            audioSource.clip = VictorySound;
            audioSource.volume = 1f;
            audioSource.pitch = 0.95f;
        }
        else 
        {
            audioSource.clip = LoseSound;
            audioSource.volume = 0.3f;
            audioSource.pitch = 1.6f;
        }
       

    }
}
