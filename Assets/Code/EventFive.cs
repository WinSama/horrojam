using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using static Unity.VisualScripting.Member;
using TMPro;
using Unity.VisualScripting;



public class EventFive : MonoBehaviour
{
    public static EventFive Instance;

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
    private bool IsFail;
    private bool IsPass;
    private bool CloseInThisRound;

    private void Awake()
    {
        Instance = this;
    }

    public void StartEventFive()
    {

        StartCoroutine(ChecksoundEvent());
    }


    private IEnumerator ChecksoundEvent()
    {
        float timer = 0f;
        int playCount = 0;


        while (timer < EventDuration && playCount < AudioRunTimes)
        {
            int Index = Random.Range(0, AudioPos.Length);
            AudioSource Source = AudioPos[Index];

            if (Source != null && GirlSFX != null)
            {
                Source.clip = GirlSFX;
                Source.Play();

                if (Index == 0)
                {
                   

                    while (Source.isPlaying)
                    {
                        if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding()) //Close 100percent
                        {
                            float t = EyesClosing.Instance.GetCurrentHoldTime();
                            if (t > 1f && t <= 2f)
                            {
                                CloseInThisRound = true; //Check 1 Round 1 loop
                            }
                            
                        }

                        yield return null;
                    }



                    if (CloseInThisRound)
                    {
                        IsPass = true;
                        Debug.Log("Pass✅");
                    }


                    else //Not close
                    {
                        IsFail = true;
                        Debug.Log("❌ Fail");
                        GameObject Summon = Instantiate(Ghost, GhostPos.position, GhostPos.rotation);
                        Destroy(Summon, 2);
                        GhostSoundPos.PlayOneShot(GhostSound);
                        Debug.Log("❌ EventFive: Fail");
                        yield break;
                        
                    }
                }

            }
            playCount++;
            yield return new WaitForSeconds(DelayBetweenSounds);
            timer += DelayBetweenSounds;

        }

        //   สรุปผลหลังจบ Event
   
        if (IsPass)
        {
            Debug.Log("✅ EventFive: Pass");
        }
        else
        {
            Debug.Log("✅ EventFive: ไม่มีการสุ่ม Audio[0] หรือไม่เข้าเงื่อนไข");
        }


    }



}






