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
    private List<int> skillSlotIndices = new List<int>(); // Track the starting indices of each skill's slots
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
        skillSlotIndices.Clear();
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

        // Track the starting index of this skill's slots
        skillSlotIndices.Add(usedSlots);

        // Track the skill in characterStats
        for (int i = 0; i < characterStats.trapSkills.Length; i++)
        {
            if (characterStats.trapSkills[i] == null)
            {
                characterStats.trapSkills[i] = skill;
                break;
            }
        }

        // Find the next available button
        for (int i = 0; i < trapSkillButtons.Length; i++)
        {
            if (!trapSkillButtons[i].gameObject.activeSelf)
            {
                // Activate the button
                trapSkillButtons[i].gameObject.SetActive(true);

                // Set the skill name as button text
                TMP_Text buttonText = trapSkillButtons[i].GetComponentInChildren<TMP_Text>();
                buttonText.text = $"{skill.skillName}";

                break;
            }
        }

        // Update the used slots count and slot visuals
        UpdateSlots(usedSlots, skill.slotRequired, 0.3f); // Set transparency for new slots
        usedSlots += skill.slotRequired;
    }

    // Remove the last TrapSkill
    public void RemoveLastTrapSkill()
    {
        if (addedSkills.Count == 0)
        {
            Debug.Log("No skills to remove!");
            return;
        }

        // Get the last skill and its starting slot index
        TrapSkill lastSkill = addedSkills[addedSkills.Count - 1];
        int startSlot = skillSlotIndices[addedSkills.Count - 1];
        int slotsToFree = lastSkill.slotRequired;

        // Remove the skill and its slot tracking
        addedSkills.RemoveAt(addedSkills.Count - 1);
        skillSlotIndices.RemoveAt(addedSkills.Count);

        // Remove the skill from characterStats
        for (int i = 0; i < characterStats.trapSkills.Length; i++)
        {
            if (characterStats.trapSkills[i] == lastSkill)
            {
                characterStats.trapSkills[i] = null;
                break;
            }
        }

        // Update slot visuals before decreasing the count
        UpdateSlots(startSlot, slotsToFree, 1.0f); // Restore transparency for freed slots

        // Decrease the used slots count
        usedSlots -= slotsToFree;

        // Deactivate the last button and clear its text
        for (int i = trapSkillButtons.Length - 1; i >= 0; i--)
        {
            if (trapSkillButtons[i].gameObject.activeSelf)
            {
                // Deactivate the button
                trapSkillButtons[i].gameObject.SetActive(false);

                // Clear the button's text
                TMP_Text buttonText = trapSkillButtons[i].GetComponentInChildren<TMP_Text>();
                buttonText.text = "";

                break;
            }
        }
    }

    // Update slot images' transparency for adding or removing skills
    void UpdateSlots(int startSlot, int slotCount, float transparency)
    {
        int endSlot = startSlot + slotCount;

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
