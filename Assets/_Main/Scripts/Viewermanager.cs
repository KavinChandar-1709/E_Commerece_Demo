using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Viewermanager : MonoBehaviour
{
    public Button Viewin3DBtn;
    public Button closeBtn;

    public GameObject panel3D;
    public GameObject canvasUI;

    private void Start()
    {
        Viewin3DBtn.onClick.AddListener(Show3D);
        closeBtn.onClick.AddListener(close3D);
    }

    public void Show3D()
    {
        canvasUI.SetActive(false);
        panel3D.SetActive(true);
    }

    public void close3D()
    {
        canvasUI.SetActive(true);
        panel3D.SetActive(false);
    }
}
