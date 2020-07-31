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
        public string zoneIdentifier;
    }


    public class SpawnerInfo : EventInfo
    {
        public iSpawner spawner;
        public bool hasEnded;
    }

        public class ObjectiveInfo : EventInfo
    {
        public ObjectiveInfo(iObjective objective, bool success){
            this.objective = objective;
            this.success = success;
        }
        public iObjective objective;

        public bool success;
    }

    public class KillInfo : EventInfo
    {
        public KillInfo(EnnemiType type){
            this.type = type;
        }
        public EnnemiType type;
    }
}

public enum EnnemiType {Robot,Tortle}