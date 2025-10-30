using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem.XR;
public class EventTwo : MonoBehaviour, IEvent
{
    
    public static EventTwo Instance;
    private bool allOffTriggered = false;

    private void Awake() => Instance = this;

    [Header("Light process")]
    [SerializeField] public GameObject[] Lightbulb;

    [Header("Sound")]
    [SerializeField] public AudioSource AudioS;
    [SerializeField] public AudioClip lightsound;
    [SerializeField] public AudioClip finalSound;

    [Header("Blink Settings")]
    [SerializeField] private int blinkCount = 10;
    [SerializeField] private float minInterval = 0.2f;
    [SerializeField] private float maxInterval = 0.9f;

    [Header("Timer")]
    private float eventStartTime;
    [SerializeField] private float eventDuration = 8f; // ระยะเวลา event โดยรวม


    bool Pass = false;
    bool Finish = false;
    public void StartEvent()
    {
        Pass = false;
        Finish = false;
        allOffTriggered = false;
        eventStartTime = Time.time;
        StartCoroutine(ConditionalEvent());

        foreach (GameObject bulb in Lightbulb)
        {
            StartCoroutine(BlinkAndCheckResponse(bulb));
        }

    }

    private IEnumerator BlinkAndCheckResponse(GameObject bulb)
    {
        // Run sound and check null
        if (lightsound != null && AudioS != null)
        {
            AudioS.clip = lightsound;
            AudioS.loop = false;
            AudioS.Play();
        }

        // ✅ Blinking
        for (int i = 0; i < blinkCount; i++)
        {

            if (allOffTriggered)
            {
                AudioS.Stop(); // ✅ หยุดเสียงทันทีถ้า fail แล้ว
                yield break;
            }

            bulb.SetActive(!bulb.activeSelf);
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);

        }

        AudioS.Stop(); // ✅ เพิ่มตรงนี้เพื่อหยุดเสียงเมื่อกระพริบจบ



    }

    private IEnumerator ConditionalEvent()
    {
        while (Time.time - eventStartTime < eventDuration)
        {
            if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding())
            {
                float holdTime = EyesClosing.Instance.GetCurrentHoldTime();
                if (!allOffTriggered && holdTime > 2f)
                {
                    allOffTriggered = true;
                    TurnOffAllLight();
                    Debug.Log("Fail ❌");
                    Pass = false;
                    Finish = true;  
                    yield break;
                }
            }

            yield return null;
        }

        // ✅ ถ้าออกจาก loop โดยไม่ fail → ถือว่าผ่าน
        if (!allOffTriggered)
        {
            TurnOnAllLight();
            Debug.Log("Pass ✅");
            Pass = true;
            Finish = true;
        }


    }


    public void TurnOffAllLight()
    {
        foreach (GameObject bulb in Lightbulb)
        {
            bulb?.SetActive(false);
        }


    }

    public void TurnOnAllLight()
    {
        foreach (GameObject bulb in Lightbulb)
        {
            bulb?.SetActive(true);
        }


    }

    
   public bool IsPassed() => Pass;           // คืน true ถ้าผ่าน, false ถ้า fail
    public string GetName() => "Event Two";          // คืนชื่อของ Event
   public bool IsFinished()=> Finish; // ✅ เพิ่มตัวนี้

}
