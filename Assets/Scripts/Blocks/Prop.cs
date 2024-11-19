using UnityEngine;

namespace Blocks
{
    public class Prop : BaseBlock
    {
        protected override void Awake()
        {
            SaveSystem.props.Add(this);
        }

        private void OnDestroy()
        {
            SaveSystem.props.Remove(this);
        }

        private void Start()
        {
            Setup();

            player = GameObject.FindWithTag("MainCamera");
            Debug.Log("HEREEEEEEEEEEEEEEEEEE" + player);
        }

        public override void Update()
        {
            SetName();
            UpdatePanelColor();
        }

        public override void Actuate()
        {
            // Intentionally empty
        }
    }
} 