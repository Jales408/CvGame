using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class EventSystem : MonoBehaviour
    {
        private int currentGeneratedKey=0;

        // Use this for initialization
        void OnEnable()
        {
            __Current = this;
        }

        static private EventSystem __Current;
        static public EventSystem Current
        {
            get
            {
                if(__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }

        delegate void EventListener(EventInfo ei);
        Dictionary<System.Type, Dictionary<int,EventListener>> eventListeners;

        public int RegisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, Dictionary<int,EventListener>>();
            }

            if(eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new Dictionary<int,EventListener>();
            }

            EventListener wrapper = (ei) => { listener((T)ei); };
            int key = currentGeneratedKey++;
            eventListeners[eventType].Add(key,wrapper);
            return key;
        }

        public void UnregisterListener<T>(int key) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners != null && eventListeners.ContainsKey(eventType) && eventListeners[eventType] != null && eventListeners[eventType].ContainsKey(key))
            {
                eventListeners[eventType].Remove(key);
            }
        }

        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners.ContainsKey(trueEventInfoClass) == false ||eventListeners[trueEventInfoClass] == null)
            {
                // No one is listening, we are done.
                return;
            }
            EventListener[] temporaryListeners = new EventListener[eventListeners[trueEventInfoClass].Count];
            int i = 0;

            //Ugly copy because the delegate cannot be called while iterating throught the dico
            foreach(EventListener eventListener in eventListeners[trueEventInfoClass].Values)
            {
                temporaryListeners[i] = eventListener;
                i++;
            }
            foreach(EventListener eventListener in temporaryListeners)
            {
                eventListener(eventInfo);
            }
        }

    }
}