using UnityEngine;

public class Shifts_System : MonoBehaviour
{
    Shifts_System Instance;
    public int CurrentShift = 0;
    private float ShiftDuration = 120;
    private float BaseShiftDuration = 120;
    public int CurrentValue;
    public float TargetValue = 100;

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

    private void Update()
    {
        if (ShiftDuration <= 0)
        {
            if (CurrentValue >= TargetValue)
            {
                SuccessfulShift();
            }
            else
            {
                //FailedShift();
                SuccessfulShift();
            }
        }
        else
        {
            ShiftDuration -= Time.deltaTime;
        }
    }

    public void FailedShift()
    {
        Debug.Log("failed shift");
    }

    public void SuccessfulShift()
    {
        Order_Spawner Instance = FindObjectOfType<Order_Spawner>();
        CurrentShift++;
        CurrentValue = 0;
        IncreaseDifficulty();
        Instance.ClearCustomers();
        Debug.Log("successful shift");
    }

    private void IncreaseDifficulty()
    {
        ShiftDuration = BaseShiftDuration + (BaseShiftDuration / 10);
        TargetValue = TargetValue * 1.5f;
        Order_Spawner Instance = FindObjectOfType<Order_Spawner>();
        Instance.baseCooldown = Instance.baseCooldown * 0.9f;
    }
}
