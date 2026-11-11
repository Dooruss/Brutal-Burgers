using System.Collections.Generic;
using UnityEngine;

public class Order_Spawner : MonoBehaviour
{
    [Header("Spawning Information")]
    [SerializeField] private List<GameObject> CustomerModels = new List<GameObject>();
    public List<GameObject> CurrentCustomers = new List<GameObject>();
    [SerializeField] private GameObject Canvas;

    [Header("Cooldowns & max")]
    [SerializeField] private int MaxCustomers = 3;
    [SerializeField] float baseCooldown = 10;
    private float SpawnCooldown = 1f;
    private float CooldownMultiplier = 1;


    void Update()
    {
        SpawnCooldown -= Time.deltaTime;
        if (SpawnCooldown <= 0 && CurrentCustomers.Count != MaxCustomers) { SpawnCustomer(); }
        RemoveExCustomers();
        checkOverlap();
    }

    void SpawnCustomer()
    {
        int TheCustomerIndex;
        TheCustomerIndex = Random.Range(0, CustomerModels.Count);
        GameObject SpawningCustomer = Instantiate(CustomerModels[TheCustomerIndex], Canvas.transform);
        SpawnCooldown = baseCooldown * CooldownMultiplier;
        CurrentCustomers.Add(SpawningCustomer);
    }

    void checkOverlap()
    {
        for (var i = CurrentCustomers.Count - 1; i > -1; i--)
        {
            if (i > 0)
            {
                CurrentCustomers[i].transform.position = new Vector3(CurrentCustomers[0].transform.position.x * i - 1000,
                    Canvas.transform.position.y - Canvas.transform.lossyScale.y / 2, 0);
            }
            else
            {
                CurrentCustomers[i].transform.position = new Vector3(transform.position.x + Canvas.transform.lossyScale.x / 2,
                    Canvas.transform.position.y - Canvas.transform.lossyScale.y / 2, 0);
            }
        }
    }

    #region removing Customers
    //All customers
    public void ClearCustomers()
    {
        for (int i = 0; i < CurrentCustomers.Count; i++)
        {
            Destroy(CurrentCustomers[i]);
        }
    }
    // Removes null customers from the list
    void RemoveExCustomers()
    {
        for (var i = CurrentCustomers.Count - 1; i > -1; i--)
        {
            if (CurrentCustomers[i] == null) { CurrentCustomers.RemoveAt(i); }
            ;
        }
    }

    #endregion
}