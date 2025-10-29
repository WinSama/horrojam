using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class EventThree : MonoBehaviour
{
    public static EventThree Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] public Camera Playercam;
    [SerializeField] public Transform DoorPos;
    [Header("Audio")]
    [SerializeField] public AudioSource AudioPos;
    [SerializeField] public AudioClip KnockSFX;
    //----------Bool-------------
    private bool IsLookingDoor = false;
    private bool IsSpeacialEventStart = false;

    [Header("Setting Event")]
    [Range(0, 5)]
    [SerializeField] public float EventDuration = 5f;
    [SerializeField] public float CamAngle = 30f;

    [Header("GameObject")]
    [SerializeField] public GameObject HandPrefeb;
    [SerializeField] public Transform HandPos;

    public void startEventThree()
    {
        StartCoroutine(KnockDoorEvent());
    }

    private IEnumerator KnockDoorEvent()
    {
        IsSpeacialEventStart = false;
        IsLookingDoor = false;

        if (AudioPos != null && KnockSFX != null)
        {
            AudioPos.clip = KnockSFX;
            AudioPos.Play();
            yield return new WaitForSeconds(1f);
            AudioPos.Play();
        }

        float timer = 0f;

        while (timer < EventDuration)
        {
            float angle = Vector3.Angle(Playercam.transform.forward, DoorPos.position - Playercam.transform.position);
            if (angle < CamAngle)
            {
                IsLookingDoor = true;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (IsLookingDoor)
        {
            Debug.Log("Fail");
            IsSpeacialEventStart = true;
            SpeacialEvent();
        }
        else
        {
            Debug.Log("Pass");
        }
    }

    public void SpeacialEvent()
    {
        if (IsSpeacialEventStart)
        {
            GameObject SpawnHand = Instantiate(HandPrefeb, HandPos.position, HandPos.rotation);
            Destroy(SpawnHand, 2);
            
        }
    }

}
