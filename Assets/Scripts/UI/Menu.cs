using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuType { Loading, Title, ServerList }

public class Menu : MonoBehaviour
{
    public MenuType menuType;
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
