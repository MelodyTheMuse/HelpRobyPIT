using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System;

public class CommunicationUDP : MonoBehaviour
{
    public int port = 30000;  // Match the port number with the one used on the ESP32
    public float forceMultiplier = 1f;
    public float stopForceMultiplier = 1f;
    public float moveSpeed = 5f;
    Rigidbody rb;
    public GameObject player;

    private UdpClient udpClient;

    void Start()
    {
        try
        {
            udpClient = new UdpClient(port);
            StartCoroutine(ReceiveData());
            rb = GetComponent<Rigidbody>();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception in Start: " + e.Message);
        }
    }

    IEnumerator ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref anyIP);

                string message = Encoding.UTF8.GetString(data);
                Debug.Log("Received: " + message);

                Move(message);
            }
            catch (Exception e)
            {
                Debug.LogError("Exception in ReceiveData: " + e.Message);
            }

            yield return null;
        }
    }

    void OnDisable()
    {
        try
        {
            if (udpClient != null)
                udpClient.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception in OnDisable: " + e.Message);
        }
    }

    void Move(string movement)
    {
        if (movement.Contains("Forward"))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            player.transform.Rotate(0, 0, 0);
        }
        else if(movement.Contains("Backwards"))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            player.transform.Rotate(0, 0, 0);
        }
        else if (movement.Contains("Left"))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            player.transform.Rotate(0, -15, 0);

        }
        else if (movement.Contains("Right"))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            player.transform.Rotate(0, 15, 0);
        }
        else
        {

        }
    }
}
