using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool hasGameEnd = false;

    public float restartDelay = 1f;
    public void GameOver()
    {
        if (hasGameEnd == false)
        {
            hasGameEnd = true;
            Restart();
            Invoke("Restart", restartDelay);
        }
        
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
