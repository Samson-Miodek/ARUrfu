using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICreator : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject buttonGM;
    [SerializeField] private GameObject TitleSubButtonGM;
    [SerializeField] private GameObject menuSubtitleGM;
    [SerializeField] private GameObject menuTitleGM;

    [Space]
    [Header("Menu")]
    public ButtonMemory[] buttonTiles;
    private float buttonHeight;


    private void Awake()
    {
        CreateMainMenu();
    }

    public void DebugPing()
    {
        Debug.Log("Pong!");
    }

    public void CreateMainMenu()
    {
        ClearSpace();
        StartCoroutine(MainMenuCorutine());
    }

    private IEnumerator MainMenuCorutine()
    {
        float buttonVerticalPos = 134;
        foreach (var button in buttonTiles)
        {
            yield return StartCoroutine(AddButton(button, buttonVerticalPos));
            buttonVerticalPos += 20 + buttonHeight;
        }
    }

    private void ClearSpace()
    {
        StopAllCoroutines();

        bool skipFirst = true;
        foreach (Transform item in content.GetComponentInChildren<Transform>())
        {
            if (skipFirst)
            {
                skipFirst = false;
                continue;
            }

            Destroy(item.gameObject);
        }

        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, 135);
    }
    private IEnumerator AddButton(ButtonMemory button, float pos)
    {
        var obj = Instantiate(buttonGM, content.transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, -pos);
        obj.GetComponent<Button>().onClick.AddListener(() => ButtonPush(button));
        obj.GetComponentInChildren<TMP_Text>().text = button.text;
        if (button.TitleSubtext.Length > 0)
        {
            var subObj = Instantiate(TitleSubButtonGM, obj.transform);
            subObj.GetComponent<TMP_Text>().text = button.TitleSubtext;
            subObj.transform.SetSiblingIndex(0);
        }

        yield return new WaitUntil(() => obj.GetComponent<RectTransform>().sizeDelta.y > 0);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + 20 + obj.GetComponent<RectTransform>().sizeDelta.y);
        buttonHeight = obj.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void ButtonPush(ButtonMemory button)
    {
        if (button.scene.Length > 0)
        {
            SceneManager.LoadScene(button.scene);
        }
        else if (button.menuTab.buttons.Length > 0)
        {
            //Debug.Log(menuTab.ToString());
            ClearSpace();
            StartCoroutine(CreateMenuCorutine(button.menuTab));
        }
        else Debug.Log("No Content");

    }

    private IEnumerator CreateMenuCorutine(MenuTab menuTab)
    {
        float blockYCord = 124;
        GameObject obj;
        if (menuTab.subtitle.Length > 0)
        {
            obj = Instantiate(menuSubtitleGM, content.transform);
            obj.GetComponent<TMP_Text>().text = menuTab.subtitle;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, -blockYCord);
            yield return new WaitUntil(() => obj.GetComponent<RectTransform>().sizeDelta.y > 0);
            blockYCord += obj.GetComponent<RectTransform>().sizeDelta.y;
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + 20 + obj.GetComponent<RectTransform>().sizeDelta.y);

        }
        if (menuTab.title.Length > 0)
        {
            obj = Instantiate(menuTitleGM, content.transform);
            obj.GetComponent<TMP_Text>().text = menuTab.title;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, -blockYCord);
            yield return new WaitUntil(() => obj.GetComponent<RectTransform>().sizeDelta.y > 0);
            blockYCord += obj.GetComponent<RectTransform>().sizeDelta.y;
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + 20 + obj.GetComponent<RectTransform>().sizeDelta.y);
        }
        blockYCord += 20;
        foreach (var button in menuTab.buttons)
        {
            yield return StartCoroutine(AddButton(button, blockYCord));
            blockYCord += 20 + buttonHeight;
        }
    }
}


[Serializable]
public class ButtonMemory
{
    [SerializeField] public string text;
    [SerializeField] public string TitleSubtext;
    //[SerializeField] public bool isLoadScene; Фуфло
    [SerializeField] public MenuTab menuTab;
    [SerializeField] public string scene;


    public ButtonMemory(string text, MenuTab menuTab)
    {
        this.text = text;
        this.menuTab = menuTab;
    }
    public ButtonMemory(string text, string scene)
    {
        this.text = text;
        this.scene = scene;
    }
}
[Serializable]
public class MenuTab
{
    [SerializeField] public string subtitle;
    [SerializeField] public string title;
    [SerializeField] public ButtonMemory[] buttons;

    public MenuTab(string title, ButtonMemory[] buttons)
    {
        this.title = title;
        this.buttons = buttons;
    }

    public override string ToString()
    {
        return "Заголовок: " + title + "\nКол-во кнопок: " + buttons.Length;
    }

}



//[CustomEditor(typeof(ButtonMemory))]
//public class MyCustomEditor : Editor
//{
//    ButtonMemory _target;

//    private void OnEnable()
//    {
//        _target = target as ButtonMemory;
//    }

//    public override void OnInspectorGUI()
//    {
//        _target.isLoadScene = EditorGUILayout.Toggle("Показать первый?", _target.isLoadScene);
//        if (_target.isLoadScene)
//        {
//            _target.menuTab = EditorGUILayout.ObjectField(_target.menuTab,typeof(ButtonMemory[]));
//        }
//        else
//        {
//            _target.scene = EditorGUILayout.TextField("Второе поле: ", _target.scene);
//        }
//    }
//}
