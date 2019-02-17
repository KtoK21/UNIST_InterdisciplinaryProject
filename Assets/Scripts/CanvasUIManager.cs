using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasUIManager : MonoBehaviour
{
    public void ExitGameOnClick()
    {
        Application.Quit();
    }

    public void SceneLoadOnClick(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ObjectToggleOnClick(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void SaveProfileOnClick()
    {
        GetComponent<ProfileManager>().SaveProfiletoJson();
    }

    public void LoadProfileOnClick()
    {
        GetComponent<ProfileManager>().LoadProfilefromJson();
    }

    public void JoinServerOnClick()
    {
        GetComponent<NetworkManager>().ConnectToServer();
        
    }

    public void HostServerOnClick()
    {
        GetComponent<NetworkManager>().HostServer();
    }

    public void NameOnEndEdit(string text)
    {
        GetComponent<ProfileManager>().SetProfileName(text);
    }

    public void AddrOnEndEdit(string text)
    {
        GetComponent<NetworkManager>().SetHostAddress(text);
    }
}
    /*How to manage similiar function like this?
     *Is current version(one function for one UI) is efficient?
     * Or this version(receive gameobject and distinguish using switch) is better?
    public void OnEndEdit(GameObject obj)
    {
        switch (obj.name)
        {
            case "NameInputField":
                GameObject.Find("Managers").GetComponent<ProfileManager>().ChangeProfileName(obj.GetComponent<InputField>().text);
                break;
            case "HostAddrInputField":
                GameObject.Find("Managers").GetComponent<NetworkManager>().SetHostAddress(obj.GetComponent<InputField>().text);
                break;
        }
    }
    */
