// Menu do Game 2D, ou 3D na Unity:

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Carrega a cena de índice 1
    }

    public void ExitGames()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Sai do jogo em build
#endif
    }
}

// Obs: Esse código é válido, tanto para game 2D, como 3D