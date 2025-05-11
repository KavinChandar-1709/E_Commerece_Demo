using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProductCard : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Image iconImage;
    public Button viewButton;

    private Product product;

    public void Setup(Product productData)
    {
        product = productData;
        titleText.text = product.name;
        StartCoroutine(LoadImage(product.iconURL));
        viewButton.onClick.AddListener(OnViewClicked);
    }

    void OnViewClicked()
    {
        UIManager.Instance.ShowObjectPanel();
        // Optional: pass data to panel if needed
    }

    IEnumerator LoadImage(string url)
    {
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = ((UnityEngine.Networking.DownloadHandlerTexture)request.downloadHandler).texture;
                iconImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}
