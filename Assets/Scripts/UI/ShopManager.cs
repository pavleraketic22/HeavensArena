using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }
}
