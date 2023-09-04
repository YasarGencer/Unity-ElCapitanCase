using System;
using UnityEngine;  
    public class EventRunner
    { 
        public static void LevelStart()
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LevelStart, new IntArgs(PlayerPrefs.GetInt("Level")));
        }

        public static void LevelFail()
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LevelFail);
        }

        public static void LevelSuccess()
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LevelSuccess);
        }

        //Level Restart is a good event to Unregister from your custom events
        public static void LevelRestart()
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LevelRestart);
        }

        //Level Finish is a good event to Unregister from your custom events
        public static void LevelFinish()
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LevelFinish);
        }

        public static void ChangeVibMode(bool vibModeOn)
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.VibrationChange, new BoolArgs(vibModeOn));
        } 

        public static void LoadSceneStart(bool levelFinish = false)
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LoadSceneStart, new BoolArgs(levelFinish));
        }

        public static void LoadSceneFinish()
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LoadSceneFinish);
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.LevelLoaded);
        }

        public static void Stationary(Vector3Args args)
        {
            MainManager.Instance.EventManager.RunOnStationary(args);
        }

        public static void HoldStart(Vector3Args args)
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.OnHoldStart, args);
        }

        public static void HoldFinish(Vector3Args args)
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.OnHoldFinish, args);
        }

        public static void SendName(StringArgs args)
        {
            MainManager.Instance.EventManager.InvokeEvent(EventTypes.OnHoldFinish, args);
            
        }

} 
