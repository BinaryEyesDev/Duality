using ImGuiNET;
using Microsoft.Xna.Framework;

namespace Duality.Editing.Utilities
{
    using NumericVector2 = System.Numerics.Vector2;

    public static class InspectVector2
    {
        public static Vector2 Perform(string label, Vector2 value)
        {
            var vector = new NumericVector2(value.X, value.Y);
            ImGui.InputFloat2(label, ref vector);

            return new Vector2(vector.X, vector.Y);
        }
    }
}
