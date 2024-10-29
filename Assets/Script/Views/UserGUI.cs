using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    public string gameMessage;
    GUIStyle style, bigstyle;
    // Start is called before the first frame update
    void Start()
    {

        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;

        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        bigstyle = new GUIStyle();
        bigstyle.normal.textColor = Color.white;
        bigstyle.fontSize = 50;



    }

    // Update is called once per frame
    void OnGUI()
    {
        userAction.Check();
        GUI.Label(new Rect(320, Screen.height * 0.1f, 50, 200), "Preists and Devils", bigstyle);
        GUI.Label(new Rect(450, 100, 50, 200), gameMessage, style);


    }
}
