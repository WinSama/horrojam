using UnityEngine;

public class Camera : MonoBehaviour
{

   [SerializeField] public static float mouseSensitivity = 50f;

    float yaw = 180f;   // หมุนซ้าย-ขวา (แกน Y)
    float pitch = 16f;
    [SerializeField] float LimitLeftX = 120f; // X min
    [SerializeField] float LimitRightX = 240f; // X max
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์ไว้กลางจอ
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        yaw += mouseX;
        yaw = Mathf.Clamp(yaw, LimitLeftX, LimitRightX);  

        transform.rotation = Quaternion.Euler(pitch,yaw,0); // หมุนกล้องอิสระ
    }

}
