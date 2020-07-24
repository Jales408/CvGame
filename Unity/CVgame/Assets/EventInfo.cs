using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public abstract class EventInfo
    {
        /*
         * The base EventInfo,
         * might have some generic text
         * for doing Debug.Log?
         */
        public string EventDescription;
    }

    public class DebugEventInfo : EventInfo
    {
        public int VerbosityLevel;
    }

    public class EndDialogInfo : EventInfo
    {
        
    }

    public class PlayerMovementInfo : EventInfo
    {
        string zoneIdentifier;
    }

    public class SpawnerInfo : EventInfo
    {
        GameObject spawner;
        bool hasNoMoreChildren;
    }

    public class ObjectiveInfo : EventInfo
    {
        GameObject objective;
        bool isWin;
    }
}