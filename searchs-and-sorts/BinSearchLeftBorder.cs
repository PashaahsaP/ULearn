public static class Temp
{
    public static int BinSearchLeftBorder(long[] array, long value, int left, int right)
    {
        if (right == 0 || right-left ==1) return left;
        var m = (left + right) / 2;
        if (value <= array[m])
            return BinSearchLeftBorder(array, value, left, m);
        else
            return BinSearchLeftBorder(array, value, m, right);
        return -1;
    }
}

public class BinarySearchTests
{
    [Theory]
    [InlineData(new long[] { 1, 2, 3, 4, 5 }, 3, -1, 5, 1)]
    [InlineData(new long[] { 3, 4, 5, 6, 7 }, 6, -1, 5, 2)]
    [InlineData(new long[] { 1, 2, 5, 10, 11 }, 1, -1, 5, -1)]
    [InlineData(new long[] { 1, 4, 5, 6, 23 }, 5, -1, 5, 1)]
   
    public void BinSearchLeftBorder_ReturnsCorrectIndex(long[] array, long value, int left, int right, int expectedIndex)
    {
        // Act
        int actualIndex = Temp.BinSearchLeftBorder(array, value, left, right);

        // Assert
        Assert.Equal(expectedIndex, actualIndex);
    }
}