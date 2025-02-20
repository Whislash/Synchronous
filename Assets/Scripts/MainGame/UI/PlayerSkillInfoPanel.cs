using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KWY
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class PlayerSkillInfoPanel : MonoBehaviour, IInstantiatableUI
    {
        [SerializeField]
        Image icon;

        [SerializeField]
        TMP_Text nameLabel;

        [SerializeField]
        TMP_Text exLabel;

        [SerializeField]
        TMP_Text costLabel;

        [SerializeField]
        GameObject containerPanel;

        #region IInstantiatableUI 

        public void Init()
        {
            // empty
        }

        #endregion

        public void OnClickClose()
        {
            //containerPanel.SetActive(false);
            Destroy(gameObject);
        }

        internal void SetData(PlayerSkillBase psb)
        {
            icon.sprite = psb.icon;
            nameLabel.text = psb.name;
            exLabel.text = psb.skillExplanation;
            costLabel.text = psb.cost.ToString();
        }
    }
}
