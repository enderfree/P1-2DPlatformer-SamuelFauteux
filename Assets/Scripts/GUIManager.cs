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
    [SerializeField] private GameObject questBoard;
    [SerializeField] private TMP_Text placeholderQuest;

    private InputSystem_Actions inputAction;
    private List<Quest> quests;
    private Dictionary<Quest, TMP_Text> questToObjectDict;

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
        questToObjectDict = new Dictionary<Quest, TMP_Text>();

        // Instanciate Quests
        for (int i = 0; i < quests.Count; ++i)
        {
            TMP_Text tmp_quest;

            if (i == 0)
            {
                tmp_quest = placeholderQuest;
            }
            else
            {
                tmp_quest = Instantiate(placeholderQuest, questBoard.transform);
            }

            tmp_quest.text = quests[i].Display;

            questToObjectDict.Add(quests[i], tmp_quest);
        }
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

    // I've put the quests in the pause menu because it looked bad when always in sight
    // but ironically, I think my way is less ressource intensive as I don't have a loop 
    // in LateUpdate anymore!
    public void QuestsClicked()
    { 
        questPanel.SetActive(true);

        foreach(Quest quest in quests)
        {
            if (quest.DisplaycompletionCondition.Invoke())
            {
                questToObjectDict[quest].fontStyle |= FontStyles.Strikethrough;
            }
        }
    }

    public void MainMenuClicked()
    {
        Unpause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // // Quest Menu Buttons
    public void BackClicked()
    {
        questPanel.SetActive(false);
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
        questPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
