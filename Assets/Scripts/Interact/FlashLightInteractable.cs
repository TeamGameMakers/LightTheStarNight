using Characters;
using Save;
using UnityEngine;

namespace Interact
{
    public class FlashLightInteractable : Interactable
    {
        protected override void Start()
        {
            base.Start();
            // 如果已经拾取到了就不再显示
            if (SaveManager.GetBool(PickSaveKey))
            {
                Destroy(gameObject);
            }
        }

        public override void Interact(Interactor interactor)
        {
            Debug.Log("与手电筒交互");
            Player player = interactor.gameObject.GetComponent<Player>();
            // 获取到手电筒
            player.data.hasFlashLight = true;
            
            // 记录并销毁
            SaveManager.Register(PickSaveKey);
            Destroy(gameObject);
        }
    }
}