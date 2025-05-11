using UnityEngine;
using UnityEngine.UI;

public class FilterPanelAnimator : MonoBehaviour
{
    public RectTransform filterPanel;
    public Button filterButton;
    public Button closeButton;
    public float slideDuration = 0.3f;
    public float panelHeight = 600f; // Adjust to your panel height

    private Vector2 hiddenPos;
    private Vector2 shownPos;
    private Coroutine currentAnimation;

    void Start()
    {
        hiddenPos = new Vector2(0, -panelHeight);
        shownPos = new Vector2(0, 0);
        filterPanel.anchoredPosition = hiddenPos;

        filterButton.onClick.AddListener(ShowPanel);
        closeButton.onClick.AddListener(HidePanel);
    }

    void ShowPanel()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(SlidePanel(filterPanel.anchoredPosition, shownPos));
    }

    public void HidePanel()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(SlidePanel(filterPanel.anchoredPosition, hiddenPos));
    }

    System.Collections.IEnumerator SlidePanel(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            filterPanel.anchoredPosition = Vector2.Lerp(from, to, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        filterPanel.anchoredPosition = to;
    }
}
