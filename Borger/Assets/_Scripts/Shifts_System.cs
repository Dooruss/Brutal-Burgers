using UnityEngine;

public class Shifts_System : MonoBehaviour
{
    Shifts_System Instance;
    public int CurrentShift = 0;
    private float ShiftDuration = 120;
    public int CurrentValue;
    public int TargetValue;

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
                FailedShift();
            }
        }
        else
        {
            ShiftDuration -= Time.deltaTime;
        }
    }

    private void FailedShift()
    {

    }

    private void SuccessfulShift()
    {
        CurrentShift++;
        ShiftDuration = ShiftDuration - (ShiftDuration/10);
    }
}
