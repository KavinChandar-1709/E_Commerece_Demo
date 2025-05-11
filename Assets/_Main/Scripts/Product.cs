using System.Collections.Generic;

[System.Serializable]
public class Product
{
    public string id;
    public string name;
    public string category;
    public string subcategory;
    public string iconURL;
    public string modelPath;
}

[System.Serializable]
public class ProductListWrapper
{
    public List<Product> products;
}
