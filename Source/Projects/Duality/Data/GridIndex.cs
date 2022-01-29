namespace Duality.Data
{
    public readonly struct GridIndex
    {
        public static GridIndex Invalid => new(-1, -1);
        public readonly int Column;
        public readonly int Row;
        public bool IsValid => CheckValid();

        public GridIndex(int column, int row)
        {
            Column = column;
            Row = row;
        }

        private bool CheckValid()
        {
            var mapSize = GlobalConfiguration.MapSize;
            return Column > 0 && Column < mapSize.X && Row > 0 && Row < mapSize.Y;
        }

        public override string ToString() => $"({Column}, {Row})";
        public override bool Equals(object obj) => obj is GridIndex index1D && Equals(index1D);
        public bool Equals(GridIndex other) => Column == other.Column && Row == other.Row;
        public override int GetHashCode() => Column.GetHashCode() ^ Row.GetHashCode();
        public static bool operator ==(GridIndex lhs, GridIndex rhs) => lhs.Equals(rhs);
        public static bool operator !=(GridIndex lhs, GridIndex rhs) => !lhs.Equals(rhs);
        public static GridIndex operator +(GridIndex lhs, GridIndex rhs) => new(lhs.Column + rhs.Column, lhs.Row + rhs.Row);
        public static GridIndex operator -(GridIndex lhs, GridIndex rhs) => new(lhs.Column - rhs.Column, lhs.Row - rhs.Row);
        public static GridIndex operator *(GridIndex lhs, GridIndex rhs) => new(lhs.Column*rhs.Column, lhs.Row*rhs.Row);
        public static GridIndex operator /(GridIndex lhs, GridIndex rhs) => new(lhs.Column/rhs.Column, lhs.Row/rhs.Row);
    }
}
