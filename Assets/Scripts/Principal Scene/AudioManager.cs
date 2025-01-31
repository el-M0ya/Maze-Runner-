using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    

    public static AudioManager Instance{get; private set;}
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else UnityEngine.Debug.LogError("Mas de un AudioManager");     
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_Audio(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
