using UnityEngine;
using UnityEngine.SceneManagement;

namespace DapperDino.GGJ2020
{
    public class GameOver : MonoBehaviour
    {
        public void Retry() => SceneManager.LoadScene(1);
        public void GoToMenu() => SceneManager.LoadScene(0);
        public void ExitGame() => Application.Quit();
    }
}
