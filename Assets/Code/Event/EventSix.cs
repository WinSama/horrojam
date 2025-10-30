using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Member;
public class EventSix : MonoBehaviour , IEvent
{
    public static EventSix Instance;
    

    private void Awake() => Instance = this;

    [Header("Sound")]
    [SerializeField] public AudioSource AudioS;
    [SerializeField] public AudioClip Ringing;
    [SerializeField] public AudioClip GhostWhisper;

    [Header("Timer")]
    [SerializeField] public float AudioRunTimes = 2f;
    [SerializeField] public float DelayBetweenSounds = 10f;
    [SerializeField] private float EventDuration = 20f; // ระยะเวลา event โดยรวม

    
    bool Pass = false;
    bool Finish = false;
    public void StartEvent()
    {
        Pass = false;
        Finish = false;
        StartCoroutine(ConditionalEvent());
    }


    private IEnumerator ConditionalEvent()
    {
        int playCount = 0;
        float timer = 0f;
        float holdTimeDuringEvent = 0f;

        while (timer < EventDuration)
        {
            // ✅ เล่นเสียงตามจำนวนรอบที่กำหนด
            if (playCount < AudioRunTimes && AudioS != null && Ringing != null)
            {
                AudioS.clip = Ringing;
                AudioS.Play();
                playCount++;
            }

            // ✅ สะสมเวลาหลับตาในช่วง Delay
            float delayTimer = 0f;
            while (delayTimer < DelayBetweenSounds && timer < EventDuration) //Delay Between times
            {
                if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding())
                {
                    holdTimeDuringEvent += Time.deltaTime;
                }

                delayTimer += Time.deltaTime;
                timer += Time.deltaTime;
                yield return null;
            }
        }

        
        if (holdTimeDuringEvent > 6f)
        {
            Debug.Log("✅ EventSix: Pass");
            Pass = true;
            Finish = true;
        }
        else
        {
            Debug.Log("❌ EventSix: Fail");
            AudioS.PlayOneShot(GhostWhisper);
            Pass = false;
            Finish = true;
        }
    }




    public bool IsPassed() => Pass;           // คืน true ถ้าผ่าน, false ถ้า fail
    public string GetName() => "Event Five"    ;          // คืนชื่อของ Event

    public bool IsFinished()=> Finish; // ✅ เพิ่มตัวนี้

}
