using UnityEngine;

namespace DapperDino.GGJ2020.Cursors
{
    public class CursorBlockerBehaviour : MonoBehaviour
    {
        private void OnEnable() => CursorStateHandler.AddBlocker(this);
        private void OnDisable() => CursorStateHandler.RemoveBlocker(this);
    }
}
