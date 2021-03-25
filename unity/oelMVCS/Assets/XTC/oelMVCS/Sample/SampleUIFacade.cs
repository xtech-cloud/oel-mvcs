using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XTC.oelMVCS;


[System.Serializable]
public class LoginUI
{
	public GameObject root;
	public Text txtTip;
	public InputField inputUsername;
	public InputField inputPassword;
	public Button btnLogin;
}

[System.Serializable]
public class HomeUI
{
	public GameObject root;
}

public class SampleUIFacade : UIFacade 
{
	public LoginUI uiLogin;
	public HomeUI uiHome;
}
