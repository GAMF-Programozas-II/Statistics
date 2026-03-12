/*
  Az alábbi feladatot LINQ használata nélkül oldja meg. 
  Az Array osztály metódusait nem használhatja, a feladat célja néhány alap algoritmus típus implementálásának begyakorlása. 
  Készítsen egy StatAnalyzer nevű osztályt. Ez egy statikus metódusokat tartalmazó osztály legyen, adattagok nélkül, konstruktor nem kell. Tagjai az alábbi statikus metódusok:
  - GetMin(int[] a) -> int : visszaadja az a-ban tárolt legkisebb értéket
  - GetMax(int[] a) -> int : visszaadja az a-ban tárolt legnagyobb értéket
  - GetUnique(int[] a) -> int: visszaadja az a-ban tárolt egyedi értékeket egy tömbben
  - GetFrequency(int[] a, int[] u) -> int[]: visszaadja a t-ben az egyedi értékek gyakoriságát
Tesztelje az osztály metódusait a {2,2,2,2,3,3,3,4,6,7,8} tömbbel.
 
 */

internal class Program
{
  private static void Main(string[] args)
  {
    Console.WriteLine("Statisztika");
    int[] a = { 2, 2, 2, 2, 3, 3, 3, 4, 6, 7, 8 };
    Console.WriteLine("k= "+a.ToFormattedString());
    Console.WriteLine("Min: " + StatAnalyzer.GetMin(a));
    Console.WriteLine("Max: " + StatAnalyzer.GetMax(a));
    int[] u = StatAnalyzer.GetUnique(a);
    Console.WriteLine("Unique:    " + u.ToFormattedString());
    int[] g= StatAnalyzer.GetFrequency(a,u);
    Console.WriteLine("Frequency: " + g.ToFormattedString());
  }
}
//------------------- StatAnalyzer osztály ------------------
static class StatAnalyzer
  {
    public static int GetMin(int[] a)
    {
      int min = a[0];
      for (int i = 1; i < a.Length; i++)
        if (a[i] < min)
          min = a[i];
      return min;
    }
    public static int GetMax(int[] a)
    {
      int max = a[0];
      for (int i = 1; i < a.Length; i++)
        if (a[i] > max)
          max = a[i];
      return max;
    }
    public static int[] GetUnique(int[] a)
    {
      List<int> u = new List<int>();
      // Végigmegyünk egyesével az a tömb elemein,
      // és ha egy elem még nincs benne a u listában,
      // akkor hozzáadjuk. A bennfoglalást úgy vizsgáljuk,
      // hogy végigmegyünk a u listán, és ha nem találunk
      // egyetlen listaelemetsem, ami megegyezne a tömb
      // aktuális elemével, akkor a tömb aktuális elemét 
      // hozzáadjuk a u listához.
      for (int i = 0; i < a.Length; i++)
      {
        bool contains = false;
        for (int j = 0; j < u.Count; j++)
          if (a[i] == u[j])
          {
            contains = true;
            break;
          }
        if (!contains)
          u.Add(a[i]);
      }
      return u.ToArray();
    }
    public static int[] GetFrequency(int[] a, int[] u)
    {
      int[] f = new int[u.Length];
      for (int i = 0; i < u.Length; i++)
      {
        for (int j = 0; j < a.Length; j++)
        {
          if (a[j] == u[i])
            f[i]++;
        }
      }
      return f;
    }
  }
//------------------- ArrayExtensions osztály ------------------
static class ArrayExtensions
{
  public static string ToFormattedString(this int[] a)
  {
    string s = "{";
    for (int i = 0; i < a.Length; i++)
    {
      s += a[i];
      if (i < a.Length - 1)
        s += ",";
    }
    s += "}";
    return s;
  }
}
