using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text time;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TMP_Text placeholderQuest;

    private InputSystem_Actions inputAction;
    private List<Quest> quests;

    public bool gameIsPaused = false;

    private int goldCount = 0;
    private float timer = 0f;

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
        pausePanel.SetActive(false);
        questPanel.SetActive(false);

        quests = new List<Quest>() { // quests should probably be their own class, but this is for the sake of exemple 
            new Quest("Get 1 coin", () => goldCount >= 1), 
            new Quest("Get 5 coins", () => goldCount >= 5), 
            new Quest("Get 17 coins", () => goldCount >= 17)
        };
    }

    private void OnEnable()
    {
        inputAction.UI.Pause.Enable();
        inputAction.UI.Pause.performed += OnPausePerformed;

        inputAction.UI.Inventory.Enable();
        inputAction.UI.Inventory.performed += OnInventoryPerformed;

        Coin.OnPickup += UpdateGold;
    }

    private void OnDisable()
    {
        Coin.OnPickup -= UpdateGold;

        inputAction.UI.Inventory.performed -= OnInventoryPerformed;
        inputAction.UI.Inventory.Disable();

        inputAction.UI.Pause.performed -= OnPausePerformed;
        inputAction.UI.Pause.Disable();
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        time.text = ((int)timer).ToString().PadLeft(3, '0');
    }

    // Event consumers
    private void UpdateGold()
    {
        gold.text = ++goldCount + "$";
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (gameIsPaused)
        {
            Unpause();
        }
        else
        {
            Pause(true);
        }
    }

    private void OnInventoryPerformed(InputAction.CallbackContext context)
    {

    }

    // Pause Menu Buttons
    public void ResumeClicked()
    {
        Unpause();
    }

    public void QuestsClicked()
    { 
        questPanel.SetActive(true);
    }

    public void MainMenuClicked()
    {
        Unpause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // Misc
    public void Pause(bool showPausePanel)
    {
        pausePanel.SetActive(showPausePanel);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Unpause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
