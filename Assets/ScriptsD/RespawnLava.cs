using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnLava : MonoBehaviour
{
    public string respawnPointTag = "respawnPoint";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        
        if (other.CompareTag("lava"))
        {
            Debug.Log("Lava");
            Respawn();
        }
        else if (other.CompareTag("finish"))
        {
            Debug.Log("finish");
            LoadCutscene();
        }
    }

    void Respawn()
    {
        Debug.Log("Respawn");
        ReloadScene();
        // You can customize the respawn logic here.
        // For example, finding the respawn point based on tag.
        GameObject respawnPoint = GameObject.FindGameObjectWithTag(respawnPointTag);
        transform.position = respawnPoint.transform.position;

        if (respawnPoint != null)
        {
            // Move the character to the respawn point.
            transform.position = respawnPoint.transform.position;
        }
        else
        {
            // If no respawn point is found, reload the current scene.
            
        }
    }

    void ReloadScene()
    {
        // Reload the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    void LoadCutscene()
    {
        // Load the "Cutscene" scene.
        SceneManager.LoadScene("Cutscene");
    }
}
