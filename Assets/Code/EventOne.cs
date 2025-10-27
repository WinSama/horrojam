using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using JetBrains.Annotations;
using DG.Tweening;
public class EventOne : MonoBehaviour
{
    public static EventOne Instance; //Used in Event

    private void Awake()
    {
        Instance = this;
    }
    [Header("Checklist")]
    private float StartTime; // count times
    private bool isCheckingRespone; // true current time
    private bool isResponsed; // if player Eyesoff true / no respone
    private bool IstargetPos = false;
    private bool hasLoggedResult = false;


    [Header("Guard Data")]
    public Transform guardpos;
    public Transform guardtarget;
    public GameObject GuardPrefeb;
    private GameObject guard;
    private bool IsEventOne = false;
    public Transform guardfailPos;
    private GameObject guardFail;

    [Header("Sound")]
    public AudioClip GuardPass;
    public AudioSource sound;
    public AudioClip Mobcleaning;
    public AudioClip GuardComing;
    [Header("Stats")]

    public float movespeed;

    public GameObject doorlight;

    Animator guardAnim;

    public void GuardEvent() //Create guard and 
    {

        guard = Instantiate(GuardPrefeb, guardpos.position, guardpos.rotation);
        guardAnim = guard.GetComponentInChildren<Animator>();
        IsEventOne = true;
        StartCoroutine(WaitAGuard(0.7f));

    }


    private void Update()
    {
       // GuardMove();
        CheckToPassEvent();

    }

    private IEnumerator WaitAGuard(float sec)
    {
        yield return new WaitForSeconds(sec);
       // sound.PlayOneShot(GuardPass);



    }

    public void GuardMove()
    {
        if (guard != null && IsEventOne && !IstargetPos)
        {
            guard.transform.position = Vector3.Lerp(
                guard.transform.position,
                guardtarget.position,
                movespeed * Time.deltaTime
            );

            float distance = Vector3.Distance(guard.transform.position, guardtarget.position);
            if (distance < 0.1f)
            {
                IstargetPos = true;
                OnGuardReachedTarget();
                CheckToPassEvent();
            }
        }


    }


    public void OnGuardReachedTarget()
    {
        //condition
        // guard.SetActive(false);
        doorlight.SetActive(true);
        guardAnim.SetTrigger("Goal");

        DG.Tweening.Sequence mySequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        mySequence.Append(guardAnim.transform.DORotate(new Vector3(0, -107, 0), 2));
        mySequence.Append(guardAnim.transform.DOLocalMove(new Vector3(1.17f, -0.1641614f, 4.7f), 2));
        sound.PlayOneShot(Mobcleaning);
        Debug.Log("Start Cleaning");
        StartTime = Time.time;
        isCheckingRespone = true;   // ✅ เริ่มตรวจ
        isResponsed = false;        // ✅ ยังไม่ได้ตอบ

    }

    private void CheckToPassEvent()
    {
        if (isCheckingRespone && !isResponsed && !hasLoggedResult)
        {
            float timeSinceStart = Time.time - StartTime;

            if (EyesClosing.Instance != null && EyesClosing.Instance.EyesOff.activeSelf)
            {
                if (timeSinceStart <= 10f)
                {
                    Debug.Log("pass");
                    doorlight.SetActive(false);
                    guard.SetActive(false);
                }
                else
                {
                    Debug.Log("fail");
                    
                }

                isResponsed = true;
                isCheckingRespone = false;
                hasLoggedResult = true; // ✅ ป้องกัน log ซ้ำ
            }
            else if (timeSinceStart > 15f)
            {
                Debug.Log("fail");
                isResponsed = true;
                isCheckingRespone = false;
                hasLoggedResult = true; // ✅ ป้องกัน log ซ้ำ
                FailCondition();
            }
        }


    }

    public void FailCondition()
    {
        guardFail = Instantiate(GuardPrefeb,guardfailPos.position, guardfailPos.rotation);
        sound.PlayOneShot(GuardComing);
    }


}
