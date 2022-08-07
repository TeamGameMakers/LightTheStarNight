using Characters;
using Save;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Interact
{
    public class StarLightInteractable : Interactable
    {
        public RambleObject rambleObject;
        public bool isAbsorbing;
        
        protected override void Start()
        {
            base.Start();
            if (SaveManager.GetBool(PickSaveKey))
            {
                Destroy(gameObject);
            }
        }

        protected override void Update()
        {
            base.Update();
            
            if (isAbsorbing && !InputHandler.InteractPressed)
                isAbsorbing = false;
        }

        public override void Interact(Interactor interactor)
        {
            isAbsorbing = true;
            
            // 开启颤动
            if (rambleObject != null)
            {
                rambleObject.Shake();
            }
            
            if (InputHandler.AbsorbPressed)
            {
                Player player = interactor.gameObject.GetComponent<Player>();
                // 获取星光

                player.data.powerRemaining = player.data.maxPower;
                player.PlayerAudioClip(player.data.absorbPower);

                var curTransform = transform;
                var parent = curTransform.parent;
                curTransform.parent = null;
            
                Destroy(parent.gameObject);
                Destroy(curTransform.gameObject);
            }
        }
    }
}