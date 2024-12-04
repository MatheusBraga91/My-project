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

    public EnemyUIUpdater enemyUIUpdater;

    void Start()
    {
        StartCoroutine(InitializeUI());
    }

   public IEnumerator InitializeUI()
    {
        yield return new WaitUntil(() => BattleManager.Instance.userCharacter != null);

        if (BattleManager.Instance.userCharacter != null)
        {
            var userCharacter = BattleManager.Instance.userCharacter;

            if (userImage != null)
            {
                userImage.sprite = userCharacter.backViewImage;
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
            Debug.LogError("User character is not found!");
        }
    }

    public void UpdateHealthUI()
    {
        var userCharacter = BattleManager.Instance.userCharacter;

        if (userCharacter != null)
        {
            healthText.text = $"{userCharacter.currentHealth}/{userCharacter.baseHealth}";
            healthBarImage.fillAmount = (float)userCharacter.currentHealth / userCharacter.baseHealth;
        }
    }

    void PopulateSkillButtons()
    {
        var userCharacter = BattleManager.Instance.userCharacter;

        if (userCharacter != null && skillButtons.Length == 4)
        {
            Skill[] defaultSkills = userCharacter.defaultSkills;

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
        var userCharacter = BattleManager.Instance.userCharacter;
        var aiCharacter = BattleManager.Instance.aiCharacter;

        if (userCharacter != null && aiCharacter != null)
        {
            Skill selectedSkill = userCharacter.defaultSkills[skillIndex];

            BattleManager.Instance.ApplyDamage(userCharacter, aiCharacter, selectedSkill);

            UpdateHealthUI();
            enemyUIUpdater?.UpdateEnemyHealthUI();
        }
    }

    void AssignRandomTrapSkill()
    {
        var userCharacter = BattleManager.Instance.userCharacter;

        if (userCharacter != null && trapSkillButton != null)
        {
            TrapSkill[] trapSkills = userCharacter.trapSkills;

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
    }
}
