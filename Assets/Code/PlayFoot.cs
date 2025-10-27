using UnityEngine;

public class PlayFoot : MonoBehaviour
{
    public AudioClip footStepSound;
    AudioSource audi;
    public void PlayFootSound()
    {
        audi.PlayOneShot(footStepSound);
    }
    void Start()
    {
        audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
