using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SlotSystem : MonoBehaviour
{


    public GameObject cursor;

    public int slot;

    public GameObject[] menuButtons = new GameObject[1];
    public int menuHeight;

    public GameObject[] localButtons = new GameObject[1];
    public int localHeight;

    public GameObject[] onlineButtons = new GameObject[1];
    public int onlineHeight;

    public GameObject[] optionsButtons = new GameObject[1];
    public int optionsHeight;

    public GamePadManager gamePad;

    int currentMax = 0;
    int currentHeight = 0;

    public GameObject invalidSound;

    GamePadManager.gamepadAxes[] m_InputAxes = new GamePadManager.gamepadAxes[4];

    Color temp;

    float[] m_ctrlrTimer = new float[4];

    // Use this for initialization
    void Start()
    {
        m_ctrlrTimer[0] = 0f;
        m_ctrlrTimer[1] = 0f;
        m_ctrlrTimer[2] = 0f;
        m_ctrlrTimer[3] = 0f;
        slot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuStates.menuState == 0)
        {
            currentHeight = menuHeight;
            currentMax = menuButtons.Length;
            cursor.transform.position = menuButtons[slot].transform.position;

            cursor.GetComponent<CursorSizeing>().bottomDistance.x = menuButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().bottomDistance.y = -menuButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;

            cursor.GetComponent<CursorSizeing>().topDistance.x = -menuButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().topDistance.y = menuButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        }
        if (MenuStates.menuState == 1)
        {
            if (slot == 1)
            {
                temp = GameObject.Find("Map1").GetComponent<Image>().color;
                temp.a = 0.0f;
                GameObject.Find("Map1").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map2").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map2").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map3").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map3").GetComponent<Image>().color = temp;
            }
            else if (slot == 2)
            {
                temp = GameObject.Find("Map2").GetComponent<Image>().color;
                temp.a = 0.0f;
                GameObject.Find("Map2").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map1").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map1").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map3").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map3").GetComponent<Image>().color = temp;
            }
            else if (slot == 3)
            {
                temp = GameObject.Find("Map3").GetComponent<Image>().color;
                temp.a = 0.0f;
                GameObject.Find("Map3").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map2").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map2").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map1").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map1").GetComponent<Image>().color = temp;
            }
            else
            {
                temp = GameObject.Find("Map1").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map1").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map2").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map2").GetComponent<Image>().color = temp;

                temp = GameObject.Find("Map3").GetComponent<Image>().color;
                temp.a = 1.0f;
                GameObject.Find("Map3").GetComponent<Image>().color = temp;
            }

            currentHeight = localHeight;
            currentMax = localButtons.Length;
            cursor.transform.position = localButtons[slot].transform.position;
            cursor.GetComponent<CursorSizeing>().bottomDistance.x = localButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().bottomDistance.y = -localButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;

            cursor.GetComponent<CursorSizeing>().topDistance.x = -localButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().topDistance.y = localButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        }
        for (int i = 0; i <= 3; i++)
        {
            m_ctrlrTimer[i] -= Time.deltaTime;
            m_InputAxes[i] = GamePadManager.GetControllerAxes(i);
            if (GamePadManager.GetControllerKeyPressed(i, 5) || (m_InputAxes[i].l_ThumbStick_Y <= -0.3f && m_ctrlrTimer[i] <= 0f))
            {
                Debug.Log("down");
                m_ctrlrTimer[i] = 0.15f;
                if (slot < currentMax - 1 && (slot + 1) % currentHeight != 0)
                    slot++;
            }
            else if (GamePadManager.GetControllerKeyPressed(i, 4) || (m_InputAxes[i].l_ThumbStick_Y >= 0.3f && m_ctrlrTimer[i] <= 0f))
            {
                Debug.Log("up");
                m_ctrlrTimer[i] = 0.15f;
                if (slot > 0 && (slot) % currentHeight != 0)
                    slot--;
            }
            else if (GamePadManager.GetControllerKeyPressed(i, 6) || (m_InputAxes[i].l_ThumbStick_X <= -0.3f && m_ctrlrTimer[i] <= 0f))
            {
                Debug.Log("left");
                m_ctrlrTimer[i] = 0.15f;
                if (slot - (currentHeight - 1) > 0)
                    slot -= currentHeight;
            }
            else if (GamePadManager.GetControllerKeyPressed(i, 7) || (m_InputAxes[i].l_ThumbStick_Y >= 0.3f && m_ctrlrTimer[i] <= 0f))
            {
                Debug.Log("Right");
                m_ctrlrTimer[i] = 0.15f;
                if (slot + currentHeight <= currentMax - 1)
                    slot += currentHeight;
            }
            if (GamePadManager.GetControllerKeyPressed(i, 0))
            {
                if (MenuStates.menuState == 0)
                {
                    if (slot == 0)
                    {
                        MenuStates.menuState = 1;
                        slot = 1;
                    }
                    else if (slot == 1)
                    {
                        MenuStates.menuState = 0;
                    }
                    else if (slot == 2)
                    {
                        //MenuStates.menuState = 2;
                    }
                    else if (slot == 3)
                    {
                        print("Close");
                        Application.Quit();
                    }

                }
                else if (MenuStates.menuState == 1)
                {
                    if (slot == 0)
                    {
                        MenuStates.menuState = 0;
                    }
                    if (GamePadManager.playersActive > 0)
                    {
                        if (slot == 1)
                        {
                            localButtons[1].GetComponent<LoadLevel>().LoadMap1();
                        }
                        else if (slot == 2)
                        {
                            localButtons[2].GetComponent<LoadLevel>().LoadMap2();
                        }
                        else if (slot == 3)
                        {
                            localButtons[3].GetComponent<LoadLevel>().LoadMap4();
                        }
                    }
                    else
                    {
                        Instantiate<GameObject>(invalidSound);
                    }
                    if (slot == 4)
                    {
                        gameObject.GetComponent<MenuStates>().setGunGame();
                    }

                }
            }
        }

    }
}
