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
        private Player _player;
        
        protected override void Start()
        {
            base.Start();
            if (SaveManager.GetBool(PickSaveKey))
            {
                Destroy(gameObject);
            }
            
            _player = FindObjectOfType<Player>();
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
            if (rambleObject && _player.data.hasFlashLight)
            {
                rambleObject.Shake();
            }
            
            if (InputHandler.AbsorbPressed && _player.data.hasFlashLight)
            {
                // 获取星光
                _player.data.powerRemaining = _player.data.maxPower;
                _player.PlayerAudioClip(_player.data.absorbPower);

                var curTransform = transform;
                var parent = curTransform.parent;
                curTransform.parent = null;
            
                Destroy(parent.gameObject);
                Destroy(curTransform.gameObject);
            }
        }
    }
}