using UnityEngine;

public class Shifts_System : MonoBehaviour
{
    Shifts_System Instance;
    public int CurrentShift = 0;
    public int CurrentValue;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
