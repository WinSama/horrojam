using UnityEngine;

public class Camera : MonoBehaviour
{

   [SerializeField] public static float mouseSensitivity = 50f;

    float yaw = 180f;   // ��ع����-��� (᡹ Y)
    float pitch = 12f; // ��ع���-ŧ (᡹ X)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ��͡���������ҧ��
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // ��ͧ�ѹ���ͧ��ع�Թ�ǵ��

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f); // ��ع���ͧ�����
    }

}
