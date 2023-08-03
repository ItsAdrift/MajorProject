using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;   

    public Pallet pallet;

    public ItemType[] startingDelivery;

    [Header("Scripts")]
    [SerializeField] Money money;
    [SerializeField] public GoalManager goalManager;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateFirstDelivery()
    {
        pallet.AddItemTypes(startingDelivery);
    }

    public void ModifyFunds(int amount)
    {
        money.money += amount;
    }

    public void PlayerJoined()
    {

    }

}
