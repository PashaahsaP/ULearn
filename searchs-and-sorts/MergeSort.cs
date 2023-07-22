 
public static class Sort
{
    
    static void Merge(int[] array,int[] temporaryArray, int start, int middle, int end)
    {
        var leftPtr = start;//начало условно первой коллекции
        var rightPtr = middle + 1;//начало условно второй коллекции
        var length = end - start + 1;
        for (int i = 0; i < length; i++)
        {
            if (rightPtr > end || (leftPtr <= middle && array[leftPtr] < array[rightPtr]))//leftPtr <= middle чтобы не залезть во вторую коллекцию,rightPtr > end если правая коллекция пуста то добавляем левую
            {
                temporaryArray[i] = array[leftPtr];
                leftPtr++;//если элемент оказался меньше чем элемент во второй коллекции, добавляем значение в массив и смещаем указатель
            }
            else
            {
                temporaryArray[i] = array[rightPtr];
                rightPtr++;// если элемент оказался меньше чем элемент в первой коллекции, добавляем значение в массив и смещаем указатель
            }
        }
        for (int i = 0; i < length; i++)
            array[i + start] = temporaryArray[i];//присваивает значения в основной массив с учетом откуда было сравнение двух коллекций и какой длины
    }
    static void MergeDescending(int[] array, int[] temporaryArray, int start, int middle, int end)
    {
        var leftPtr = start;//начало условно первой коллекции
        var rightPtr = middle + 1;//начало условно второй коллекции
        var length = end - start + 1;
        for (int i = 0; i < length; i++)
        {
            if (rightPtr > end || (leftPtr <= middle && array[leftPtr] > array[rightPtr]))//leftPtr <= middle чтобы не залезть во вторую коллекцию,rightPtr > end если правая коллекция пуста то добавляем левую
            {
                temporaryArray[i] = array[leftPtr];
                leftPtr++;//если элемент оказался меньше чем элемент во второй коллекции, добавляем значение в массив и смещаем указатель
            }
            else
            {
                temporaryArray[i] = array[rightPtr];
                rightPtr++;// если элемент оказался меньше чем элемент в первой коллекции, добавляем значение в массив и смещаем указатель
            }
        }
        for (int i = 0; i < length; i++)
            array[i + start] = temporaryArray[i];//присваивает значения в основной массив с учетом откуда было сравнение двух коллекций и какой длины
    }
    static void MergeSort(int[] array, int[] temporaryArray, int start, int end)
    {
        if (start == end) return;
        var middle = (start + end) / 2;
        MergeSort(array, temporaryArray,start, middle);//  разбиение индексов по левой ветке  
        MergeSort(array, temporaryArray, middle + 1, end);//разбиение индексов правой ветки
        Merge(array, temporaryArray, start, middle, end);// слияние

    }
    public static void MergeSortRecursive(this int[] array)
    {
        int[] temporaryArray = new int[array.Length];
        MergeSort(array, temporaryArray,0, array.Length - 1);
    }
    public static void MergeSortDescendingRecursive(this int[] array)
    {
        int[] temporaryArray = new int[array.Length];
        MergeSort(array, temporaryArray, 0, array.Length - 1);
    }
    

}