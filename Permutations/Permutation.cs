namespace Statistics;

class Permutation
{
    public static double Factorial(int x)
    {
        double result = 1;
        for (int i = 1; i <= x; i++) result *= i;
        return result;
    }

    public static double PermutationsFrom(int n, int r)
    {
        if (r > n) throw new Exception("R cannot be greater than N");
        double result = 1;
        for (int i = n; i > (n - r); i--) result *= i;
        return result;
    }
    public static double nPr(int n, int r) => PermutationsFrom(n, r);

    public static double DistinguishablePermutations(int[] occurances)
    {
        double result = Factorial(occurances.Sum());
        foreach (int i in occurances) result /= Factorial(i);
        return result;
    }

    public static double CombinationsFrom(int n, int r) 
    {
        if (r > n - r) return PermutationsFrom(n, r) / Factorial(r);
        return PermutationsFrom(n, n - r) / Factorial(n - r);
    }
    public static double nCr(int n, int r) => CombinationsFrom(n, r);
}