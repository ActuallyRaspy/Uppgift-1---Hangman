using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
bool spelaIgen = true;
while (spelaIgen)
{
    string[] ordLista = {"vispgrädde",
          "ukulele",
          "innebandyspelare",
          "flaggstång",
          "yxa",
          "havsfiske",
          "prisma",
          "landsbygd",
          "generositet",
          "lyckosam",
          "perrong",
          "samarbeta",
          "välartad"};

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

    int antalGissningar = 5;
    List<char> gissningarLista = new List<char>();

    int nummer = slumpaNummer(); // Slumpa ett nummer tidigt så att den kan återanvändas.

    string ord = ordLista[nummer];
    string betydelse = betydelseLista[nummer];
    string gissning = "";
    bool ogiltigGissning = false;
    bool redanGissad = false;

    int slumpaNummer() // slumpa ett nummer mellan 0 och 14 och returnera som en int.
    {
        Random rand = new Random();
        return rand.Next(13);

    }

    while (!kollaOrdKorrekt(gissningarLista, ord) && antalGissningar > 0) //Medans gissningarLista inte innehåller alla bokstäver i ord, loopa.
    {

        byggSpelText();

        if (ogiltigGissning) //Om gissning var ogiltig i tidigare loop, visa text och återställ bool för nästa loop.
        {
            Console.WriteLine("Din gissning får bara vara en bokstav!");
            ogiltigGissning = false;

        }

        if (redanGissad)
        {
            Console.WriteLine("Du har redan gissat den här bokstaven!");
            redanGissad = false;
        }

        gissning = Console.ReadLine().ToLower();
        if (verifieraInput(gissning))
        {
            if (gissningarLista.Contains(gissning[0]))
            {
                redanGissad = true;
            }

            else
            {
                string jämförOrd = byggUppOrd(gissningarLista);
                gissningarLista.Add(gissning[0]);

                if (jämförOrd == byggUppOrd(gissningarLista))
                {
                    antalGissningar--;
                }
            }
        }

        else
        {
            ogiltigGissning = true;
        }
    }

    if (antalGissningar <= 0)
    {
        byggSpelText();
        Console.WriteLine("Du har förlorat, vill du spela igen? y/n");
        if (Console.ReadLine().ToLower() == "y")
        {
            spelaIgen = true;
        }

        else if (Console.ReadLine().ToLower() == "n")
        {
            spelaIgen = false;
        }
    }

    else if (kollaOrdKorrekt(gissningarLista, ord))
    {
        byggSpelText();
        Console.WriteLine("Du har vunnit, vill du spela igen? y/n");

        do //Sålänge inte y eller n anges, loopa tills y eller n blir skrivet
        {
            string slutSvar = Console.ReadLine().ToLower();
            if (slutSvar == "y")
            {
                spelaIgen = true;
                break;
            }

            else if (slutSvar == "n")
            {
                spelaIgen = false;
                break;
            }

            else
            {
                Console.WriteLine("Felaktigt svar.");
            }
        }

        while (true);
        
    }

    void byggSpelText()
    {
        gissning = ""; //Rensa gissning
        Console.Clear(); //Rensa konsolen för ny uppdaterad output
        Console.WriteLine("Hänga gubbe!\nGissa ordet en bokstav i taget."); // Bygg upp speltexten dynamiskt med hjälp av metoden byggUppOrd & visaGissningar
        Console.WriteLine("Ord: " + byggUppOrd(gissningarLista));
        Console.WriteLine("\n****Ord: " + ord + "\n");
        Console.WriteLine("\nDina gissningar: " + visaGissningar());
        Console.WriteLine("Antal gissningar kvar: " + antalGissningar.ToString());
    }

    string visaGissningar() // Returnera vilka bokstäver som finns i gissningarLista i formatet: a, b, c, d, 
    {
        string gissningar = "";
        foreach (char gissning in gissningarLista)
        {
            gissningar += gissning + ", ";
        }
        return gissningar;
    }

    bool verifieraInput(string input)
    {
        if (input.Count() == 1 && Char.IsLetter(input[0])) // Om det bara finns 1 bokstav & det är en bokstav, return true
        {
            return true;
        }

        else { return false; }
    }

    string byggUppOrd(List<char> gissLista)
    {
        string byggtOrd = "";
        int i = 0; // Räknar om en bokstav har använts eller det behövs ett _
        foreach (char bokstav in ord) // För varje bokstav i ordet
        {
            foreach (char gissning in gissLista) // För varje gissad bokstav i gissLista
            {
                i = 0; //Återställ räknaren
                if (gissning == bokstav) // Om den gissade bokstaven stämmer med bokstaven i ordet
                {
                    byggtOrd += gissning; // Lägg till gissningen i en string
                    i++; //Öka räknaren med 1
                    break; //Bryt loopen eftersom vi redan har fått fram bokstaven
                }
            }

            if (i != 1) // Om räknaren inte står på 1 (alltså att bokstaven inte finns i gissLista pga ovan loop) lägg till ett _ i ordet  
            {
                byggtOrd += "_";

            }

        }

        return byggtOrd;
    }

    bool kollaOrdKorrekt(List<char> gissLista, string ord)
    {
        ord = ord.ToLower(); //Felhantering
        HashSet<char> unikaChars = new HashSet<char>(ord); //Gör ett hashset av de unika bokstäverna. Hashset liknar en lista/array i att den kan innehållar flera värden, men bara UNIKA värden, inga dubletter.

        foreach (char gissning in gissLista) // För varje gissad bokstav i listan
        {
            if (unikaChars.Contains(gissning)) // Om hashsetet innehåller en bokstav som har gissats, ta bort bokstaven från hashsetet.
            {
                unikaChars.Remove(gissning); // Ta bort den gissade bokstaven ifrån hashsetet, vi vet att den har gissats och behöver inte kollas igen. Så här trackar vi även ifall vi ska returnera sant eller falskt.
            }
        }

        return unikaChars.Count == 0; // Om unikaChars är 0, returnera true, annars false.
    }
}






