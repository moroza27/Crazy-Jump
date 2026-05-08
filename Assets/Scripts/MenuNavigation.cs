using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuNavigation : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Вихід із гри натиснуто!"); 
        Application.Quit();
    }
}