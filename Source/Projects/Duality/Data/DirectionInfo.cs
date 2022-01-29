namespace Duality.Data
{
    public readonly struct DirectionInfo
    {
        public readonly GridIndex Index;
        public readonly float Angle;

        public DirectionInfo(GridIndex index, float angle)
        {
            Index = index;
            Angle = angle;
        }
    }
}