using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[Serializable]
public class ActivationRule
{
    public UnityEvent func;
    public float activateDelay;
}
public class GameControl : MonoBehaviour
{
    [HideInInspector]
    public float waitForAnimTime;

    public ActivationRule[] rules;

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

    void Start()
    {
        foreach (ActivationRule rule in rules)
        {
            StartCoroutine(Activate(rule));
        }
    }

    private void Update()
    {
        //if (!instance.isBulletTime)
        //{
        //    instance.time = 1f;
        //    instance.lerpTime = .5f;
        //}
        //else
        //{
        //    instance.time = 0.1f;
        //    instance.lerpTime = 0.01f;
        //}
        //Time.timeScale = Mathf.Lerp(Time.timeScale, instance.time, instance.lerpTime);

        //Debug.Log($"[TimeScaleDebugger] Time.timeScale = {Time.timeScale}");
    }


    private IEnumerator Activate(ActivationRule rule)
    {
        yield return new WaitForSeconds(rule.activateDelay);
        rule.func.Invoke();
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
