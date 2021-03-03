using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu[] menuObjects;
    private Dictionary<MenuType, Menu> menus;
    private MenuType currentMenu;

    private static MenuManager instance;
    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MenuManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        menus = new Dictionary<MenuType, Menu>();
        foreach (Menu m in menuObjects)
        {
            menus.Add(m.menuType, m);
        }
        OpenMenu(MenuType.Loading);
    }

    public void OpenMenu(MenuType _type)
    {
        if (currentMenu == _type)
        {
            return;
        }
        CloseMenu();
        menus[_type].Open();
        currentMenu = _type;
    }

    public void CloseMenu()
    {
        menus[currentMenu].Close();
    }
}
