using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
    public class TutorialData : ScriptableObject
    {
        [SerializeField]
        private AudioClip tutorialMusic = null;
        public AudioClip TutorialMusic { get { return tutorialMusic; } }

        [SerializeField]
        private TutorialInstruction[] instructions = null;
        public IEnumerable<TutorialInstruction> Instructions { get { return instructions; } }

        [SerializeField]
        private PowerupData powerupItem = null;
        public PowerupData PowerupItem { get { return powerupItem; } }

        [SerializeField]
        private bool showSmoke = false;
        public bool ShowSmoke { get { return showSmoke; } }

        [System.Serializable]
        public class TutorialInstruction
        {
            [SerializeField]
            private GameItemData gameItem = null;
            public GameItemData GameItem { get { return gameItem; } }

            [SerializeField]
            private Sprite instructionBackground = null;
            public Sprite InstructionBackground { get { return instructionBackground; } }

            [SerializeField]
            private string instruction = "";
            public string Instruction { get { return instruction; } }

            public enum Action { None, Swipe, Tap }
            [SerializeField]
            private Action expectedAction = Action.None;
            public Action ExpectedAction { get { return expectedAction; } }
        }
    }

}