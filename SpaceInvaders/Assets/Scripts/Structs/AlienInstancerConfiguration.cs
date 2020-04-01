
public struct AlienInstancerConfiguration
{
    public readonly int columns;
    public readonly int rows;
    public readonly int total;
    public readonly float pandding;

    public AlienInstancerConfiguration(int columns, int rows, float pandding)
    {
        this.columns = columns;
        this.rows = rows;
        this.pandding = pandding;
        total = columns * rows;
    }
}
