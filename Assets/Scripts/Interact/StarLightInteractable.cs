using Characters;
using Save;
using UnityEngine;

namespace Interact
{
    public class StarLightInteractable : Interactable
    {
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
            if (InputHandler.AbsorbPressed)
            {
                Debug.Log("与星光交互");
                Player player = interactor.gameObject.GetComponent<Player>();
                // TODO: 获取星光
            
                // 记录并销毁
                SaveManager.Register(PickSaveKey);
                Destroy(gameObject);
            }
        }
    }
}