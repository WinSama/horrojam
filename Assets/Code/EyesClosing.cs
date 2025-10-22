using UnityEngine;
using UnityEngine.UI;
public class EyesClosing : MonoBehaviour
{
    [Header("Eyes")]
    public GameObject RightEye;
    public GameObject LeftEye;
    public GameObject Eyes;

    
    bool isRight = false;
    bool isLeft = false;

    void Start()
    {
        RightEye.SetActive(false);
        LeftEye.SetActive(false);
        Eyes.SetActive(false);
    }


    void Update()
    {

        HoldKey();

    }

    public void HoldKey()
    {
        // ตรวจว่ากดปุ่มไหนอยู่ แล้วเปิดเฉพาะอันนั้น
        if (Input.GetMouseButton(0))
        {
            LeftEye.SetActive(true);
            RightEye.SetActive(false);
            Eyes.SetActive(false);
        }
        else if (Input.GetMouseButton(1))
        {
            RightEye.SetActive(true);
            LeftEye.SetActive(false);
            Eyes.SetActive(false);
        }
        else if (Input.GetMouseButton(2))
        {
            Eyes.SetActive(true);
            LeftEye.SetActive(false);
            RightEye.SetActive(false);
        }
        else
        {
            // ถ้าไม่กดอะไรเลย → ปิดทั้งหมด
            RightEye.SetActive(false);
            LeftEye.SetActive(false);
            Eyes.SetActive(false);
        }


    }
}
