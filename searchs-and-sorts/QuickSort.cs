 
namespace Sort
{
 
    static class TaskSort
    {
        public static void QuickSort(this int[] array)
        {
            Sort(array,0,array.Length-1);
        }
        static void Sort(this int[] array,int start,int end)
        {
            if (end == start) return;
            var pivot = array[end];
            var storeIndex = start;

            for (int i = start; i <= end-1; i++)
            {
                if (array[i] <= pivot)
                {
                    (array[i], array[storeIndex]) = (array[storeIndex], array[i] );
                    storeIndex++;
                }
            }
            (array[end], array[storeIndex]) = (array[storeIndex], array[end]);
            if(storeIndex > start) Sort(array,start,storeIndex-1);
            if(storeIndex < end) Sort(array,storeIndex+1,end);

        }
       
    }
}
