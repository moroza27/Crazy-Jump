using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Налаштування сітки")]
    public Transform[] gridSlots;       // Сюди ми перетягнемо позиції для 8 рамок
    public ShopItemUI itemPrefab;       // Сюди перетягнемо наш синій префаб з папки Project

    [Header("Дані пташок")]
    public BirdData[] allBirds;         // Сюди скинемо файли пташок з папки Configs

    void Start()
    {
        FillShop();
    }

    void FillShop()
    {
        // Проходимо по черзі по всіх пташках, які ми додали в список
        for (int i = 0; i < allBirds.Length; i++)
        {
            // Якщо раптом пташок більше, ніж рамок на фоні — зупиняємось
            if (i >= gridSlots.Length) break;

            // Створюємо копію кнопки на позиції відповідної рамки
            ShopItemUI newItem = Instantiate(itemPrefab, gridSlots[i]);
            
            // Скидаємо позицію в нуль, щоб вона стала рівно по центру рамки
            newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Передаємо в картку дані конкретної пташки
            newItem.SetupItem(allBirds[i]);
        }
    }
}