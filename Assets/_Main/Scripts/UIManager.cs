using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public ObjectPanelAnimator objectPanelAnimator; // Reference to Object Panel animator

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowObjectPanel()
    {
        objectPanelAnimator.ShowPanel();
    }

    public void HideObjectPanel()
    {
        objectPanelAnimator.HidePanel();
    }
}
