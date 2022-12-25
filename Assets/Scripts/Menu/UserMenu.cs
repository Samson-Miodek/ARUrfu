using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using static UnityEngine.UI.Button;

public class UserMenu : MonoBehaviour
{
    [SerializeField] GameObject userMenuItemGM;
    [Space]
    [SerializeField] private MenuItem[] MenuItems;
    [SerializeField] private float startButtonPos;
    [SerializeField] private int menuWidth;
    [SerializeField] private bool isLeftMenu;
    private bool isMenuOpend;
    private double sensitivity = 0.009;

    void Awake()
    {
        UserMenuCreator();
        if (isLeftMenu)
            menuWidth *= -1;
    }
    
    private void UserMenuCreator()
    {
        float itemHeight = startButtonPos;
        foreach (var item in MenuItems)
        {
            
            var obj = Instantiate(userMenuItemGM, transform);
            obj.GetComponentInChildren<TMP_Text>().text = item.title;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, -itemHeight);
            itemHeight += 10 + 40;
        }
    }
    public void ToggleMenu()
    {
        if (isMenuOpend) StartCoroutine(CloseMenu());
        else StartCoroutine(OpenMenu());
    }

    private IEnumerator OpenMenu()
    {

        for (float i = 0; i < 1; i+=Time.deltaTime/3)
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, new Vector2(0, 0),i);
            if (Math.Abs(GetComponent<RectTransform>().anchoredPosition.x) < sensitivity) break;
            yield return null;
        }

        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        isMenuOpend = true;
    }

    private IEnumerator CloseMenu()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / 2)
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, new Vector2(menuWidth, 0), i);
            if (Math.Abs(menuWidth - GetComponent<RectTransform>().anchoredPosition.x) < sensitivity) break;
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(menuWidth, 0);
        isMenuOpend = false;
    }
}

[Serializable]
public class MenuItem
{
    public string title;
    public ButtonClickedEvent Methods;
}