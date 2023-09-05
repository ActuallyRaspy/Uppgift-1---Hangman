string[] ordLista = {"Vispgrädde",
      "Ukulele",
      "Innebandyspelare",
      "Flaggstång",
      "Yxa",
      "Havsfiske",
      "Prisma",
      "Landsbygd",
      "Generositet",
      "Lyckosam",
      "Perrong",
      "Samarbeta",
      "Välartad"};

string[] betydelseLista = {"Uppvispad grädde.",
      "Ett fyrsträngat instrument med ursprung i Portugal.",
      "En person som spelar sporten innebandy.",
      "En mast man hissar upp en flagga på.",
      "Verktyg för att hugga ved.",
      "Fiske till havs.",
      "Ett transparent optiskt element som bryter ljuset vid plana ytor.",
      "Geografiskt område med lantlig bebyggelse.",
      "En personlig egenskap där man vill dela med sig av det man har.",
      "Att man ofta, eller för stunden har tur.",
      "Den upphöjda yta som passagerare väntar på eller stiger på/av ett spårfordon.",
      "Att arbeta tillsamans mot ett gemensamt mål.",
      "Att någon är väluppfostrad, skötsam, eller lovande."};

string antalGissningar = "5";
List<string> gissningarLista = new List<string>();

int nummer = slumpaNummer(); // Slumpa ett nummer tidigt så att den kan återanvändas.
string ord = ordLista[nummer];
string betydelse = betydelseLista[nummer];


Console.WriteLine("Hänga gubbe!\nFörsök att gissa ordet en bokstav i taget.\nDu har " + antalGissningar + " gissningar kvar.\n");


Console.WriteLine(ord); // TA BORT
Console.WriteLine("\nDina gissningar: ");
Console.WriteLine();

string gissning = Console.ReadLine();
gissningarLista.Add(gissning);

Console.WriteLine(gissningKoll(gissning));

gissning = Console.ReadLine();
gissningarLista.Add(gissning);

Console.WriteLine(gissningKoll(gissning));


foreach (string bokstav in gissningarLista)
{
    Console.WriteLine(bokstav);
}

Console.WriteLine(antalGissningar);


//if (ord.Contains(gissning)){
//    gissningarLista.Add(gissning);
//}

//else
//{
//    gissningarLista.Add(gissning);
//    antalGissningar -= 1;
//}

//Console.WriteLine(gissningarLista[0]);
//Console.WriteLine(antalGissningar.ToString());


int slumpaNummer() // Slumpa ett nummer mellan 0 och 14 och returnera som en int.
{
    Random rand = new Random();
    return rand.Next(14);

}


string gissningKoll(string ordInput)
{
    string ordgrej = ""; //Det gissade ordet printar ut kända chars. t.ex k_k_o för kakao om K och O är känt.
    foreach (char i in ord) //För varje karaktär i ordet
    {
        foreach (string c in gissningarLista) //För varje bokstav i den gissade listan
        {
            if (i == c[0]) // Om ordet innehåller en bokstav som har gissats, lägg till den i ordgrej för att bygga ihop stringen.
            {
                ordgrej += c.ToString();
            }

            else // Om bokstaven inte har gissats, lägg ut ett _
            {
                ordgrej += "_";
            }
        }
    }

    return ordgrej;
}