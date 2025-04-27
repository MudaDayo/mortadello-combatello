using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    public void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1f; // Reset time scale to normal
        SceneManager.LoadScene(sceneName);
    }

    public void Reset()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        panel1.SetActive(false);
        panel2.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
