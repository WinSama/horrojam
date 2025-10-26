using UnityEngine;

public class BackGroundSFX : MonoBehaviour
{
    [SerializeField] public AudioSource mainLightNose;
    [SerializeField] public AudioClip[] SFX;

    private void Awake()
    {
        mainLightNose.clip = SFX[0];
        mainLightNose.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
