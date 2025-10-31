using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SequentRandomEvent : MonoBehaviour
{
    public GameObject continuePanel; // ✅ Panel ที่มีปุ่ม
    public Button continueButton;
    public Button stopButton;
    private bool isGameOver = false; // ✅ เพิ่มตัวแปรนี้

    private bool waitingForPlayerChoice = false;


    public List<IEvent> allEvents = new List<IEvent>();
    private Queue<IEvent> eventQueue = new Queue<IEvent>();
    private IEvent currentEvent = null;
    private int completedCount = 0;

    public static SequentRandomEvent Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        allEvents.Add(EventOne.Instance);
        allEvents.Add(EventTwo.Instance);
        allEvents.Add(EventThree.Instance);
        allEvents.Add(EventFour.Instance);
        allEvents.Add(EventFive.Instance);
        allEvents.Add(EventSix.Instance);
        allEvents.Add(EventSeven.Instance);
        allEvents.Add(EventEight.Instance);
        allEvents.Add(EventNine.Instance);

        ShuffleAndSelectFour();
    }

    void Update()
    {
        if (waitingForPlayerChoice || isGameOver) return;

        if (currentEvent == null)
        {
            if (HoldKey.instance != null && HoldKey.instance.isHold)
            {
                TryStartNextEvent();
            }
        }
        else if (currentEvent.IsFinished())
        {
            if (currentEvent.IsPassed())
            {
                Debug.Log("✅ Event passed");
                currentEvent = null;
                completedCount++;

                if (completedCount < 4)
                {
                    ShowContinueChoice();
                    return;
                }

                Debug.Log("🎉 Clear Game!");
                // เพิ่มระบบจบเกมตรงนี้ได้เลย
            }
            else
            {
                Debug.Log("❌ Event failed. Game Over!");
                currentEvent = null;
                isGameOver = true;
                GameOver.instance.FailGame(); // ✅ ใช้แค่ระบบนี้
            }
        }
    }


    void TryStartNextEvent()
    {
        if (eventQueue.Count > 0)
        {
            currentEvent = eventQueue.Dequeue();
            currentEvent.StartEvent();
            Debug.Log($"Starting event: {currentEvent.GetName()}");
        }
    }

    void ShuffleAndSelectFour()
    {
        List<IEvent> temp = new List<IEvent>(allEvents);
        for (int i = 0; i < 4 && temp.Count > 0; i++)
        {
            int index = Random.Range(0, temp.Count);
            eventQueue.Enqueue(temp[index]);
            temp.RemoveAt(index);
        }
    }

    void ShowContinueChoice()
    {
        waitingForPlayerChoice = true;
        continuePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        continueButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(true);

        continueButton.onClick.RemoveAllListeners();
        stopButton.onClick.RemoveAllListeners();

        continueButton.onClick.AddListener(() =>
        {
            continuePanel.SetActive(false);
            waitingForPlayerChoice = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            TryStartNextEvent();
        });

        stopButton.onClick.AddListener(() =>
        {
            continuePanel.SetActive(false);
            waitingForPlayerChoice = false;
            isGameOver = false; // ✅ รีเซ็ตสถานะ

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    void ShowLosePanel()
    {
        waitingForPlayerChoice = true;
        continuePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        continueButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);

        stopButton.onClick.RemoveAllListeners();
        stopButton.onClick.AddListener(() =>
        {
            continuePanel.SetActive(false);
            waitingForPlayerChoice = false;
            isGameOver = false; // ✅ รีเซ็ตสถานะ

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }


}
