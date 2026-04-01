using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinSelectorManager : MonoBehaviour
{
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private GameObject skinSelectionPanel;

    [Header("Tooltip")]
    [SerializeField] private TMP_Text tooltipTitle;
    [SerializeField] private TMP_Text tooltipDescription;

    [Header("Buttons")]
    [SerializeField] private Button blueButton;
    [SerializeField] private Button greenButton;
    [SerializeField] private Button redButton;
    [SerializeField] private Button blackButton;

    [Header("Selectors")] // idk why, my unity is unable to serialize the UI.Image directly...
    [SerializeField] private GameObject blueSelector;
    [SerializeField] private GameObject greenSelector;
    [SerializeField] private GameObject redSelector;
    [SerializeField] private GameObject blackSelector;

    private Image blueSelectorImage;
    private Image greenSelectorImage;
    private Image redSelectorImage;
    private Image blackSelectorImage;

    [Header("Skins")]
    [SerializeField] private Skin blueSkin;
    [SerializeField] private Skin greenSkin;
    [SerializeField] private Skin redSkin;
    [SerializeField] private Skin blackSkin;

    private InputSystem_Actions inputAction;
    private MethodInfo isHighlighted; // sorry, didn't find a better way to get that info

    private bool skinSelectorIsOpen = false;
    private string tooltipActive = "";

    // is skin unlocked
    private bool blueSkinUnlocked = false;
    private bool greenSkinUnlocked = false;
    private bool redSkinUnlocked = false;
    private bool blackSkinUnlocked = false;

    private void Awake()
    {
        // get non-serialized variables
        inputAction = new InputSystem_Actions();
        isHighlighted = typeof(Selectable).GetMethod("IsHighlighted", BindingFlags.Instance | BindingFlags.NonPublic);

        // get selector Images since I couldn't get them through Serialized fields
        blueSelectorImage = blueSelector.GetComponent<Image>();
        greenSelectorImage = greenSelector.GetComponent<Image>();
        redSelectorImage = redSelector.GetComponent<Image>();
        blackSelectorImage = blackSelector.GetComponent<Image>();

        // get defaults
        DisableAllSelectors();
        blueSelectorImage.enabled = true;
        tooltipActive = "blue";

        // disable stuff
        skinSelectionPanel.SetActive(false);
    }

    private void OnEnable()
    {
        inputAction.UI.Inventory.Enable();
        inputAction.UI.Inventory.performed += OnInventoryPerformed;
    }

    private void OnDisable()
    {
        inputAction.UI.Inventory.performed -= OnInventoryPerformed;
        inputAction.UI.Inventory.Disable();
    }
    
    private void Update()
    {
        if (skinSelectorIsOpen)
        {
            if ((bool)isHighlighted.Invoke(blueButton, null))
            {
                if (tooltipActive != "blue")
                {
                    tooltipActive = "blue";
                    DrawTooltip();
                }
            }
            else if ((bool)isHighlighted.Invoke(greenButton, null))
            {
                if (tooltipActive != "green")
                {
                    tooltipActive = "green";
                    DrawTooltip();
                }
            }
            else if ((bool)isHighlighted.Invoke(redButton, null))
            {
                if (tooltipActive != "red")
                {
                    tooltipActive = "red";
                    DrawTooltip();
                }
            }
            else if ((bool)isHighlighted.Invoke(blackButton, null))
            {
                if (tooltipActive != "black")
                {
                    tooltipActive = "black";
                    DrawTooltip();
                }
            }
            else
            {
                if (blueSelectorImage.enabled)
                {
                    if (tooltipActive != "blue")
                    {
                        tooltipActive = "blue";
                        DrawTooltip();
                    }
                }
                else if (greenSelectorImage.enabled)
                {
                    if (tooltipActive != "green")
                    {
                        tooltipActive = "green";
                        DrawTooltip();
                    }
                }
                else if (redSelectorImage.enabled)
                {
                    if (tooltipActive != "red")
                    {
                        tooltipActive = "red";
                        DrawTooltip();
                    }
                }
                else if (blackSelectorImage.enabled)
                {
                    if (tooltipActive != "black")
                    {
                        tooltipActive = "black";
                        DrawTooltip();
                    }
                }
                else
                {
                    Debug.LogWarning("SkinSelectorManager/Update: " +
                        "Skin Panel is open, but nothing is higlighted or has an active selector. " +
                        "It is likely due to a faulty new implementation.");
                    tooltipActive = "";
                }
            }
        }
    }

    // Event Consumers
    private void OnInventoryPerformed(InputAction.CallbackContext context)
    {
        if (skinSelectorIsOpen)
        {
            skinSelectionPanel.SetActive(false);
            skinSelectorIsOpen = false;
        }
        else
        {
            skinSelectionPanel.SetActive(true);
            skinSelectorIsOpen = true;
        }
    }

    // Buttons
    public void BlueButtonClicked()
    {
        if (blueSkinUnlocked)
        {
            DisableAllSelectors();
            blueSelectorImage.enabled = true;
            playerRenderer.material = blueSkin.Material;
        }
    }

    public void GreenButtonClicked()
    {
        if (greenSkinUnlocked)
        {
            DisableAllSelectors();
            greenSelectorImage.enabled = true;
            playerRenderer.material = greenSkin.Material;
        }
    }

    public void RedButtonClicked()
    {
        if (redSkinUnlocked)
        {
            DisableAllSelectors();
            redSelectorImage.enabled = true;
            playerRenderer.material = redSkin.Material;
        }
    }

    public void BlackButtonClicked()
    {
        if (blackSkinUnlocked)
        {
            DisableAllSelectors();
            blackSelectorImage.enabled = true;
            playerRenderer.material = blackSkin.Material;
        }
    }

    // Misc
    private void DisableAllSelectors()
    {
        blueSelectorImage.enabled = false;
        greenSelectorImage.enabled = false;
        redSelectorImage.enabled = false;
        blackSelectorImage.enabled = false;
    }

    private void DrawTooltip()
    {
        tooltipTitle.text = "Locked!!!";
        tooltipDescription.text = "To unlock this skin, you must have completed the following quest: \n";

        switch (tooltipActive)
        {
            case "blue":
                blueSkinUnlocked = true; // this one is unlocked by default

                if (blueSkinUnlocked)
                {
                    tooltipTitle.color = Color.white;
                    tooltipDescription.color = Color.white;

                    tooltipTitle.text = blueSkin.name;
                    tooltipDescription.text = blueSkin.Description;
                }
                else
                {
                    Debug.LogWarning("SkinSelectorManager/DrawTooltip: " +
                        "the blue skin is supposed to be unlocked by default," +
                        " so this message should never be shown.");
                }
                break;
            case "green":
                greenSkinUnlocked = Quest.quests[0].CompletionCondition.Invoke();

                if (greenSkinUnlocked)
                {
                    tooltipTitle.color = Color.white;
                    tooltipDescription.color = Color.white;

                    tooltipTitle.text = greenSkin.name;
                    tooltipDescription.text = greenSkin.Description;
                }
                else
                {
                    tooltipTitle.color = Color.red;
                    tooltipDescription.color = Color.red;

                    tooltipDescription.text += Quest.quests[0].Display;
                }
                break;
            case "red":
                redSkinUnlocked = Quest.quests[1].CompletionCondition.Invoke();

                if (redSkinUnlocked)
                {
                    tooltipTitle.color = Color.white;
                    tooltipDescription.color = Color.white;

                    tooltipTitle.text = redSkin.name;
                    tooltipDescription.text = redSkin.Description;
                }
                else
                {
                    tooltipTitle.color = Color.red;
                    tooltipDescription.color = Color.red;

                    tooltipDescription.text += Quest.quests[1].Display;
                }
                break;
            case "black":
                blackSkinUnlocked = Quest.quests[0].CompletionCondition.Invoke();

                if (blackSkinUnlocked)
                {
                    tooltipTitle.color = Color.white;
                    tooltipDescription.color = Color.white;

                    tooltipTitle.text = blackSkin.name;
                    tooltipDescription.text = blackSkin.Description;
                }
                else
                {
                    tooltipTitle.color = Color.red;
                    tooltipDescription.color = Color.red;

                    tooltipDescription.text += Quest.quests[2].Display;
                }
                break;
            default:
                Debug.LogWarning("SkinSelectorManager/DrawTooltip: " +
                    "Reached an unimplemented case.");
                break;
        }
    }
}
