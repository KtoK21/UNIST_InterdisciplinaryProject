using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.IO;

public class Server : MonoBehaviour
{
    public int port = 6321;

    //List of clients that is connected at least once. Added when client is first accepted
    private List<ServerClient> clients;
    //List of clients that is disconnected.
    private List<ServerClient> disconnectList;

    private TcpListener server;
    private bool IsServerStarted;

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            StartListening();
            IsServerStarted = true;
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    private void Update()
    {
        if (!IsServerStarted)
            return;

        foreach(ServerClient c in clients)
        {
            if (!IsConnected(c.tcp)){
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            }

            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();
                    if (data != null)
                        OnIncomingData(c, data);
                }
            }
        }

        for(int i=0; i< disconnectList.Count - 1; i++)
        {
            clients.Remove(disconnectList[i]);
            disconnectList.RemoveAt(i);
        }
    }

    private void StartListening()
    {
        //HandShake
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
        sc.clientName = clients.Count.ToString();
        clients.Add(sc);
        Debug.Log("Accept TCP Client");
    }

    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                /* pear to the documentation on Poll:
                * When passing SelectMode.SelectRead as a parameter to the Poll method it will return 
                * -either- true if Socket.Listen(Int32) has been called and a connection is pending;
                * -or- true if data is available for reading; 
                * -or- true if the connection has been closed, reset, or terminated; 
                * otherwise, returns false
                */
                if (c.Client.Poll(0, SelectMode.SelectRead))
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);

                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }


    //Send message to clients in list
    private void Broadcast(string data, List<ServerClient> list)
    {
        foreach(ServerClient sc in list)
        {
            try
            {
                StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error : " + e.Message);
            }
        }
    }
    
    //Receive message from specific client
    private void OnIncomingData(ServerClient c, string data)
    {
        Debug.Log(c.clientName + " : " + data);
    }

}

//Client instance to save it server-side
public class ServerClient
{
    public string clientName;
    public TcpClient tcp;

    public ServerClient(TcpClient tcp)
    {
        this.tcp = tcp;
    }
}
