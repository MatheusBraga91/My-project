using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UserUIUpdater : MonoBehaviour
{
    public Image userImage;
    public TextMeshProUGUI healthText;
    public Image healthBarImage;
    public Button[] skillButtons;
    public Button trapSkillButton;

    public EnemyUIUpdater enemyUIUpdater; // Reference to the EnemyUIUpdater component

    void Start()
    {
        if (enemyUIUpdater == null)
        {
            enemyUIUpdater = FindObjectOfType<EnemyUIUpdater>();
        }

        StartCoroutine(InitializeUI());
    }

    IEnumerator InitializeUI()
    {
        yield return new WaitUntil(() => BattleManager.userCharacterClone != null);

        if (BattleManager.userCharacterClone != null)
        {
            if (userImage != null)
            {
                userImage.sprite = BattleManager.userCharacterClone.backViewImage;
            }
            else
            {
                Debug.LogError("User Image component is not assigned!");
            }

            UpdateHealthUI();
            PopulateSkillButtons();
            AssignRandomTrapSkill();
        }
        else
        {
            Debug.LogError("User character clone is not found!");
        }
    }

   public void UpdateHealthUI()
    {
        if (BattleManager.userCharacterClone != null)
        {
            int currentHealth = BattleManager.userCharacterClone.currentHealth;
            int baseHealth = BattleManager.userCharacterClone.baseHealth;

            healthText.text = $"{currentHealth}/{baseHealth}";
            healthBarImage.fillAmount = (float)currentHealth / baseHealth;
        }
    }

    void PopulateSkillButtons()
    {
        if (BattleManager.userCharacterClone != null && skillButtons.Length == 4)
        {
            Skill[] defaultSkills = BattleManager.userCharacterClone.defaultSkills;

            for (int i = 0; i < defaultSkills.Length && i < 4; i++)
            {
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = defaultSkills[i].skillName;

                int skillIndex = i;
                skillButtons[i].onClick.AddListener(() => UseSkill(skillIndex));
            }
        }
        else
        {
            Debug.LogError("Default skills not found or skill buttons are improperly assigned!");
        }
    }

    void UseSkill(int skillIndex)
    {
        if (BattleManager.userCharacterClone != null && BattleManager.aiCharacterClone != null)
        {
            Skill selectedSkill = BattleManager.userCharacterClone.defaultSkills[skillIndex];

            BattleManager battleManager = FindObjectOfType<BattleManager>();
            if (battleManager != null)
            {
                battleManager.ApplyDamage(BattleManager.userCharacterClone, BattleManager.aiCharacterClone, selectedSkill);
            }
            else
            {
                Debug.LogError("BattleManager instance not found!");
            }

            UpdateHealthUI();

            if (enemyUIUpdater != null)
            {
                enemyUIUpdater.UpdateEnemyHealthUI(
                    BattleManager.aiCharacterClone.currentHealth,
                    BattleManager.aiCharacterClone.baseHealth
                );
            }
            else
            {
                Debug.LogError("EnemyUIUpdater is not assigned!");
            }
        }
    }

    void AssignRandomTrapSkill()
    {
        if (BattleManager.userCharacterClone != null && trapSkillButton != null)
        {
            TrapSkill[] trapSkills = BattleManager.userCharacterClone.trapSkills;

            if (trapSkills != null && trapSkills.Length > 0)
            {
                int randomIndex = Random.Range(0, trapSkills.Length);
                TrapSkill selectedTrapSkill = trapSkills[randomIndex];

                trapSkillButton.GetComponentInChildren<TextMeshProUGUI>().text = selectedTrapSkill.skillName;
            }
            else
            {
                Debug.LogError("Trap skills are either null or empty!");
            }
        }
        else
        {
            Debug.LogError("TrapSkillButton or userCharacterClone is not found!");
        }
    }
}
