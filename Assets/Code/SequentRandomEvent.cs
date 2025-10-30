using UnityEngine;

public class SequentRandomEvent : MonoBehaviour
{



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //startEventOne
        {
            Debug.Log("Event One starts");
            EventOne.Instance.GuardEvent();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Event Two starts");
            EventTwo.Instance.StartEventTwo();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Event Three starts");
            EventThree.Instance.startEventThree();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Event Four starts");
            EventFour.Instance.StartEventFour();

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Event Five starts");
            EventFive.Instance.StartEventFive();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Event Six starts");
            EventSix.Instance.StartEventSix();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("Event Seven Starts");
            EventSeven.Instance.StartEventSeven();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("Event Eight Starts");
            EventEight.Instance.StartEventEight();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Event Nine Starts");
            EventNine.Instance.StartEventNine();
        }

    }
}