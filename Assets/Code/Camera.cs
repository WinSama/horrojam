using UnityEngine;

public class Camera : MonoBehaviour
{
    public CameraData Data;
    void Start()
    {
        Data.Cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์ไว้กลางจอ
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Data.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Data.mouseSensitivity * Time.deltaTime;
        
        Data.yaw += mouseX;
        Data.yaw = Mathf.Clamp(Data.yaw, Data.LimitLeftX, Data.LimitRightX);  

        transform.rotation = Quaternion.Euler(Data.pitch,Data.yaw,0); // หมุนกล้องอิสระ
    }

}
