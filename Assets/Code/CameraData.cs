using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "CameraSettingState")]
public class CameraData : ScriptableObject
{
    public float mouseSensitivity = 50f;
    public float yaw = 180f;   // หมุนซ้าย-ขวา (แกน Y)
    public float pitch = 16f;
    public float LimitLeftX = 120f; // X min
    public float LimitRightX = 240f; // X max
    public Camera Cam;
}
