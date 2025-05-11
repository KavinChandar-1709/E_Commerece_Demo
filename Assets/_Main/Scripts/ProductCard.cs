using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class ProductCard : MonoBehaviour
{
    public Image productImage;
    public TextMeshProUGUI productName;

    public void Setup(Product product)
    {
        productName.text = product.name;
        StartCoroutine(LoadImageFromURL(product.iconURL));
    }

    IEnumerator LoadImageFromURL(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(request);
            productImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        }
        else
        {
            Debug.LogError("Image load failed: " + request.error);
        }
    }
}
