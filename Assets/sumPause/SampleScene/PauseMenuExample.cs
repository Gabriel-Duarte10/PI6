using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuExample : MonoBehaviour {

    GameObject panel;
    private PlayerController _PlayerController;
    private LootLocker_Sistema _LootLocker_Sistema;
    private Boss _boss;
    private void Start()
    {
        _PlayerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _LootLocker_Sistema = FindObjectOfType(typeof(LootLocker_Sistema)) as LootLocker_Sistema;
        _boss = FindObjectOfType(typeof(Boss)) as Boss;
    }

    void Awake () {
        // Get panel object
        panel = transform.Find("PauseMenuPanel").gameObject;
        if (panel == null) {
            Debug.LogError("PauseMenuPanel object not found.");
            return;
        }

        panel.SetActive(false); // Hide menu on start
	}

    // Call from inspector button]
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(5f);
        
    }
    public void ResumeGame () {
        if(_PlayerController.HP <= 0 || _boss.HP <= 0)
        {
            Destroy(_PlayerController.gameObject);
            Destroy(_LootLocker_Sistema.gameObject);
            SumPause.Status = false;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        else
        {
            SumPause.Status = false; // Set pause status to false
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Add/Remove the event listeners
    void OnEnable() {
        SumPause.pauseEvent += OnPause;
    }

    void OnDisable() {
        SumPause.pauseEvent -= OnPause;
    }

    /// <summary>What to do when the pause button is pressed.</summary>
    /// <param name="paused">New pause state</param>
    void OnPause(bool paused) {
        if (paused) {
            // This is what we want do when the game is paused
            panel.SetActive(true); // Show menu
        }
        else {
            // This is what we want to do when the game is resumed
            panel.SetActive(false); // Hide menu
        }
    }

}
