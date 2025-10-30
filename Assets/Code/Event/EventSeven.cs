using UnityEngine;
using System.Collections;
using Unity.VisualScripting;



public class EventSeven : MonoBehaviour, IEvent
{
    public static EventSeven Instance;

    bool Pass = false;
    bool Finish = false;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Anomaly")]
    [SerializeField] public GameObject PictureAnomaly;
    [SerializeField] public Light[] LightChanging;

    [Header("Sound")]
    [SerializeField] public AudioSource Audios;
    [SerializeField] public AudioClip GlitchSFX;


    [Header("Setting")]
    [SerializeField] public float EventDuration = 20f;
    [SerializeField] public Transform JumpScarePos;
    [SerializeField] public GameObject GhostPrefab;

    private Color[] originalLightColors;
    private bool IsClose = false;
    public void StartEvent()
    {
        Pass = false;
        Finish = false;
        StartCoroutine(HorrorStart());
    }

    private IEnumerator HorrorStart()
    {
        float timer = 0f;
        float holdTimeDuringEvent = 0f;
        bool hasJumped = false;

        // เก็บสีเดิมของไฟ
        originalLightColors = new Color[LightChanging.Length];
        for (int i = 0; i < LightChanging.Length; i++)
        {
            if (LightChanging[i] != null)
            {
                originalLightColors[i] = LightChanging[i].color;
                LightChanging[i].color = Color.red;
            }
        }

        // เปิดภาพผี
        if (PictureAnomaly != null)
        {
            PictureAnomaly.SetActive(true);
        }

        // เล่นเสียง glitch
        if (Audios != null && GlitchSFX != null)
        {
            Audios.PlayOneShot(GlitchSFX);
        }

        // ตรวจหลับตา
        while (timer < EventDuration)
        {
            float t = EyesClosing.Instance.GetCurrentHoldTime();
            if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding())
            {
                IsClose = true;
                holdTimeDuringEvent += Time.deltaTime;

                if (!hasJumped && t > 5)
                {
                    JumpScare();
                    hasJumped = true;

                    // ✅ สรุปผลทันที
                    Debug.Log("❌ EventSeven: Fail (หลับตาเกิน 5 วิ)");

                    // ✅ คืนค่าทุกอย่าง
                    if (PictureAnomaly != null)
                    {
                        PictureAnomaly.SetActive(false);
                    }

                    for (int i = 0; i < LightChanging.Length; i++)
                    {
                        if (LightChanging[i] != null)
                        {
                            LightChanging[i].color = originalLightColors[i];
                        }
                    }

                    Audios.Stop();
                    yield break; // ✅ จบ Event ทันที
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // ✅ สรุปผลเมื่อไม่หลับตาเลย
        Debug.Log("✅ EventSeven: Pass (ไม่หลับตาเลย)");


        // ✅ คืนค่าหลังจบ Event
        if (PictureAnomaly != null)
        {
            PictureAnomaly.SetActive(false);
            Pass = true;
            Finish = true;
        }

        for (int i = 0; i < LightChanging.Length; i++)
        {
            if (LightChanging[i] != null)
            {
                LightChanging[i].color = originalLightColors[i];
            }
        }
    }




    public void JumpScare()
    {
        GameObject J = Instantiate(GhostPrefab, JumpScarePos.position, JumpScarePos.rotation);
        Destroy(J, 2);
        Pass = false;
        Finish = true;
    }

    public bool IsPassed() => Pass;           // คืน true ถ้าผ่าน, false ถ้า fail
    public string GetName() => "EventSeven";          // คืนชื่อของ Event

    public bool IsFinished()=> Finish; // ✅ เพิ่มตัวนี้

}
