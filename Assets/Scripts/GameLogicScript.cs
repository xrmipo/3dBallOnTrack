using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Strings
{
    public static string player_Tag, checkpoint_Tag, finish_Tag, gameController_Tag, resultPrefs_Key;
}
public class GameLogicScript : MonoBehaviour
{
    [SerializeField] private float sphereSpeed = 1.5f;
    [SerializeField] private Transform checkPointsContainer, playerRespawnPoint;
    [SerializeField] private GameObject finishObject, playerSpherePrefab;
    [SerializeField] private string playerTag, checkpointTag, finishTag, gameControllerTag, resultPrefsKey;

    private int oldCheckOrder, lastCheckIndex;
    private bool rightOrder;
    private GameObject playerSphere;

    private void Awake() { SetPublicStrings(); }

    public void StartNewGame()
    {
        lastCheckIndex = checkPointsContainer.childCount - 1;
        oldCheckOrder = -1;
        rightOrder = false;
        ResetCheckpointsState();
        RespawnPlayer();
        GameController.SetStartTime();
    }

    public void TriggerProcessing(Transform currentTrigger)
    {
        string tag = currentTrigger.gameObject.tag;
        if (tag == Strings.checkpoint_Tag)
        {
            int idx = currentTrigger.GetSiblingIndex();
            rightOrder = (idx - 1 == oldCheckOrder);
            currentTrigger.gameObject.SetActive(!rightOrder);
            if (rightOrder)
            {
                oldCheckOrder = idx;
                CheckLastCheck(idx);
            }
        }
        else Finish(tag == finishTag);
    }

    public void DestroySphere()
    {
        try { Destroy(playerSphere); } catch { }
    }

    private void RespawnPlayer()
    {
        DestroySphere();
        playerSphere = Instantiate(playerSpherePrefab, playerRespawnPoint);
        playerSphere.GetComponent<SphereScript>().speed = sphereSpeed;
    }

    private void ResetCheckpointsState()
    {
        for (int a = 0; a < checkPointsContainer.childCount; a++)
        {
            var curCheck = checkPointsContainer.GetChild(a).gameObject;
            curCheck.SetActive(true);
        }
        finishObject.SetActive(false);
    }

    private void CheckLastCheck(int currentIndex)
    {
        finishObject.SetActive(currentIndex == lastCheckIndex);
    }

    private void Finish(bool win)
    {
        DestroySphere();
        if (win)
        {
            GameController.SaveResult(GameController.finishTime());
        }
        GameController.GameState.GameEnd(win);
    }

    private void SetPublicStrings()
    {
        Strings.player_Tag = playerTag;
        Strings.checkpoint_Tag = checkpointTag;
        Strings.finish_Tag = finishTag;
        Strings.gameController_Tag = gameControllerTag;
        Strings.resultPrefs_Key = resultPrefsKey;
    }
}