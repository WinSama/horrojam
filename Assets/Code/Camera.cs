using UnityEngine;

public class Camera : MonoBehaviour
{

   [SerializeField] public static float mouseSensitivity = 50f;

    float yaw = 180f;   // หมุนซ้าย-ขวา (แกน Y)
    float pitch = 12f; // หมุนขึ้น-ลง (แกน X)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์ไว้กลางจอ
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // ป้องกันกล้องหมุนเกินแนวตั้ง

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f); // หมุนกล้องอิสระ
    }

}
