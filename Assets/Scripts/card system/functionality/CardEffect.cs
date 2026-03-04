using System.Collections.Generic;
using model.entity;
using UnityEngine;

namespace card_system.functionality
{
    //TODO: Create other 2 types of effects: Buff and debuff , and try to make em work.
    [CreateAssetMenu(fileName = "New Card Effect Data", menuName = "Card Effect/Card Effect Data")]
    public abstract class CardEffect : ScriptableObject
    {
        public TargetType targetType;
        public abstract void Execute(EffectContext context);
        
        public List<Entity> ResolveTargets(EffectContext context, TargetType targeting)
        {
            if (context.isManual && context.manualTargetEntity != null)
            {
                return new List<Entity> {  context.manualTargetEntity };
            }

            switch (targeting)
            {
                case TargetType.AllEnemies:
                    return new List<Entity>(context.allTargets);
                case TargetType.RandomEnemy:
                    if (context.allTargets.Count > 0)
                    {
                        int randIndex = Random.Range(0, context.allTargets.Count);
                        return new List<Entity>{context.allTargets[randIndex]};
                    }
                    return new List<Entity>();
                default:
                    Debug.Log("Someting Wong");
                    return new List<Entity>();
            }
        }
    }
    
    public enum TargetType
    {
        ManualTargeting ,RandomEnemy, Self, SelfGear, AllEnemies
    }

    public struct EffectContext
    {
        public Player caster;
        public Entity manualTargetEntity;
        public Entity singleTargetEntity;
        public List<Entity> allTargets;
        public bool isManual;
    }
}