using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public void Change(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("row", 3);
        PlayerPrefs.SetInt("col", 4);
        PlayerPrefs.Save();
    }

    public void ChangeToEasy(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("row", 2);
        PlayerPrefs.SetInt("col", 3);
        PlayerPrefs.Save();
    }

    public void ChangeToHard(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("row", 4);
        PlayerPrefs.SetInt("col", 4);
        PlayerPrefs.Save();
    }

    public void Exit()
    {
        Application.Quit();
    }

}
