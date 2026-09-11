using global_events;
using model.entity;
using UnityEngine;

public class PassiveEffectBase : MonoBehaviour
{
    [SerializeField] protected int duration = 0; // 0 or less = permanent
    protected Entity owner;
    private int turnsRemaining;

    protected virtual void Awake()
    {
        owner = GetComponentInChildren<Entity>();
        turnsRemaining = duration;
    }

    protected virtual void OnEnable()
    {
        if (duration > 0)
            GlobalEvents.OnPlayerTurnEnded += HandleTurnEnded;
    }

    protected virtual void OnDisable()
    {
        if (duration > 0)
            GlobalEvents.OnPlayerTurnEnded -= HandleTurnEnded;
    }

    private void HandleTurnEnded()
    {
        if (--turnsRemaining <= 0)
            Destroy(this);
    }
}
