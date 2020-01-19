using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject playerObject;
    public ScenePlayerController scenePlayerController;
    public PlayerController playerController;
    public GameObject textUI;

    private void Start()
    {
        scenePlayerController = playerObject.GetComponent<ScenePlayerController>();
        playerController = playerObject.GetComponent<PlayerController>();
    }

    public void TriggerScene()
    {
        playerController.enabled = false;
        scenePlayerController.enabled = true;
    }

    public void SceneHappened()
    {
        playerController.enabled = true;
        scenePlayerController.enabled = false;

        if (SceneManager.GetActiveScene().name == "Press button")
        {
            playerController.pressEToHide = false;
            textUI.SetActive(false);

            StartCoroutine(LoadScene("Free movement"));
        }
        else
        {
            StartCoroutine(LoadScene("Press button"));
        }
    }

    IEnumerator LoadScene(string name)
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
