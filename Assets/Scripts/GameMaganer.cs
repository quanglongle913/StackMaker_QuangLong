using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaganer : MonoBehaviour
{
    [SerializeField] protected GameObject level1;
    [SerializeField] protected GameObject level2;
    [SerializeField] protected GameObject level3;
    [SerializeField] protected GameObject level4;
    [SerializeField] protected GameObject level5;

    protected string level = "Level";
    protected int inGameLevel;
    protected string brick = "Brick";
    protected int inGameBrick;

    protected bool isReplay;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {   
        inGameLevel = PlayerPrefs.GetInt(level, 0);
        if (inGameLevel == 1)
        {
            Instantiate(level1, new Vector3(0, 0, 0), Quaternion.Euler(0,-90,0));
            //level1.gameObject.transform.GetChild(0).gameObject.
            for (int i = 0; i < level1.gameObject.transform.childCount; i++)
            {
                if (level1.gameObject.transform.GetChild(i).name== "StartPoint")
                {
                    transform.position = level1.gameObject.transform.GetChild(i).position;
                }
            }
            //transform.position = level1StartPoint.transform.position;
        } else if (inGameLevel == 2)
        {
            Instantiate(level2, new Vector3(0, 0, 0), Quaternion.Euler(0, -90, 0));
            for (int i = 0; i < level1.gameObject.transform.childCount; i++)
            {
                if (level1.gameObject.transform.GetChild(i).name == "StartPoint")
                {
                    transform.position = level1.gameObject.transform.GetChild(i).position;
                }
            }
            //transform.position = level2StartPoint.transform.position;
        }
        else if (inGameLevel == 3)
        {
            Instantiate(level3, new Vector3(0, 0, 0), Quaternion.Euler(0, -90, 0));
            for (int i = 0; i < level1.gameObject.transform.childCount; i++)
            {
                if (level1.gameObject.transform.GetChild(i).name == "StartPoint")
                {
                    transform.position = level1.gameObject.transform.GetChild(i).position;
                }
            }
            //transform.position = level3StartPoint.transform.position;
        }
        else if (inGameLevel == 4)
        {
            Instantiate(level4, new Vector3(0, 0, 0), Quaternion.Euler(0, -90, 0));
            for (int i = 0; i < level1.gameObject.transform.childCount; i++)
            {
                if (level1.gameObject.transform.GetChild(i).name == "StartPoint")
                {
                    transform.position = level1.gameObject.transform.GetChild(i).position;
                }
            }
            //transform.position = level4StartPoint.transform.position;
        }
        else if (inGameLevel == 5)
        {
            Instantiate(level5, new Vector3(0, 0, 0), Quaternion.Euler(0, -90, 0));
            for (int i = 0; i < level1.gameObject.transform.childCount; i++)
            {
                if (level1.gameObject.transform.GetChild(i).name == "StartPoint")
                {
                    transform.position = level1.gameObject.transform.GetChild(i).position;
                }
            }
            //transform.position = level5StartPoint.transform.position;
        }
    }
}
