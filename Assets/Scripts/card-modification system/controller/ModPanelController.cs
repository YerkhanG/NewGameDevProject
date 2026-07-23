using System.Collections.Generic;
using card_modification_system.data;
using card_system.animation;
using card_system.UI;
using combat_system;
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
            //Need to remove the targetting and dragging components
            var card = Instantiate(cardPrefab, cardPlace).GetComponent<SingleCardController>();
            card.Setup(record);
            card.GetComponent<CardAnimationController>().enabled = false;
            //Need to add clicking to the options
            foreach (var mod in PickRandom(modPool, 3))
                Instantiate(modChoicePrefab, modPanel.transform)
                    .GetComponent<SingleModController>().SetUp(mod, HandleModPicked);
        }

        private List<ModDefinition> PickRandom(List<ModDefinition> mods , int amount)
        {
            List<ModDefinition> result = new List<ModDefinition>();
            for (var i = 0; i < amount; i++)
            {
                result.Add(mods[Random.Range(0, mods.Count)]);
            }
            return result;
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