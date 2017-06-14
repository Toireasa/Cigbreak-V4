using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CigBreakGUI
{
    /// <summary>
    /// Handles dismissing popup windows when the backgreound is pressed
    /// Also block events from bubbling out of the popup
    /// </summary>

    [RequireComponent(typeof(EventTrigger))]
    public class PopupBackground : MonoBehaviour
    {
        private EventTrigger eventTrigger = null;

        public UnityAction onPopupDismissed { get; set; }

        void Start()
        {
            eventTrigger = GetComponent<EventTrigger>();
            

            AddEventTrigger(SetSelected, EventTriggerType.PointerDown);
            AddEventTrigger(SetSelected, EventTriggerType.PointerClick);
            AddEventTrigger(SetSelected, EventTriggerType.Drag);
        }

        #region TriggerEventsSetup

        // Register an event tigger, use listener with no parameters
        private void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
        {
            // Create a nee TriggerEvent and add a listener
            EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
            trigger.AddListener((eventData) => action()); // ignore event data

            // Create and initialise EventTrigger.Entry using the created TriggerEvent
            EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

            // Add the EventTrigger.Entry to delegates list on the EventTrigger
            eventTrigger.triggers.Add(entry);
        }

        // Register an event tigger, use listener that uses the BaseEventData passed to the Trigger
        private void AddEventTrigger(UnityAction<BaseEventData> action, EventTriggerType triggerType)
        {
            // Create a nee TriggerEvent and add a listener
            EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
            trigger.AddListener((eventData) => action(eventData)); // capture and pass the event data to the listener

            // Create and initialise EventTrigger.Entry using the created TriggerEvent
            EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

            // Add the EventTrigger.Entry to delegates list on the EventTrigger
            eventTrigger.triggers.Add(entry);
        }

        #endregion

        #region Callbacks

        // On press register this object as current, so other elements checking the input agains the event system see that it's already being consumed by GUI
        private void SetSelected(BaseEventData data)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, data);
        }

        #endregion
    }

}