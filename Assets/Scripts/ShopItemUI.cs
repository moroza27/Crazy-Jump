using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI Елементи")]
    public Image birdImage;       // Сюди перетягнемо об'єкт картинки пташки
    public Button selectButton;   // Сюди перетягнемо саму кнопку картки

    private BirdData currentBirdData;

    // Цей метод ми викликатимемо, щоб заповнити картку даними пташки
    public void SetupItem(BirdData data)
    {
        currentBirdData = data;
        
        // Показуємо картинку пташки
        birdImage.sprite = data.birdSprite;
        birdImage.gameObject.SetActive(true);

        // Перевіряємо, чи гравець набрав достатньо очок
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Отримуємо рекорд гравця

        if (highScore >= data.scoreRequired)
        {
            // Пташка розблокована
            selectButton.interactable = true;
            birdImage.color = Color.white; // Яскрава пташка
        }
        else
        {
            // Пташка ще заблокована
            selectButton.interactable = false;
            birdImage.color = new Color(0.2f, 0.2f, 0.2f, 0.6f); // Темна/напівпрозора
        }
    }

    // Метод, який спрацює при натисканні на картку пташки
    public void OnItemClick()
    {
        // Зберігаємо ID вибраної пташки, щоб гра знала, ким ми граємо
        PlayerPrefs.SetString("SelectedBird", currentBirdData.birdId);
        Debug.Log("Вибрано пташку: " + currentBirdData.birdName);
    }
}