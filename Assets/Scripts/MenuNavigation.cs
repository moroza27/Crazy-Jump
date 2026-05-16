using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Для кнопки "ГРАТИ" в головному меню
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Нове: Для кнопки "МАГАЗИН" в головному меню
    public void OpenShop()
    {
        SceneManager.LoadScene("ShopScene"); 
    }

    // Нове: Для прозорої кнопки-стрілочки в магазині
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Для кнопки "ВИХІД" в головному меню
    public void ExitGame()
    {
        Debug.Log("Гра закривається!"); 
        Application.Quit();
    }
}