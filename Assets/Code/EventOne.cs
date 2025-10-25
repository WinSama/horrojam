using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class EventOne : MonoBehaviour
{
    public static EventOne Instance; //Used in Event

    private void Awake()
    {
        Instance = this;
    }

    [Header("Guard Data")]
    public Transform guardpos;
    public Transform guardtarget;
    public GameObject GuardPrefeb;
    private GameObject guard;
    private bool IsEventOne = false;

    [Header("Sound")]
    public AudioClip GuardPass;
    public AudioSource sound;
    [Header ("Stats")]
   
    public float movespeed;

    public void GuardEvent() //Create guard and 
    {

        guard = Instantiate(GuardPrefeb, guardpos.position, guardpos.rotation);
        IsEventOne = true;
        StartCoroutine(WaitAGuard(0.8f));

    }


    private void Update()
    {
        if (guard != null && IsEventOne) //GuardMove
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
        guard.SetActive(false);
    }



}
