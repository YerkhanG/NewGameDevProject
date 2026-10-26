using global_events;
using model.entity;
using UnityEngine;

public class PassiveEffectBase : MonoBehaviour
{
    [SerializeField] protected int duration = 0; // 0 or less = permanent
    protected Entity owner;
    private int turnsRemaining;
    private bool initialized = false;

    protected virtual void Awake()
    {
        owner = GetComponentInChildren<Entity>();
    }

    public void Initialize(int dur)
    {
        duration = dur;
        turnsRemaining = duration;
        if (duration > 0)
            GlobalEvents.OnPlayerTurnEnded += HandleTurnEnded;
        initialized = true;
    }

    protected virtual void OnDisable()
    {
        if (duration > 0)
            GlobalEvents.OnPlayerTurnEnded -= HandleTurnEnded;
    }

    private void HandleTurnEnded()
    {
        if (--turnsRemaining <= 0)
        {
            enabled = false;   // fires OnDisable → unsubscribes immediately
            Destroy(this);
        }
    }
}
