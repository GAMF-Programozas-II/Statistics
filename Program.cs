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
// Ez egy statikus osztály, amely bővítő metódust tartalmaz.
// A bővítő metódusok (extension methods) lehetővé teszik, hogy
// meglévő típusokhoz új metódusokat adjunk hozzá anélkül, hogy
// módosítanánk az eredeti típus forráskódját vagy örökölnénk belőle.
static class ArrayExtensions
{
  // BŐVÍTŐ METÓDUS (Extension Method) magyarázata:
  // --------------------------------------------
  // A bővítő metódus egy speciális statikus metódus, amely úgy viselkedik,
  // mintha az első paraméterként megadott típus saját metódusa lenne.
  // 
  // A "this" kulcsszó az első paraméter előtt jelzi, hogy ez bővítő metódus.
  // Így a metódus nem csak hagyományos módon hívható (ArrayExtensions.ToFormattedString(tomb)),
  // hanem közvetlenül a tömb objektumon is (tomb.ToFormattedString()),
  // mintha az int[] típus saját metódusa lenne.
  //
  // PARAMÉTEREZÉS:
  // - "this int[] a": Az első paraméter MINDIG "this" kulcsszóval kezdődik
  //   * "this" = jelzi, hogy bővítő metódusról van szó
  //   * "int[]" = azt a típust határozza meg, amelyet bővítünk (jelen esetben int tömb)
  //   * "a" = a paraméter neve, ezen keresztül érjük el a tömb elemeit a metóduson belül
  //
  // HASZNÁLAT:
  // Ha van egy "int[] szamok" tömbünk, akkor hívhatjuk:
  // - szamok.ToFormattedString()  <- így néz ki úgy, mintha az int[] saját metódusa lenne
  // - ArrayExtensions.ToFormattedString(szamok)  <- hagyományos statikus metódus hívás is működik
  
  public static string ToFormattedString(this int[] a)
  {
    // Egy üres stringet hozunk létre, amely a formázott kimenetet fogja tartalmazni
    string s = "{";
    
    // Végigiterálunk a tömb összes elemén
    for (int i = 0; i < a.Length; i++)
    {
      // Hozzáfűzzük az aktuális elemet a stringhez
      s += a[i];
      
      // Ha nem az utolsó elemnél járunk, vesszőt is hozzáfűzünk
      // Ez biztosítja, hogy az utolsó elem után ne legyen vessző
      if (i < a.Length - 1)
        s += ",";
    }
    
    // Lezárjuk a stringet záró kapcsos zárójellel
    s += "}";
    
    // Visszaadjuk a formázott stringet (pl. "{2,2,2,2,3,3,3,4,6,7,8}")
    return s;
  }
}
