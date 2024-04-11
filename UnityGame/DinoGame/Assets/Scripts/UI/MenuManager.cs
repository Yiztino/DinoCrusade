using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menus; 

    public void ActivateMenu(int menuIndex)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == menuIndex);
        }
    }
}
