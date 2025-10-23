using UnityEngine;

public class Camera : MonoBehaviour
{

   [SerializeField] public static float mouseSensitivity = 50f;

    float yaw = 180f;   // ��ع����-��� (᡹ Y)
    float pitch = 16f;
    [SerializeField] float LimitLeftX = 120f; // X min
    [SerializeField] float LimitRightX = 240f; // X max
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ��͡���������ҧ��
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        yaw += mouseX;
        yaw = Mathf.Clamp(yaw, LimitLeftX, LimitRightX);  

        transform.rotation = Quaternion.Euler(pitch,yaw,0); // ��ع���ͧ�����
    }

}
