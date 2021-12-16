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
        bool doUpdate = true;

        for (int i = weaponButtons.Count - 1; i > -1; i--)
        {
            if (weaponButtons[i].isOn == true)
            {
                if (selectedButtons.Count < weaponImageBoxes.Count + 1)
                {

                    if (selectedButtons.Count == 0)
                    {
                        selectedButtons.Add(weaponButtons[i]);

                        //UpdateImages();
                        doUpdate = false;

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
                        doUpdate = false;


                    }
                }
                else
                {
                    for (int a = selectedButtons.Count - 1; a > -1; a--)
                    {
                        if (selectedButtons[a] != null)
                        {
                            selectedButtons[a].isOn = false;
                            selectedButtons.RemoveAt(a);
                            doUpdate = false;

                            //UpdateImages();
                        }
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
                        weaponImageBoxes[a].sprite = blank;
                        selectedButtons.RemoveAt(a);
                        //UpdateImages();
                        doUpdate = false;

                        return;
                    }
                }
            }
        }

        if (doUpdate == true)
        {
            UpdateImages();
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
            if (weaponImageBoxes[a] != null && selectedButtons[a] != null)
            {
                weaponImageBoxes[a].sprite = selectedButtons[a].GetComponent<StoreSelectButtonVars>().gameWeapon.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                return;
            }
        }

        if (selectedButtons.Count > weaponImageBoxes.Count)
        {
            for (int a = selectedButtons.Count - 1; a > -1; a--)
            {
                if (weaponImageBoxes[a] != null)
                {
                    weaponImageBoxes[a].sprite = blank;
                }
                else
                {
                    return;
                }
            }
        }

        if (selectedButtons.Count == 0)
        {
            for (int a = 0; a < weaponImageBoxes.Count; a++)
            {
                weaponImageBoxes[a].sprite = blank;
            }
        }

        if (weaponImageBoxes.Count > selectedButtons.Count)
        {
            for (int a = 0; a < weaponImageBoxes.Count - selectedButtons.Count; a++)
            {
                weaponImageBoxes[weaponImageBoxes.Count - a - 1].sprite = blank;

                //if (selectedButtons[weaponImageBoxes.Count - a] != null)
                //{
                    //a = Mathf.Infinity;
                //}
            }
        }
    }
}
