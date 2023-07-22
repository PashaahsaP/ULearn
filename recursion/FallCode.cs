
MakePermutations(new int[3],0,new List<int[]>());


static void MakePermutations(int[] permutation, int position, List<int[]> result)
{
    if (position == permutation.Length)
    {
        result.Add(permutation);
        return;
    }
    else
    {
        for (int i = 0; i < permutation.Length; i++)
        {
            var index = Array.IndexOf(permutation, i, 0, position);
            if (index == -1)
            {
                permutation[position] = i;
                MakePermutations(permutation,position+1,result);
            }
        }
    }
}