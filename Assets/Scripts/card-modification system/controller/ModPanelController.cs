using System.Collections.Generic;
using System.Linq;
using card_modification_system.data;
using card_system.animation;
using card_system.functionality;
using card_system.UI;
using combat_system;
using math_structs;
using persistence_system.helpers;
using persistence_system.manager;
using persistence_system.model;
using UnityEngine;

namespace card_modification_system.controller
{
    public class ModPanelController : MonoBehaviour
    {
        //For now , not sure if i will be changing this 
        [SerializeField] private GameObject modPanel;
        [SerializeField] private Transform cardPlace;
        [SerializeField] private Transform modChoicesParent;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform modChoicePrefab;
        //What specific card's data is this gonna change
        private CardInstanceRecord targetRecord;
        //IDK IF IT SHOULD BE HERE 
        [SerializeField] private List<ModDefinition> modPool;
        
        public void Open(CardInstanceRecord record)
        {
            targetRecord = record;
            Instantiate(cardPrefab, cardPlace).GetComponent<SingleCardController>().Setup(record);

            List<CardEffect> currentEffects = CardEffectBuilder.BuildRuntimeEffects(record);
            List<ModDefinition> choices = BuildChoices(currentEffects, totalCount: 3);

            foreach (var mod in choices)
                Instantiate(modChoicePrefab, modPanel.transform)
                    .GetComponent<SingleModController>().SetUp(mod, HandleModPicked);
        }

        private List<ModDefinition> BuildChoices(List<CardEffect> currentEffects, int totalCount)
        {
            List<ModDefinition> result = new List<ModDefinition>();

            ModDefinition upgrade = PickCompatibleUpgrade(currentEffects);
            if (upgrade != null) result.Add(upgrade);

            var newEffectPool = modPool.Where(m => m.type == ModificationType.AddEffect).ToList();
            ShuffleUtil.Shuffle(newEffectPool);
            result.AddRange(newEffectPool.Take(totalCount - result.Count));

            ShuffleUtil.Shuffle(result);
            return result;
        }

        private ModDefinition PickCompatibleUpgrade(List<CardEffect> currentEffects)
        {
            var shuffled = new List<CardEffect>(currentEffects);
            ShuffleUtil.Shuffle(shuffled);

            foreach (var effect in shuffled)
            {
                var compatible = modPool
                    .Where(m => m.type == ModificationType.FieldOverride && effect.HasField(m.fieldName))
                    .ToList();
                if (compatible.Count > 0)
                    return compatible[Random.Range(0, compatible.Count)];
            }
            return null; // nothing on this card has an upgradeable field yet
        }
        private void HandleModPicked(ModDefinition chosen)
        {
            targetRecord.modifications.Add(new CardModification
            {
                type = chosen.type,
                effectIndex = chosen.effectIndex,
                fieldName = chosen.fieldName,
                value = chosen.value,
                effectTemplateId = chosen.effectTemplateIdForAdd
            });

            PersistenceManager.instance.SaveSceneData(cards: DeckManager.instance.deck);

            gameObject.SetActive(false);
        }
    }
}