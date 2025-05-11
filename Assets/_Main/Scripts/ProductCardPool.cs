using System.Collections.Generic;
using UnityEngine;

public class ProductCardPool : MonoBehaviour
{
    public GameObject cardPrefab;
    public int poolSize = 40; // Start with enough to cover 1–2 screens

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    public void Initialize(Transform parent)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(cardPrefab, parent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetCard()
    {
        if (pool.Count > 0)
        {
            GameObject card = pool.Dequeue();
            card.SetActive(true);
            return card;
        }

        // Fallback in rare case: create one more
        return Instantiate(cardPrefab);
    }

    public void ReturnCard(GameObject card)
    {
        card.SetActive(false);
        pool.Enqueue(card);
    }

    public void Clear()
    {
        foreach (var card in pool)
            Destroy(card);

        pool.Clear();
    }
}
