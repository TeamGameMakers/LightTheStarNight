using Characters;
using Save;
using UnityEngine;

namespace Interact
{
    public class StarLightInteractable : Interactable
    {
        public RambleObject rambleObject;
        
        protected override void Start()
        {
            base.Start();
            if (SaveManager.GetBool(PickSaveKey))
            {
                Destroy(gameObject);
            }
        }

        public override void Interact(Interactor interactor)
        {
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