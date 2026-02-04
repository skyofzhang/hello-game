using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Creates MainScene per SETUP_INSTRUCTIONS.md (HG-001).
/// Menu: Tools > Hello Game > Create Main Scene
/// </summary>
public static class CreateMainScene
{
    private const string ScenePath = "Assets/Scenes/MainScene.unity";

    [MenuItem("Tools/Hello Game/Create Main Scene")]
    public static void Create()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Canvas: Screen Space Overlay, Scale With Screen Size 1080x1920
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
        canvasGO.AddComponent<GraphicRaycaster>();

        var canvasRect = canvasGO.GetComponent<RectTransform>();
        if (canvasRect == null) canvasRect = canvasGO.AddComponent<RectTransform>();
        canvasRect.anchorMin = Vector2.zero;
        canvasRect.anchorMax = Vector2.one;
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;

        // BackgroundImage: full stretch, #1a1a2e
        var bgGO = new GameObject("BackgroundImage");
        bgGO.transform.SetParent(canvasGO.transform, false);
        var bgImage = bgGO.AddComponent<Image>();
        bgImage.color = new Color32(0x1a, 0x1a, 0x2e, 255);
        var bgRect = bgGO.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        // TitleText: TMP "Hello Game", 72, white, center, anchor (0.1,0.45)-(0.9,0.6)
        var titleGO = new GameObject("TitleText");
        titleGO.transform.SetParent(canvasGO.transform, false);
        var titleTMP = titleGO.AddComponent<TextMeshProUGUI>();
        titleTMP.text = "Hello Game";
        titleTMP.fontSize = 72;
        titleTMP.color = Color.white;
        titleTMP.alignment = TextAlignmentOptions.Center;
        var titleRect = titleGO.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.1f, 0.45f);
        titleRect.anchorMax = new Vector2(0.9f, 0.6f);
        titleRect.offsetMin = Vector2.zero;
        titleRect.offsetMax = Vector2.zero;
        titleRect.pivot = new Vector2(0.5f, 0.5f);

        // ColorButton: anchor (0.3,0.12)-(0.7,0.18), #4A90D9, child "Change Color" 28
        var btnGO = new GameObject("ColorButton");
        btnGO.transform.SetParent(canvasGO.transform, false);
        var btnImage = btnGO.AddComponent<Image>();
        btnImage.color = new Color32(0x4A, 0x90, 0xD9, 255);
        btnGO.AddComponent<Button>();
        var btnRect = btnGO.GetComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0.3f, 0.12f);
        btnRect.anchorMax = new Vector2(0.7f, 0.18f);
        btnRect.offsetMin = Vector2.zero;
        btnRect.offsetMax = Vector2.zero;
        btnRect.pivot = new Vector2(0.5f, 0.5f);

        var btnTextGO = new GameObject("ButtonText");
        btnTextGO.transform.SetParent(btnGO.transform, false);
        var btnText = btnTextGO.AddComponent<TextMeshProUGUI>();
        btnText.text = "Change Color";
        btnText.fontSize = 28;
        btnText.color = Color.white;
        btnText.alignment = TextAlignmentOptions.Center;
        var btnTextRect = btnTextGO.GetComponent<RectTransform>();
        btnTextRect.anchorMin = Vector2.zero;
        btnTextRect.anchorMax = Vector2.one;
        btnTextRect.offsetMin = Vector2.zero;
        btnTextRect.offsetMax = Vector2.zero;

        // GameManager: empty GO + GameManager.cs, wire titleText & colorButton
        var gmGO = new GameObject("GameManager");
        var gm = gmGO.AddComponent<GameManager>();
        var so = new SerializedObject(gm);
        so.FindProperty("titleText").objectReferenceValue = titleTMP;
        so.FindProperty("colorButton").objectReferenceValue = btnGO.GetComponent<Button>();
        so.ApplyModifiedPropertiesWithoutUndo();

        // Ensure EventSystem exists
        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            var esGO = new GameObject("EventSystem");
            esGO.AddComponent<UnityEngine.EventSystems.EventSystem>();
            esGO.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        EditorSceneManager.SaveScene(scene, ScenePath);
        AssetDatabase.Refresh();
        Debug.Log("MainScene created and saved: " + ScenePath);
    }
}
