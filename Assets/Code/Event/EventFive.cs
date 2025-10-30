using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using static Unity.VisualScripting.Member;
using TMPro;
using Unity.VisualScripting;




public class EventFive : MonoBehaviour , IEvent
{
    public static EventFive Instance;
    bool Pass = false;
    bool Finish = false;    
    //GameObject
    public GameObject Ghost;
    public Transform GhostPos;

    [Header("Audio")]
    [SerializeField] public AudioSource[] AudioPos;
    [SerializeField] public AudioClip GirlSFX;

    //------------------------------------------------

    [SerializeField] public AudioSource GhostSoundPos;
    [SerializeField] public AudioClip GhostSound;

    [Header("Duration")]
    [SerializeField] public float EventDuration;
    [SerializeField] public float AudioRunTimes;
    [SerializeField] public float DelayBetweenSounds;
    //Bool Conditional
    

    private void Awake()
    {
        Instance = this;
    }

    public void StartEvent()
    {
        Pass = false;
        Finish = false;
        StartCoroutine(ChecksoundEvent());
    }


    private IEnumerator ChecksoundEvent()
    {
        float timer = 0f;
        int playCount = 0;

        while (timer < EventDuration && playCount < AudioRunTimes)
        {
            int Index = Random.Range(0, AudioPos.Length);
            AudioSource source = AudioPos[Index];

            if (source != null && GirlSFX != null)
            {
                source.clip = GirlSFX;
                source.Play();

                if (Index == 0)
                {
                    bool closedInTime = false;
                    float checkTimer = 0f;

                    // ✅ ตรวจแค่ช่วง 2 วินาทีแรก
                    while (checkTimer < 3.5f)
                    {
                        if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding())
                        {
                            float t = EyesClosing.Instance.GetCurrentHoldTime();
                            if (t > 1f && t <= 2f)
                            {
                                closedInTime = true;
                                break;
                            }
                        }

                        checkTimer += Time.deltaTime;
                        yield return null;
                    }

                    // ✅ ถ้าไม่หลับตาในช่วง 1–2 วิ → Fail ทันที
                    if (!closedInTime)
                    {
                        Debug.Log("❌ Fail: ไม่หลับตาในช่วงเสียง Audio[0]");
                        GameObject summon = Instantiate(Ghost, GhostPos.position, GhostPos.rotation);
                        Destroy(summon, 2f);
                        GhostSoundPos.PlayOneShot(GhostSound);
                        Pass = false;
                        Finish = true;
                        yield break;
                        
                    }
                    else
                    {
                        Debug.Log("✅ Pass: หลับตาในช่วงเสียง Audio[0]");
                    }
                }
            }

            playCount++;
            timer += DelayBetweenSounds;
            yield return new WaitForSeconds(DelayBetweenSounds);
        }

        // ✅ สรุปผลหลังจบ Event
        Debug.Log("✅ EventFive: จบโดยไม่มี Fail");
        Pass = true;
        Finish = true;
    }


    public bool IsPassed() => Pass;           // คืน true ถ้าผ่าน, false ถ้า fail
    public string GetName() => "Event Five";          // คืนชื่อของ Event
    public bool IsFinished()=> Finish; // ✅ เพิ่มตัวนี้

}





