using UnityEngine;
using System.Collections;
using System.Threading;
public class EventEight : MonoBehaviour, IEvent
{
    public static EventEight Instance;

    bool Pass = false;
    bool Finish = false;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Setting")]
    [SerializeField] public float EventDuration;
    [SerializeField] public Transform Leftpos;
    [SerializeField] public Camera cam;
    [SerializeField] public float CamAngle = 30f;

    private bool IsNoticeSound = false;
    private bool isEnd = false;
    [Header("Sound")]
    [SerializeField] public AudioClip Whisper;
    [SerializeField] public AudioSource AudioS;
    [SerializeField] public AudioClip BreathSFX;

    public void StartEvent()
    {
        Pass = false;
        Finish = false;
        StartCoroutine(EventStart());
    }


    private IEnumerator EventStart()
    {
        float timer = 0f;
        if (AudioS != null && BreathSFX != null)
        {
            AudioS.clip = BreathSFX;
            AudioS.Play();

        }

        while (timer < EventDuration)
        {
            float t = EyesClosing.Instance.GetCurrentHoldTime();
            // ✅ ตรวจหลับตา → Fail ทันที
            if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding() && t >= 3)
            {

                Debug.Log("❌ EventEight: Fail - หลับตา");
                EndEvent(false);
                yield break;

            }

            // ✅ ตรวจมุมกล้อง
            float angle = Vector3.Angle(cam.transform.forward, Leftpos.position - cam.transform.position);
            if (angle < CamAngle)
            {
                Debug.Log("✅ EventEight: Pass - หันทัน");
                AudioS.Stop();
                EndEvent(true);
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // ✅ ถ้าเลยเวลาแล้วยังไม่หัน → Fail
        Debug.Log("❌ EventEight: Fail - หมดเวลา");
        EndEvent(false);




    }

    private void EndEvent(bool isPass)
    {
        if (isEnd) return;
        isEnd = true;

        // ✅ หยุดเสียง
        if (AudioS != null && AudioS.isPlaying)
        {
            AudioS.Stop();
        }

        // ✅ สรุปผล
        if (isPass)
        {
            Debug.Log("🎉 EventEight: PASS");
            Pass = true;
            Finish = true;
        }
        else
        {
            Debug.Log("💀 EventEight: FAIL");
            AudioS.PlayOneShot(Whisper);
            Pass = false;
            Finish = true;
        }

        // ✅ เพิ่มเติม: คืนค่า, ปิดภาพ, ปิดไฟ ฯลฯ ได้ตรงนี้
    }

    public bool IsPassed() => Pass;           // คืน true ถ้าผ่าน, false ถ้า fail
    public string GetName() => "EventEight";          // คืนชื่อของ Event

    public bool IsFinished()=> Finish; // ✅ เพิ่มตัวนี้

}
