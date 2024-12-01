using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TrapSkillContainer : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform trapSkillPanel; // Panel to hold TrapSkill buttons
    public Button eraseButton; // Erase button
    public Button[] trapSkillButtons; // Pre-created buttons (size 5)
    public Image[] slotImages; // Images representing slots

    [Header("State Management")]
    private List<TrapSkill> addedSkills = new List<TrapSkill>();
    private List<List<int>> skillSlots = new List<List<int>>(); // To track the slots each skill occupies
    private int usedSlots = 0;
    private const int maxSlots = 5;

    public CharacterStats characterStats; // Reference to the character stats

    void Start()
    {
        // Set initial visibility of all buttons to 0 (hidden)
        foreach (Button button in trapSkillButtons)
        {
            button.gameObject.SetActive(false); // Hide all buttons at the start
        }

        eraseButton.onClick.AddListener(RemoveLastTrapSkill);

        // Initialize the trap skills based on the character's stats
        InitializeTrapSkills();
    }

    // Initialize trap skills based on character's equipped skills
    public void InitializeTrapSkills()
    {
        // Clear any existing skills first
        addedSkills.Clear();
        skillSlots.Clear();
        usedSlots = 0;

        // Iterate through character's trap skills and display them
        for (int i = 0; i < characterStats.trapSkills.Length; i++)
        {
            if (characterStats.trapSkills[i] != null)
            {
                AddTrapSkill(characterStats.trapSkills[i]);
            }
        }
    }

    // Add a TrapSkill to the container
    public void AddTrapSkill(TrapSkill skill)
    {
        if (usedSlots + skill.slotRequired > maxSlots)
        {
            Debug.Log("No more slots available!");
            return;
        }

        // Add the skill to the list
        addedSkills.Add(skill);

        // Track the slots the skill occupies
        List<int> occupiedSlots = new List<int>();

        // Add the skill to characterStats
        for (int i = 0; i < characterStats.trapSkills.Length; i++)
        {
            if (characterStats.trapSkills[i] == null)
            {
                characterStats.trapSkills[i] = skill; // Assign the skill to the first empty slot in characterStats
                occupiedSlots.Add(i); // Track the slot index
                if (occupiedSlots.Count == skill.slotRequired)
                {
                    break;
                }
            }
        }

        skillSlots.Add(occupiedSlots); // Store the occupied slots for this skill

        // Find the next available button
        for (int i = 0; i < trapSkillButtons.Length; i++)
        {
            if (!trapSkillButtons[i].gameObject.activeSelf) // Check if the button is hidden
            {
                // Activate the button
                trapSkillButtons[i].gameObject.SetActive(true);

                // Set the skill name as button text
                TMP_Text buttonText = trapSkillButtons[i].GetComponentInChildren<TMP_Text>();
                buttonText.text = skill.skillName;

                // Update slot visuals
                UpdateSlots(skill.slotRequired, 0.3f);
                usedSlots += skill.slotRequired;

                break; // Exit the loop once the button is assigned
            }
        }
    }

    // Remove the last TrapSkill
    public void RemoveLastTrapSkill()
    {
        if (addedSkills.Count == 0)
        {
            Debug.Log("No skills to remove!");
            return;
        }

        // Get the last skill and remove it
        TrapSkill lastSkill = addedSkills[addedSkills.Count - 1];
        addedSkills.RemoveAt(addedSkills.Count - 1);

        // Remove the skill from characterStats
        List<int> occupiedSlots = skillSlots[skillSlots.Count - 1];
        skillSlots.RemoveAt(skillSlots.Count - 1);

        for (int i = 0; i < characterStats.trapSkills.Length; i++)
        {
            if (characterStats.trapSkills[i] == lastSkill)
            {
                characterStats.trapSkills[i] = null; // Remove the skill from characterStats
            }
        }

        // Restore the transparency of all slots the removed skill occupied
        foreach (int slotIndex in occupiedSlots)
        {
            UpdateSlotTransparency(slotIndex, 1.0f);
        }

        // Decrease used slots
        usedSlots -= lastSkill.slotRequired;

        // Deactivate the last button and clear its text
        for (int i = trapSkillButtons.Length - 1; i >= 0; i--)
        {
            if (trapSkillButtons[i].gameObject.activeSelf) // Find the last active button
            {
                // Deactivate the button
                trapSkillButtons[i].gameObject.SetActive(false);

                // Clear the button's text
                TMP_Text buttonText = trapSkillButtons[i].GetComponentInChildren<TMP_Text>();
                buttonText.text = ""; // Clear text

                break; // Exit the loop once the button is cleared
            }
        }
    }

    // Update the transparency of a specific slot
    void UpdateSlotTransparency(int slotIndex, float transparency)
    {
        if (slotIndex >= 0 && slotIndex < slotImages.Length)
        {
            Color color = slotImages[slotIndex].color;
            color.a = transparency;
            slotImages[slotIndex].color = color;
        }
    }

    // Update slot images' transparency for adding new skills
    void UpdateSlots(int slotChange, float transparency)
    {
        int startSlot = usedSlots;
        int endSlot = startSlot + Mathf.Abs(slotChange);

        for (int i = startSlot; i < endSlot; i++)
        {
            if (i >= 0 && i < slotImages.Length)
            {
                Color color = slotImages[i].color;
                color.a = transparency;
                slotImages[i].color = color;
            }
        }
    }
}
