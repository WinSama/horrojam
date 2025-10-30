using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.Rendering;
public class EyesClosing : MonoBehaviour
{
    [Header("Eyes")]
    public GameObject EyesOff;
    

    private bool isHolding = false;
    private float holdStartTime = 0f;
    private float lastHoldDuration = 0f;
    private bool wasHoldingKeys = false;

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
        bool keysHeld = HoldKey.instance != null && HoldKey.instance.isHold;

        // ✅ ถ้าปล่อยปุ่มจากที่เคยกด → เรียก Horror
        if (!keysHeld && wasHoldingKeys)
        {
            GameOver.instance.StartHorror();
            EndHold(); // ปิดตาเผื่อหลับอยู่
        }

        // ✅ อัปเดตสถานะล่าสุดไว้ใช้เปรียบเทียบรอบหน้า
        wasHoldingKeys = keysHeld;

        // ✅ ถ้ากดครบ → อนุญาตให้คลิกเพื่อหลับตา
        if (keysHeld)
        {
            HandleClicking();
        }
    }


    private void HandleClicking()
    {
        // เริ่มกดเมาส์
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !isHolding)
        {
            StartHold();
        }

        // ขณะกดค้างเมาส์
        if (isHolding && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            EyesOff.SetActive(true);
        }

        // ปล่อยเมาส์
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            EndHold();
        }
    }

    private void StartHold()
    {
        isHolding = true;
        holdStartTime = Time.time;
        EyesOff.SetActive(true);
    }

    private void EndHold()
    {
        lastHoldDuration = Time.time - holdStartTime;
        isHolding = false;
        EyesOff.SetActive(false);

        Debug.Log($"Eyes held for {lastHoldDuration:F2} seconds");
    }

    public float GetCurrentHoldTime() => isHolding ? Time.time - holdStartTime : 0f;
    public float GetLastHoldDurationExact() => lastHoldDuration;
    public bool IsHolding() => isHolding;



    

}
