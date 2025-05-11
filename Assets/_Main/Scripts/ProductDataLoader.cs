using System.Collections.Generic;
using UnityEngine;

public class ProductDataLoader : MonoBehaviour
{
    public static List<Product> LoadProducts()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("products");
        ProductListWrapper wrapper = JsonUtility.FromJson<ProductListWrapper>(jsonText.text);
        return wrapper.products;
    }
}
