using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Customer_Script : MonoBehaviour
{
    [Header("The Image stuff")]
    [SerializeField] private GameObject Order_Block;
    [SerializeField] private List<Sprite> Buns = new();
    [SerializeField] private Sprite[] Burgers;
    [SerializeField] private Sprite[] Ingredients;
    [SerializeField] private Sprite[] Toppings;
    public List<Image> Current_Orders = new();
    [SerializeField] private GameObject OrderParent;

    [Header("Loadbar / Time Stuff")]
    [SerializeField] private Image Loadbar;
    [SerializeField] private float Base_OrderTime;
    private float OrderTime;

    void Start()
    {
        if (OrderParent == null) { Destroy(gameObject); }
        OrderTime = Base_OrderTime;
        GetOrder();
    }
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
            int MaxOrder = Buns.Count;
            Current_Orders[0].sprite = Buns[0];
            Current_Orders[MaxOrder - 1].sprite = Buns[MaxOrder - 1];
            CheckForMiddleImage();
        }
    }

    private void CheckForMiddleImage()
    {
        if (Current_Orders[0].sprite.name.Contains("Topping"))
        {
            string[] currSprite = Current_Orders[0].sprite.name.Split('_');
            string finalSprite = string.Format("{0}_Pot_{1}", currSprite[0], currSprite[4]);
            for (int i = 0; i < Ingredients.Length; i++)
            {
                if (Ingredients[i] == null) continue;
                if (Ingredients[i].name.Equals(finalSprite))
                {
                    Current_Orders[3].sprite = Ingredients[i];
                    break;
                }
            }
        }
        else
        {
            Current_Orders[3].sprite = Current_Orders[0].sprite;
        }
    }


    #region Ending Order
    void LeavingCustomer()
    {
        DestroyOrder(false);
    }

    private void DestroyOrder(bool isServed)
    {
        Customer_Script ParentScript = gameObject.transform.parent.GetComponent<Customer_Script>();
        if (isServed) { print("YOU SERVED"); }
        Destroy(ParentScript);
        Destroy(Order_Block);
        Destroy(this);
    }
    #endregion

}