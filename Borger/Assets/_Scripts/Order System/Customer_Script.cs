using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Customer_Script : MonoBehaviour
{
    [Header("The Image stuff")]
    [SerializeField] private GameObject Order_Block;
    [SerializeField] private List<Sprite> Buns = new();
    [Tooltip("Any ingredient for the burger here (sprites)")]
    [SerializeField] private Sprite[] Ingredients;
    public List<Image> Current_Orders = new();
    [SerializeField] private GameObject OrderParent;

    [Header("Loadbar / Time Stuff")]
    [SerializeField] private Image Loadbar;
    [Tooltip("Time u can take to complete the order")]
    [SerializeField] private float Base_OrderTime;
    private float OrderTime;
    [SerializeField] private int OrderValue;

    void Start()
    {
        if (OrderParent == null) { Destroy(gameObject); }
        OrderTime = Base_OrderTime;
        GetOrder();
    }

    #region Order Generation

    void GetOrder()
    {
        for (int i = 0; i < OrderParent.transform.childCount; i++)
        {
            if (OrderParent.transform.GetChild(i).gameObject.activeSelf == true)
            {
                Current_Orders.Add(OrderParent.transform.GetChild(i).gameObject.GetComponent<Image>());
            }
        }
        foreach (Image CurrentOrder in Current_Orders)
        {
            // Top bun
            int Max = Buns.Count;
            Current_Orders[0].sprite = Buns[0];
            //Toppings
            for (int i = 1; i < Current_Orders.Count - 1; i++)
            {
                int RandomTopping = UnityEngine.Random.Range(0, Ingredients.Length);
                Current_Orders[i].sprite = Ingredients[RandomTopping];
            }
            Current_Orders[Current_Orders.Count - 1].sprite = Buns[1];
        }
    }
    #endregion

    #region Loadbar
    void Update()
    {
        Loadbar_Updater();
    }

    void Loadbar_Updater()
    {
        OrderTime -= Time.deltaTime;
        Loadbar.fillAmount = OrderTime / Base_OrderTime;
        if (OrderTime <= 0) { LeavingCustomer(); }
    }
    #endregion

    #region Ending Order
    void LeavingCustomer()
    {
        DestroyOrder(false);
    }

    private void DestroyOrder(bool isServed)
    {
        if (isServed) {
            Shifts_System Instance = FindObjectOfType<Shifts_System>();
            Instance.CurrentValue += OrderValue;
            print("YOU SERVED"); 
        }
        Destroy(Order_Block);
    }
    #endregion

}