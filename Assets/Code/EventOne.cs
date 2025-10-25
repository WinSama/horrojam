using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class EventOne : MonoBehaviour
{
    [Header("Guard Data")]
    public Transform guardpos;
    public Transform guardtarget;
    public GameObject GuardPrefeb;
    private GameObject guard;

    [Header("Sound")]
    public AudioClip GuardPass;
    public AudioSource sound;
    [Header ("Stats")]
   
    public float movespeed;
    

    void Start()
    {
        guard = Instantiate(GuardPrefeb, guardpos.position, guardpos.rotation);
        StartCoroutine(WaitAGuard(0.5f));
        
    }

    void Update()
    {
        if (guard != null)
        {
            guard.transform.position = Vector3.Lerp(
                guard.transform.position,
                guardtarget.position,
                movespeed * Time.deltaTime
            );
            
        }

        
    }

    private IEnumerator WaitAGuard(float sec)
    {
        yield return new WaitForSeconds(sec);
        sound.PlayOneShot(GuardPass);
    }



}
