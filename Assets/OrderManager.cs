using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [HideInInspector] public static OrderManager Instance;

    [Header("Fields")]
    [SerializeField] Transform orderParent;

    [Header("Prefabs")]
    [SerializeField] GameObject orderPrefab;

    [Header("Items")]
    [SerializeField] ItemType[] items;

    // Private
    List<Order> activeOrders = new List<Order>();

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;   
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateOrder(string itemID)
    {
        Order order = Instantiate(orderPrefab, orderParent).GetComponent<Order>();
        foreach (ItemType item in items)
        {
            if (item.id == itemID)
                order.type = item;
        }
        activeOrders.Add(order);
    }

    public void ItemExported(ItemSlot slot, ItemType item)
    {
        Order toRemove = null;
        foreach (Order order in activeOrders)
        {
            if (order.type == item)
            {
                // Set the order to be removed from the list
                toRemove = order;

                // Destroy the order's UI element
                Destroy(order.gameObject);

                // Clear the physical item parcel
                Destroy(slot.item.gameObject);
                slot.ClearItem();

                /*
                 * Handle order completion rewards here
                 */

                gameManager.ModifyFunds(10); // Temporary

                Debug.Log("Order Completed");

                break;
            }
        }

        if (toRemove != null)
            activeOrders.Remove(toRemove);

    }
}