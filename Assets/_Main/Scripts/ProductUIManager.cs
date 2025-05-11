using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductUIManager : MonoBehaviour
{
    [Header("Prefabs and Holders")]
    public ProductCardPool cardPool;
    public ScrollableProductLoader scrollLoader;

    [Header("Category Toggles")]
    public Toggle toggleClothes;
    public Toggle toggleWatches;
    public Toggle toggleJewelry;
    public Toggle toggleFootwear;
    public Toggle toggleAccessories;

    [Header("Subcategory Toggles")]
    public Toggle toggleMale;
    public Toggle toggleFemale;
    public Toggle toggleKids;

    [Header("Buttons")]
    public Button applyButton;
    public Button resetButton;

    [Header("Animator")]
    public FilterPanelAnimator filterAnimator;

    private List<Product> allProducts;

    void Start()
    {
        allProducts = ProductDataLoader.LoadProducts();
        if (allProducts == null || allProducts.Count == 0)
        {
            Debug.LogWarning("No products loaded.");
            return;
        }

        scrollLoader.Load(allProducts);

        applyButton.onClick.AddListener(ApplyFilter);
        resetButton.onClick.AddListener(ResetFilter);
    }

    void ApplyFilter()
    {
        List<string> activeCategories = new List<string>();
        List<string> activeSubcategories = new List<string>();

        if (toggleClothes.isOn) activeCategories.Add("Clothes");
        if (toggleWatches.isOn) activeCategories.Add("Watches");
        if (toggleJewelry.isOn) activeCategories.Add("Jewelry");
        if (toggleFootwear.isOn) activeCategories.Add("Footwear");
        if (toggleAccessories.isOn) activeCategories.Add("Accessories");

        if (toggleMale.isOn) activeSubcategories.Add("Male");
        if (toggleFemale.isOn) activeSubcategories.Add("Female");
        if (toggleKids.isOn) activeSubcategories.Add("Kids");

        List<Product> filtered = allProducts.FindAll(p =>
            (activeCategories.Count == 0 || activeCategories.Contains(p.category)) &&
            (activeSubcategories.Count == 0 || activeSubcategories.Contains(p.subcategory))
        );

        scrollLoader.Load(filtered);
        filterAnimator.HidePanel();
    }

    void ResetFilter()
    {
        toggleClothes.isOn = false;
        toggleWatches.isOn = false;
        toggleJewelry.isOn = false;
        toggleFootwear.isOn = false;
        toggleAccessories.isOn = false;

        toggleMale.isOn = false;
        toggleFemale.isOn = false;
        toggleKids.isOn = false;

        scrollLoader.Load(allProducts);
    }
}
