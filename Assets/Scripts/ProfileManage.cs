using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[SerializeField]
public class Profile
{
    public string Name;
}

public class ProfileManage : MonoBehaviour
{
    Profile _Profile;
    private string dataPath;
    private void Awake()
    {
        _Profile = new Profile();
        dataPath = Application.dataPath + "Profile.json";
    }

    public void ChangeProfileName(string name)
    {
        _Profile.Name = name;
        Debug.Log(_Profile.Name);
    }

    public void SaveProfiletoJson()
    {
        string saveString = JsonUtility.ToJson(_Profile, prettyPrint: true);

        File.WriteAllText(dataPath, saveString);
    }

    public void LoadProfilefromJson()
    {

        _Profile = JsonUtility.FromJson<Profile>(File.ReadAllText(dataPath));
        Debug.Log(_Profile.Name);
    }
}
