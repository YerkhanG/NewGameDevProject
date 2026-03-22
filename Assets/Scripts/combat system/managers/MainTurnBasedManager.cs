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

    public void OnDisable()
    {
        GlobalEvents.OnEndTurnButtonPressed -= HandleEndTurnButtonPressed;
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
        PlayerTurnUIManager.instance.UIActivate();
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
        CombatEntityManager.instance.UpdateBuffsAndDebuffs();
        PlayerTurnUIManager.instance.UIDeactivate();
        StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        Debug.Log("Enemy turn");  
        EnemyAIManager.instance.AllEnemiesTurn();
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
        Invoke(nameof(StartPlayerTurn),3);
    }

    private void EndCombat()
    {
        
    }

    private bool CheckCombatState()
    {
        if (CombatEntityManager.instance.mainCharacter.IsAlive)
        {
            return true;
        }
        return false;
    }
}