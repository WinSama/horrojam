using UnityEngine;
using System.Collections;
public class EventFour : MonoBehaviour ,IEvent
{
    public static EventFour Instance;
    
    bool Pass = false;  
    bool Finish = false;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Sound")]
    [SerializeField] public AudioSource AudioS;
    [SerializeField] public AudioClip WindSFX;
    [SerializeField] public AudioClip GhostSFX;

    [Header("Setting")]
    [SerializeField] public float FogDuration = 12f;

    [Header("Other")]
    [SerializeField] public GameObject ghost;
    [SerializeField] public Transform GhostPos;

    public void StartEvent()
    {
        Pass = false;
        Finish = false;
        StartCoroutine(FogDurationRoutine());
    }



    private IEnumerator FogFade(float startDensity, float endDensity, float Duration)
    {
        float timer = 0;
        while (timer < Duration)
        {
            float t = timer / Duration;
            RenderSettings.fogDensity = Mathf.Lerp(startDensity, endDensity, t);
            timer += Time.deltaTime;
            yield return null;
        }
        RenderSettings.fogDensity = endDensity;
    }

    private IEnumerator FogDurationRoutine()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.white;

        if (AudioS != null && WindSFX != null)
        {
            AudioS.clip = WindSFX;
            AudioS.Play();
        }

        yield return StartCoroutine(FogFade(0f, 0.3f, 4f)); //Fog spawning Fade in

        //check you close your eyes?
        float timer = 0f;
        bool IsPass = false;
        bool IsFail = false;



        while (timer < FogDuration)
        {
            if (EyesClosing.Instance.IsHolding())
            {
                float holdtime = EyesClosing.Instance.GetCurrentHoldTime();
                if (holdtime >= 6 && holdtime <= 8)
                {
                    IsPass = true;
                }
                else if (holdtime > 9)
                {
                    IsFail = true;

                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(FogFade(0.3f, 0f, 4f)); //Fog spawning Fade in


        AudioS.Stop();


        if (IsFail)
        {
            SpawnGhost();
            Debug.Log("❌ Fail: หลับตานานเกินไป");
        }
        else if (IsPass)
        {
            Debug.Log("✅ Pass: หลับตาในช่วงที่กำหนด");
            Pass = true;
            Finish =true;
        }
        else
        {
            SpawnGhost();
            Debug.Log("❌ Fail: ไม่หลับตาเลย");
        }

    }


    public void SpawnGhost()
    {
        GameObject Spawn = Instantiate(ghost, GhostPos.position, GhostPos.rotation);
        AudioS.PlayOneShot(GhostSFX);
        Destroy(Spawn,3f);
        Pass = false;
        Finish = true;

    }
    // เรียกเมื่อเริ่ม Event
    public bool IsPassed() => Pass;           // คืน true ถ้าผ่าน, false ถ้า fail        // คืนชื่อของ Event
    public string GetName() => "EventFour";

    public bool IsFinished()=> Finish; // ✅ เพิ่มตัวนี้

}
