using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    /*public static UIManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
            }
            return instance;
        }
    }*/
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] Text brickText;
    [SerializeField] Text levelText;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject replayButton;

    public void SetBrick(int brick)
    {
        brickText.text = brick.ToString();
    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }
    public void isNextButton(bool isBool)
    {
        nextButton.gameObject.SetActive(isBool);
    }
    public void isReplayButton(bool isBool)
    {
        replayButton.gameObject.SetActive(isBool);
    }
}