//using System.ComponentModel;

//string[] ordLista = {"vispgrädde",
//      "ukulele",
//      "innebandyspelare",
//      "flaggstång",
//      "yxa",
//      "havsfiske",
//      "prisma",
//      "landsbygd",
//      "generositet",
//      "lyckosam",
//      "perrong",
//      "samarbeta",
//      "välartad"};

//string[] betydelseLista = {"Uppvispad grädde.",
//      "Ett fyrsträngat instrument med ursprung i Portugal.",
//      "En person som spelar sporten innebandy.",
//      "En mast man hissar upp en flagga på.",
//      "Verktyg för att hugga ved.",
//      "Fiske till havs.",
//      "Ett transparent optiskt element som bryter ljuset vid plana ytor.",
//      "Geografiskt område med lantlig bebyggelse.",
//      "En personlig egenskap där man vill dela med sig av det man har.",
//      "Att man ofta, eller för stunden har tur.",
//      "Den upphöjda yta som passagerare väntar på eller stiger på/av ett spårfordon.",
//      "Att arbeta tillsamans mot ett gemensamt mål.",
//      "Att någon är väluppfostrad, skötsam, eller lovande."};

//int antalGissningar = 5;
//List<string> gissningarLista = new List<string>();

//int nummer = slumpaNummer(); // Slumpa ett nummer tidigt så att den kan återanvändas.
//string ord = ordLista[nummer];
//string betydelse = betydelseLista[nummer];


//string ordgrej = string.Empty; //??
//while (svarsKoll(gissningarLista)) // Så länge gissningarLista inte innehåller alla bokstäver i ordet, loopa programmet
//{

//    Console.WriteLine("Hänga gubbe!\nFörsök att gissa ordet en bokstav i taget.\nDu har " + antalGissningar + " gissningar kvar.\n");
//    Console.WriteLine("****Ord: " + ord); // TA BORT
//    Console.WriteLine("\nDina gissningar: ");

//    string gissning = Console.ReadLine(); //Felhantera
//    gissningarLista.Add(gissning);

//    Console.WriteLine("\nGissning " + gissningKoll(gissning));
//    Console.WriteLine("****Bokstav: ");

//    foreach (string bokstav in gissningarLista)
//    {
//        Console.Write(bokstav + " ");

//    }

//    Console.WriteLine("\nAntal gissningar: " + antalGissningar);

//}

//Console.WriteLine("Du vann :)");


//int slumpaNummer() // Slumpa ett nummer mellan 0 och 14 och returnera som en int.
//{
//    Random rand = new Random();
//    return rand.Next(14);

//}

//string gissningKoll(string ordInput)
//{
//    ordgrej = ""; //Det gissade ordet printar ut kända chars. t.ex k_k_o för kakao om K och O är känt.
//    foreach (char i in ord) //För varje karaktär i ordet
//    {
//        foreach (string c in gissningarLista)//För varje bokstav i listan av gissningar
//        {
//            if (i.ToString() == c) // Om ordet innehåller en bokstav som har gissats, lägg till den i ordgrej för att bygga ihop stringen.
//            {
//                ordgrej += c;
//            }

//            else if (i.ToString() != c)
//            {
//                ordgrej += "_";

//            }
//        }
//    }
//    return ordgrej;
//}

//bool svarsKoll(List<String> svarsInput)
//{

//    // För varje Index i SvarsInput, lägg dem i b
//    //jämför ord och b, om 

//    string b = "";
//    //if (svarsinput.count == 0)
//    //{
//    //    return true;
//    //}

//    for (int i = 0; i < svarsInput.Count; i++)
//    {
//        b += svarsInput[i];
//    }

//    Console.WriteLine("*****GISSNINGSKOLL: " + gissningKoll(b));



//    Console.WriteLine("****svarsinput: " + b);


//    if (ord.Contains(b))
//    {   return false;   }

//    else { return true; }

//}