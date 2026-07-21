using System.Collections.Generic;
using card_system.data;
using card_system.functionality;
using combat_system;
using persistence_system.model;
using UnityEngine;

namespace persistence_system.helpers
{
    public static class CardEffectBuilder
    {
        public static List<CardEffect> BuildRuntimeEffects(CardInstanceRecord record)
        {
            CardData template = CardRegistry.instance.GetCard(record.templateId);
            List<CardEffect> effects = new List<CardEffect>();

            foreach (var baseEffect in template.cardEffects)
                effects.Add(Object.Instantiate(baseEffect));   // clone, not the shared asset

            foreach (var mod in record.modifications)
            {
                switch (mod.type)
                {
                    case ModificationType.FieldOverride:
                        effects[mod.effectIndex].ApplyFieldOverride(mod.fieldName, mod.value);
                        break;
                    case ModificationType.AddEffect:
                        effects.Add(Object.Instantiate(CardRegistry.instance.GetCardEffect(mod.effectTemplateId)));
                        break;
                    case ModificationType.RemoveEffect:
                        effects.RemoveAt(mod.effectIndex);
                        break;
                }
            }
            return effects;
        }
    }
}