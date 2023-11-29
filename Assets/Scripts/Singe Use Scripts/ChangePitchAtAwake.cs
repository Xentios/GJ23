using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePitchAtAwake : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> punchSounds;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = punchSounds[Random.Range(0, punchSounds.Count)];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
