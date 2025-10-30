using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;
public class HoldKey : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public CanvasGroup HowtoplayUI;

    public bool isHold = false;
    private bool hasFadedOut = false;

    public static HoldKey instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        HowtoplayUI.alpha = 0;
        StartCoroutine(Fade(HowtoplayUI, 1, 1)); // fade-in
    }

    void Update()
    {
        // ✅ เช็คปุ่มทุกเฟรม
        isHold = Input.GetKey(KeyCode.A) &&
                 Input.GetKey(KeyCode.S) &&
                 Input.GetKey(KeyCode.K) &&
                 Input.GetKey(KeyCode.L);

        // ✅ ถ้ายังไม่ fade-out และกดครบ → fade-out
        if (isHold && !hasFadedOut)
        {
            hasFadedOut = true;
            StartCoroutine(Fade(HowtoplayUI, 0, 0.5f));
        }
    }

    IEnumerator Fade(CanvasGroup cg, float target, float duration)
    {
        float start = cg.alpha;
        float t = 0;
        while (t < duration)
        {
            cg.alpha = Mathf.Lerp(start, target, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        cg.alpha = target;
    }

    
}
