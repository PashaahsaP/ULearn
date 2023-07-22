using System;

public static class Sort
{
    public static void BubbleSort(this int[] array)
    {
        for (int k = array.Length - 1; k > 0; k--)
        for (int j = 0; j < k; j++)
            if (array[j] > array[j+1])
                Swap(ref array[j], ref array[j+1]);

    }
    public static void BubbleSortDescending(this int[] array)
    {
        for (int k = array.Length - 1; k > 0; k--)
        for (int j = 0; j < k; j++)
            if (array[j] < array[j + 1])
                Swap(ref array[j], ref array[j + 1]);

    }
    public static void BubbleSortRange(int[] array, int left, int right)
    {
        for (int k = right; k > left; k--)
        for (int j = left; j < right; j++)
            if (array[j] > array[j + 1])
                Swap(ref array[j], ref array[j + 1]);
    }
    public static void Swap(ref int val1, ref int val2)
    {
        int temp = val1;
        val1 = val2;
        val2 = temp;
    }
}
