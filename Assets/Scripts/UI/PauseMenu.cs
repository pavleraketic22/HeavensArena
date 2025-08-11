using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        public static bool GameIsPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            Music.Instance.PlaySFX("Unpause",0.7f);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; 
            GameIsPaused = false;
        }

        void Pause()
        {
            Music.Instance.PlaySFX("Pause",0.7f);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f; 
            GameIsPaused = true;
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene("MainMenu"); 
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}
