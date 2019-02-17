using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NetworkManager : MonoBehaviour
{
    public GameObject ServerPrefab;
    public GameObject ClientPrefab;

    private string HostAddress = "127.0.0.1";

    public void ConnectToServer()
    {
        try
        {
            Client c = Instantiate(ClientPrefab).GetComponent<Client>();
            c.ConnectToServer(HostAddress, 6321);
            c.Send(GetComponent<ProfileManager>().GetProfileName());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void HostServer()
    {
        try
        {
            Server s = Instantiate(ServerPrefab).GetComponent<Server>();
            s.Init();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void SetHostAddress(string addr)
    {
        HostAddress = addr;
    }

}
