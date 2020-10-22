using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static void PauseGame() {
        Time.timeScale = 0;
    }

    public static void UnpauseGame() {
        Time.timeScale = 1;
    }
}
