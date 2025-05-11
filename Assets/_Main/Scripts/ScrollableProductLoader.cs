using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableProductLoader : MonoBehaviour
{
    public ScrollRect scrollRect;
    public ProductCardPool pool;
    public Transform contentHolder;
    public int batchSize = 30;

    private List<Product> products;
    private int currentIndex = 0;
    private bool isLoading = false;

    private readonly List<GameObject> activeCards = new List<GameObject>();

    public void Load(List<Product> productList)
    {
        products = productList;
        currentIndex = 0;

        ClearAllActiveCards(); // Remove old UI when applying filter/resetting
        pool.Clear();
        pool.Initialize(contentHolder);
        LoadNextBatch();

        scrollRect.onValueChanged.RemoveAllListeners();
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    void LoadNextBatch()
    {
        int count = Mathf.Min(batchSize, products.Count - currentIndex);

        for (int i = 0; i < count; i++)
        {
            GameObject card = pool.GetCard();
            card.transform.SetParent(contentHolder, false);
            card.GetComponent<ProductCard>().Setup(products[currentIndex]);
            activeCards.Add(card);
            currentIndex++;
        }

        isLoading = false;
    }

    void OnScroll(Vector2 scrollPos)
    {
        // Trigger when close to bottom
        if (!isLoading && scrollRect.verticalNormalizedPosition <= 0.01f && currentIndex < products.Count)
        {
            isLoading = true;
            LoadNextBatch(); // No clearing!
        }
    }

    void ClearAllActiveCards()
    {
        foreach (var card in activeCards)
        {
            pool.ReturnCard(card);
        }
        activeCards.Clear();
    }
}
