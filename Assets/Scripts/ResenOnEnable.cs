using UnityEngine;

// Interface for scripts that need to be reset when re-enabled
public interface IResettable
{
    void ResetScript();
}

// Helper component you can add to scripts that need resetting
public class ResetOnEnable : MonoBehaviour
{
    public void ResetScript()
    {
        // Find any IResettable components on this GameObject
        IResettable[] resettables = GetComponents<IResettable>();
        foreach (IResettable resettable in resettables)
        {
            resettable.ResetScript();
        }
        
        // Or just manually re-run Start-like initialization
        SendMessage("ManualStart", null, SendMessageOptions.DontRequireReceiver);
    }
}