using UnityEngine;

public class ReachGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.CompareTag("Goal"))
        {
            EventOne.Instance.OnGuardReachedTarget();
        }
    }
}
