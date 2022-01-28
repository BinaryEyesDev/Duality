namespace Duality.Data
{
    public readonly struct GridIndex
    {
        public readonly int Column;
        public readonly int Row;

        public GridIndex(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public override string ToString()
        {
            return $"({Column}, {Row})";
        }
    }
}
