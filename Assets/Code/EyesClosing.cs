using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class EyesClosing : MonoBehaviour
{
    [Header("Eyes")]
    public GameObject EyesOff;

    private bool isHolding = false;
    private float holdStartTime = 0f;
    private float lastHoldDuration = 0f;

    public static EyesClosing Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        EyesOff.SetActive(false);

    }


    void Update()
    {
        // เริ่มกด
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            StartHold();
        }

        // ขณะกดค้าง
        if (isHolding && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            EyesOff.SetActive(true);
            // ไม่ต้องอัปเดต UI จึงไม่มีโค้ดเพิ่มเติมที่นี่
        }

        // ปล่อย
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            EndHold();
        }


    }

    private void StartHold()
    {
        if (!isHolding) //Used to stop Update Process
        {
            isHolding = true;
            holdStartTime = Time.time;
            EyesOff.SetActive(true);
        }
    }

    private void EndHold()
    {
        if (!isHolding) return;

        lastHoldDuration = Time.time - holdStartTime;
        isHolding = false;

        EyesOff.SetActive(false);

        
        Debug.Log($"Eyes held for {lastHoldDuration:F2} seconds");
    }

    // เวลากดค้างอยู่ตอนนี้ (เรียลไทม์)
    public float GetCurrentHoldTime()
    {
        return isHolding ? Time.time - holdStartTime : 0f;
    }

    // เวลากดครั้งล่าสุดหลังปล่อย
    public float GetLastHoldDurationExact()
    {
        return lastHoldDuration;
    }


    // เรียกเพื่อตรวจสถานะว่ากำลังกดค้างอยู่หรือไม่
    public bool IsHolding() //
    {
        return isHolding;
    }

}
