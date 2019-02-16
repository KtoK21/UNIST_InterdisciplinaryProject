using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasUIManage : MonoBehaviour
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

    public void OnEndEdit(string text)
    {
        GameObject.Find("Manager").GetComponent<ProfileManage>().ChangeProfileName(text);
    }

    public void SaveProfileOnClick()
    {
        GameObject.Find("Manager").GetComponent<ProfileManage>().SaveProfiletoJson();
    }

    public void LoadProfileOnClick()
    {
        GameObject.Find("Manager").GetComponent<ProfileManage>().LoadProfilefromJson();
    }
}
