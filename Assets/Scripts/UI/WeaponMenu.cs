using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class WeaponMenu : MonoBehaviour
{

    public List<Toggle> weaponButtons = new List<Toggle>();

    public List<Toggle> selectedButtons = new List<Toggle>();

    public List<SpriteRenderer> weaponImageBoxes = new List<SpriteRenderer>();

    public GameObject player;

    public Sprite blank;

    // Start is called before the first frame update
    void Update()
    {
        int delta = weaponButtons.Count - 1;
        if (delta == -1)
        {
            delta = 0;
        }

        for (int i = weaponButtons.Count - 1; i > -1; i--)
        {
            if (weaponButtons[i].isOn == true)
            {
                if (selectedButtons.Count < weaponButtons.Count)
                {

                    if (selectedButtons.Count == 0)
                    {
                        selectedButtons.Add(weaponButtons[i]);

                        UpdateImages();
                    }

                    bool b = true;
                    for (int a = 0; a < selectedButtons.Count; a++)
                    {

                        if (selectedButtons[a] == weaponButtons[i])
                        {
                            b = false;
                        }
                    }
                    if (b == true)
                    {
                        selectedButtons.Add(weaponButtons[i]);

                        UpdateImages();
                    }
                }
                else
                {
                    for (int a = selectedButtons.Count - 1; a > -1; a--)
                    {
                        selectedButtons[a].isOn = false;
                        selectedButtons.RemoveAt(a);
                        UpdateImages();
                        return;
                    }
                }
            }
            else if (weaponButtons[i].isOn == false)
            {
                for (int a = 0; a < selectedButtons.Count; a++)
                {
                    if (selectedButtons[a] == weaponButtons[i])
                    {
                        selectedButtons.RemoveAt(a);
                        UpdateImages();
                    }
                }
            }
        }
    }

    public void Next()
    {
        for (int a = selectedButtons.Count - 1; a > -1; a--)
        {
            selectedButtons[a].GetComponent<StoreSelectButtonVars>().gameWeapon.transform.SetAsFirstSibling();
        }

        this.gameObject.SetActive(false);
    }

    public void UpdateImages()
    {
        for (int a = 0; a < selectedButtons.Count; a++)
        {
            if (weaponImageBoxes[a] != null)
            {
                weaponImageBoxes[a].sprite = selectedButtons[a].GetComponent<StoreSelectButtonVars>().gameWeapon.GetComponent<SpriteRenderer>().sprite;
            }
        }

        if (selectedButtons.Count > weaponImageBoxes.Count)
        {
            for (int a = 2; a > selectedButtons.Count - 1; a--)
            {
                if (weaponImageBoxes[a] != null)
                {
                    weaponImageBoxes[a].sprite = blank;
                }
            }
        }
    }
}
