using GMain.Editor.SetupTool.Window;
using UnityEditor;

namespace GMain.Editor.SetupTool.Menu
{
    public class SetupToolMenu
    {
        [MenuItem("GMain/Setup Structure/SETUP")]
        public static void Init()
        {
            SetupToolWindow.InitWindow();
        }
    }
}