using Duality.Data;

namespace Duality.Editing
{
    public static class TextureSelectionManager
    {
        public static GameElementTemplateInfo CurrentlySelected = GameElementTemplateInfo.Invalid;

        public static void Set(GameElementTemplateInfo info)
        {
            CurrentlySelected = CurrentlySelected.Pointer == info.Pointer ? GameElementTemplateInfo.Invalid : info;
        }
    }
}
