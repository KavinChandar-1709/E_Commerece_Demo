using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProductUIManager : MonoBehaviour
{
    public GameObject productCardPrefab;
    public Transform contentHolder;

    // Main category toggles
    public Toggle toggleClothes;
    public Toggle toggleWatches;
    public Toggle toggleJewelry;
    public Toggle toggleFootwear;
    public Toggle toggleAccessories;

    // Subcategory toggles
    public Toggle toggleMale;
    public Toggle toggleFemale;
    public Toggle toggleKids;

    // Buttons
    public Button applyButton;
    public Button resetButton;

    // Filter panel animator reference
    public FilterPanelAnimator filterAnimator;

    private List<Product> allProducts;

    void Start()
    {
        allProducts = ProductDataLoader.LoadProducts();
        DisplayProducts(allProducts); // Show all at start

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

        // Filter logic
        List<Product> filtered = allProducts.FindAll(p =>
            (activeCategories.Count == 0 || activeCategories.Contains(p.category)) &&
            (activeSubcategories.Count == 0 || activeSubcategories.Contains(p.subcategory))
        );

        DisplayProducts(filtered);

        // Close panel
        filterAnimator.HidePanel();
    }

    void ResetFilter()
    {
        // Turn off all toggles
        toggleClothes.isOn = false;
        toggleWatches.isOn = false;
        toggleJewelry.isOn = false;
        toggleFootwear.isOn = false;
        toggleAccessories.isOn = false;

        toggleMale.isOn = false;
        toggleFemale.isOn = false;
        toggleKids.isOn = false;

        DisplayProducts(allProducts); // Reset product list
    }

    void DisplayProducts(List<Product> products)
    {
        foreach (Transform child in contentHolder)
            Destroy(child.gameObject);

        foreach (var p in products)
        {
            GameObject card = Instantiate(productCardPrefab, contentHolder);
            card.GetComponent<ProductCard>().Setup(p);
        }
    }
}
