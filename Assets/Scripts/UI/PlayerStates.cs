using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PlayerStates : MonoBehaviour
    {
        public static PlayerStates instance;
        [SerializeField] private List<ResourceText> resourceTexts;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            UpdateAllStatesUI();
        }

        [ContextMenu("UpdateAllStatesUI")]
        public void UpdateAllStatesUI()
        {
            foreach (ResourceText resourceText in resourceTexts)
            {
                resourceText.ShowText();
            }
        }
    }
}