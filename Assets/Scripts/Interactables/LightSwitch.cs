using UnityEngine;


    public class LightSwitch : MonoBehaviour, IInteractable
    {
        [SerializeField] private Light[] ConnectedLights;
        [SerializeField] private bool IsOn = true;

        public LightSwitchData lightSwitchData;

    

        //public bool canSwitchLight => BasicInventory.items.HasItem("test") ?? false;

        private void Awake()
        {
            UpdateConnectedLights();
        }
        public bool CanInteractWith(GameObject interactor)
        {
            if (interactor.TryGetComponent<BasicInventory>(out BasicInventory inventory))
            {
                return inventory.HasItem(lightSwitchData.requiredItem);
            }

            return false;
        }
        public void Interact(GameObject interactor)
        {
            IsOn = !IsOn;
            UpdateConnectedLights();
        }
        private void UpdateConnectedLights()
        {
                foreach (var light in ConnectedLights)
                {
                    light.enabled = IsOn;
                }
        }
    }

