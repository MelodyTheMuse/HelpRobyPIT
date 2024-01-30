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
    static float moveSpeed = 2f;
    Rigidbody rb;
    public GameObject player;

    private UdpClient udpClient;

    void Start()
    {
        // Check for network availability
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork ||
            Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
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
        else
        {
            Debug.LogError("No network connection available.");
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
        float forceMagnitude = forceMultiplier * moveSpeed;

        if (movement.Contains("Forward"))
        {
            rb.AddForce(transform.forward * forceMagnitude, ForceMode.Impulse);
        }
        else if (movement.Contains("Backwards"))
        {
            rb.AddForce(transform.forward * -forceMagnitude, ForceMode.Impulse);
        }
        else if (movement.Contains("Left"))
        {
            //rb.AddForce(transform.right * -forceMagnitude, ForceMode.Impulse);
            player.transform.Rotate(0, -15, 0);
        }
        else if (movement.Contains("Right"))
        {
            //rb.AddForce(transform.right * forceMagnitude, ForceMode.Impulse);
            player.transform.Rotate(0, 15, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        // Dead-zone to ignore small input changes
        if (movement.Length < 10)
        {
            return;
        }

        // Rotate the player based on input
        //if (movement.Contains("Left"))
        //{
        //    player.transform.Rotate(0, -15, 0);
        //}
        //else if (movement.Contains("Right"))
        //{
        //    player.transform.Rotate(0, 15, 0);
        //}
    }
}
