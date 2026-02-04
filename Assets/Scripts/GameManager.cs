using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button colorButton;

    private readonly Color[] _colorPool = new Color[]
    {
        new Color(1f, 0.42f, 0.42f),    // #FF6B6B
        new Color(0.31f, 0.80f, 0.77f),  // #4ECDC4
        new Color(0.27f, 0.72f, 0.82f),  // #45B7D1
        new Color(0.59f, 0.81f, 0.70f),  // #96CEB4
        new Color(1f, 0.92f, 0.65f),     // #FFEAA7
        new Color(0.87f, 0.63f, 0.87f),  // #DDA0DD
        new Color(1f, 0.55f, 0f)         // #FF8C00
    };

    private void Start()
    {
        if (colorButton != null)
        {
            colorButton.onClick.AddListener(OnColorButtonClicked);
        }
    }

    private void OnColorButtonClicked()
    {
        if (titleText == null) return;
        
        Color currentColor = titleText.color;
        Color newColor;
        
        do
        {
            newColor = _colorPool[Random.Range(0, _colorPool.Length)];
        } while (newColor == currentColor && _colorPool.Length > 1);
        
        titleText.color = newColor;
    }
}
