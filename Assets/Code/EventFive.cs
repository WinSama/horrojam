using UnityEngine;
using System.Collections;
using UnityEditor;
public class EventFive : MonoBehaviour
{
    public static EventFive Instance;

    [Header("Audio")]
    [SerializeField] public AudioSource[] AudioPos;
    [SerializeField] public AudioClip GirlSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void StartEventFive()
    {
        if (AudioPos != null && GirlSFX != null)
        {
            AudioPos[0].clip = GirlSFX;
            AudioPos[0].Play();
        }
    }


}
