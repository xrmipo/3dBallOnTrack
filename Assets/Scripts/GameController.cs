using System;
using UnityEngine;

public static class GameController
{
    private static DateTime startTime;      
    public static GameLogicScript GameLogic = GameObject.FindGameObjectWithTag(Strings.gameController_Tag).GetComponent<GameLogicScript>();
    public static GameStateScript GameState = GameLogic.gameObject.GetComponent<GameStateScript>();
    public static bool inMenu;

    public static void TriggerAction(Transform activeTrigger)
    {
        GameLogic.TriggerProcessing(activeTrigger);
    }

    public static void SetStartTime()
    {
        startTime = DateTime.Now;
    }

    public static void SaveResult(float result)
    {
        PlayerPrefs.SetFloat(Strings.resultPrefs_Key, result);
        PlayerPrefs.Save();
    }

    public static float finishTime()
    {
        float finTime = (float)Math.Round((DateTime.Now - startTime).TotalSeconds, 2);
        return finTime;
    }

    public static float oldResult()
    {
        float result;
        try { result = PlayerPrefs.GetFloat(Strings.resultPrefs_Key); } catch { result = 0; }
        return result;
    }
}