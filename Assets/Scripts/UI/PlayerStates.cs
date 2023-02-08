using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PlayerStates : MonoBehaviour
    {
        public static PlayerStates Instance;
        [SerializeField] private List<ResourceText> resourceTexts;

        private void Awake()
        {
            Instance = this;
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