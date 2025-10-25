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
    [Header("Timer")]
    private float StartTime; // count times
    private bool isCheckingRespone; // true current time
    private bool isResponsed; // if player Eyesoff true / no respone
    
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
        StartCoroutine(WaitAGuard(0.7f));
    
    }


    private void Update()
    {

        GuardMove();
        if (isCheckingRespone && !isResponsed)
        {
            float timeSinceSound = Time.time - StartTime;

            // ✅ ถ้าหลับตาทัน → pass
            if (EyesClosing.Instance != null && EyesClosing.Instance.EyesOff.activeSelf)
            {
                if (timeSinceSound <= 4f)
                {
                    Debug.Log("pass");
                }
                else
                {
                    Debug.Log("fail");
                }

                isResponsed = true;
                isCheckingRespone = false;
            }

            // ✅ ถ้าเกินเวลาแล้วไม่หลับตา → fail อัตโนมัติ
            else if (timeSinceSound > 4f)
            {
                Debug.Log("fail");
                isResponsed = true;
                isCheckingRespone = false;
            }
        }



    }

    private IEnumerator WaitAGuard(float sec)
    {
        yield return new WaitForSeconds(sec);
        sound.PlayOneShot(GuardPass);
        StartTime = Time.time;
        isCheckingRespone = true;
        isResponsed = false;

        yield return new WaitForSeconds(sec);
        guard.SetActive(false);
    }

    public void GuardMove()
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

}
