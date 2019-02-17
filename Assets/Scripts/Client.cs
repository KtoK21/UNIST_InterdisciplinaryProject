using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Client : MonoBehaviour
{
    private bool IsSocketReady = false;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    //Connect client socket to server
    //Return true if functions works
    //Return false 1)if function does nothing, 2)connection failed
    public bool ConnectToServer(string host, int port)
    {
        if (IsSocketReady)
            return false;
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            IsSocketReady = true;
        }
        catch (Exception e)
        {
                Debug.Log("Socket error : " + e.Message);
        }

        return IsSocketReady;
    }

    private void Update()
    {
        if (IsSocketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if(data != null)
                {
                    OnIncomingDate(data);
                }
            }
        }   
    }

    //Send message to server
    public void Send(string data)
    {
        if (!IsSocketReady)
            return;

        writer.WriteLine(data);
        writer.Flush();
    }

    //Read message from server
    private void OnIncomingDate(string data)
    {
        Debug.Log(data);
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }

    private void CloseSocket()
    {
        if (!IsSocketReady)
            return;

        writer.Close();
        reader.Close();
        socket.Close();
        IsSocketReady = false;

    }
}

public class GameClient
{
    public string name;
    public bool isHost;
}