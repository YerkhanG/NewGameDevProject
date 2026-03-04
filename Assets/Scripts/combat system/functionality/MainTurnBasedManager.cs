using System;
using System.Collections.Generic;
using combat_system;
using combat_system.UI;
using global_events;
using model;
using model.entity;
using UnityEngine;

public class MainTurnBasedManager :  MonoBehaviour
{
    public static MainTurnBasedManager instance;
    public Entity mainCharacter;
    public int turnCounter;
    private bool isPlayerTurn;
    public void Awake()
    {
        turnCounter = 1;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        GlobalEvents.OnEndTurnButtonPressed += HandleEndTurnButtonPressed;
    }

    private void HandleEndTurnButtonPressed()
    {
        if (isPlayerTurn)
        {
            EndPlayerTurn();
        }
    }

    private void Start()
    {
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
        turnCounter++;
        Debug.Log($"Player Turn {turnCounter}");
        
        ManaCountManager.instance.ResetMana();
        PlayerController.instance.RedrawCards();
    }
    private void EndPlayerTurn()
    {
        Debug.Log("Player turn ended");
        isPlayerTurn = false;
        
        // Disable player input, hide end turn button
        PlayerTurnUIManager.instance.UIDeactivate();
        StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        Debug.Log("Enemy turn");  
        EndEnemyTurn();
    }

    private void EndEnemyTurn()
    {
        Debug.Log("Enemy turn ended");
        if (!CheckCombatState())
        {
            Debug.Log("Game ended you dead");
            EndCombat();
        }
        PlayerTurnUIManager.instance.UIActivate();
        StartPlayerTurn();
    }

    private void EndCombat()
    {
        
    }

    private bool CheckCombatState()
    {
        if (mainCharacter.IsAlive)
        {
            return true;
        }
        return false;
    }
}