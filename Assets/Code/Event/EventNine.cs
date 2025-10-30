using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class EventNine : MonoBehaviour
{
    public static EventNine Instance;
    //--------------Game-Object-----------------
    public GameObject FireStart;
    public GameObject Book;
    public GameObject BookOpen;
    public GameObject BurnRoom;

    private bool IsClosed = false;
    [Header("Audio")]
    public AudioSource AudioS;
    public AudioClip PaperSFX;
    public AudioClip FireSfx;

    [Header("Timer")]
    [SerializeField] private float EventDuration = 10f; // ระยะเวลา event โดยรวม

    private bool isEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartEventNine()
    {
        isEnded = false;
        StartCoroutine(BookEvent());
    }

    private IEnumerator BookEvent()
    {
        Book.SetActive(false);
        BookOpen.SetActive(true);

        float timer = 0f;

        // ✅ เล่นเสียงเปิดหนังสือ
        if (AudioS != null && PaperSFX != null)
        {
            AudioS.clip = PaperSFX;
            AudioS.Play();
        }

        while (timer < EventDuration)
        {
            if (EyesClosing.Instance != null && EyesClosing.Instance.IsHolding())
            {
                float holdTime = EyesClosing.Instance.GetCurrentHoldTime();
                if (holdTime > 2f)
                {
                    Debug.Log("❌ EventNine: FAIL - หลับตาเกิน 2 วิ");
                    EndFailSequence();
                    yield break;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // ✅ ถ้าไม่หลับตาเลย → PASS
        EndPassSequence();
    }

    private void EndPassSequence()
    {
        if (isEnded) return;
        isEnded = true;

        if (AudioS.isPlaying)
            AudioS.Stop();

        BookOpen.SetActive(false);
        Book.SetActive(true);
        FireStart.SetActive(false);

        Debug.Log("✅ EventNine: PASS");
    }

    private void EndFailSequence()
    {
        if (isEnded) return;
        isEnded = true;

        // ✅ หยุดเสียงเปิดหนังสือ
        if (AudioS.isPlaying)
            AudioS.Stop();

        // ✅ เล่นเสียงไฟลุก
        AudioS.PlayOneShot(FireSfx);

        // ✅ เปิดเอฟเฟกต์ไฟ
        FireStart.SetActive(true);
        BurnRoom.SetActive(true);

        // ✅ ปิดหนังสือเปิด
        BookOpen.SetActive(true);
        Book.SetActive(false);

        // ✅ รออีก 10 วิแล้วค่อยปิดไฟ
        StartCoroutine(FireDelay());
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(15f);
        Book.SetActive(true);
        BurnRoom.SetActive(false);
        BookOpen.SetActive(false);
        FireStart.SetActive(false);
        Debug.Log("🔥 EventNine: ไฟดับแล้วหลัง Fail");
    }



}
