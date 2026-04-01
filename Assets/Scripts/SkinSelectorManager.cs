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
    [SerializeField] private GameObject tooltipPanel;
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
        tooltipPanel.SetActive(false);
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
                tooltipActive = "blue";
            }
            else if ((bool)isHighlighted.Invoke(greenButton, null))
            {
                tooltipActive = "green";
            }
            else if ((bool)isHighlighted.Invoke(redButton, null))
            {
                tooltipActive = "red";
            }
            else if ((bool)isHighlighted.Invoke(blackButton, null))
            {
                tooltipActive = "black";
            }
            else
            {
                if (blueSelectorImage.enabled)
                {
                    tooltipActive = "blue";
                }
                else if (greenSelectorImage.enabled)
                {
                    tooltipActive = "green";
                }
                else if (redSelectorImage.enabled)
                {
                    tooltipActive = "red";
                }
                else if (blackSelectorImage.enabled)
                {
                    tooltipActive = "black";
                }
                else
                {
                    Debug.LogWarning("SkinSelectorManager/Update: " +
                        "Skin Panel is open, but nothing is higlighted or has an active selector. " +
                        "It is likely due to a faulty new implementation.");
                    tooltipActive = "";
                }
            }

            if (tooltipActive != "")
            {
                if (!tooltipPanel.activeSelf)
                {
                    tooltipPanel.SetActive(true);
                }

                DrawTooltip();
            }
            else
            { 
                tooltipPanel.SetActive(false);
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
        DisableAllSelectors();
        blueSelector.GetComponent<Image>().enabled = true;
        playerRenderer.material = blueSkin.Material;
    }

    public void GreenButtonClicked()
    {
        DisableAllSelectors();
        greenSelector.GetComponent<Image>().enabled = true;
        playerRenderer.material = greenSkin.Material;
    }

    public void RedButtonClicked()
    {
        DisableAllSelectors();
        redSelector.GetComponent<Image>().enabled = true;
        playerRenderer.material = redSkin.Material;
    }

    public void BlackButtonClicked()
    {
        DisableAllSelectors();
        blackSelector.GetComponent<Image>().enabled = true;
        playerRenderer.material = blackSkin.Material;
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
        switch (tooltipActive)
        {
            case "blue":
                tooltipTitle.text = blueSkin.name;
                tooltipDescription.text = blueSkin.Description;
                break;
            case "green":
                tooltipTitle.text = greenSkin.name;
                tooltipDescription.text = greenSkin.Description;
                break;
            case "red":
                tooltipTitle.text = redSkin.name;
                tooltipDescription.text = redSkin.Description;
                break;
            case "black":
                tooltipTitle.text = blackSkin.name;
                tooltipDescription.text = blackSkin.Description;
                break;
            default:
                Debug.LogWarning("SkinSelectorManager/DrawTooltip: " +
                    "Reached an unimplemented case.");
                break;
        }
    }
}
