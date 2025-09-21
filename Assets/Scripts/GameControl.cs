using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
{
    [HideInInspector]
    public float waitForAnimTime;

    public ForceInventoryChange inventoryChange;

    public static GameControl instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Scene 2");
            inventoryChange.ChangeToScene2Inventory(ScenarioPicker.instance.currentScenario.TFAInventory);
        }
    }

    //--------------------------SetPlayer--------------------------------
    //public void SetPlayer(PlayerStats copied)
    //{
    //    instance.runtimePlayer.CopyFrom(copied);
    //}


    //--------------------------Scene Changing--------------------------------

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void LoadNewScene(string NextScene)
    {
        SceneManager.LoadScene(NextScene);
    }

    public void SetWaitingTime(float waitingTime)
    {
        instance.waitForAnimTime = waitingTime;
    }

    public void LoadSceneWithWait(string sceneName)
    {
        instance.StartCoroutine(WaitForAnim(sceneName));
    }

    public IEnumerator WaitForAnim(string sceneName)
    {
        yield return new WaitForSecondsRealtime(instance.waitForAnimTime);

        SceneManager.LoadScene(sceneName);
    }
}
