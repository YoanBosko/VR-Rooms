using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveTo(string parameter1)
    {
        SceneManager.LoadScene(parameter1);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.ExitPlaymode();
    }
}
