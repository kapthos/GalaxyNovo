using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuComandos : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene(2);
    }
}
