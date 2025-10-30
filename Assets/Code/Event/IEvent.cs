public interface IEvent
{
    void StartEvent();         // เรียกเมื่อเริ่ม Event
    bool IsFinished(); // ✅ เพิ่มตัวนี้
    bool IsPassed();           // คืน true ถ้าผ่าน, false ถ้า fail
    string GetName();          // คืนชื่อของ Event

}