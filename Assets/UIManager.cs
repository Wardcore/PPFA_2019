using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text m_consoleTxt;
    public Text m_actionTxt;
    static private UIManager s_instance = null;

	static public UIManager GetInstance()
	{
		return s_instance;
	}

	void Awake()
	{
#if UNITY_EDITOR
		if (s_instance != null)
			Debug.LogError ("There's more than one UI Manager instance in the scene");
#endif

		s_instance = this;
        
        m_consoleTxt.text = "Messages :";
        m_actionTxt.text = "";
	}

    public static void WriteConsoleMessage(string message){
        GetInstance().m_consoleTxt.text = "\n" + message;
    }

    public static void WriteActionMessage(string message){
        GetInstance().m_actionTxt.text = message;
    }
}
