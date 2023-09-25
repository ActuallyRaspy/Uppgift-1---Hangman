using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

bool spelaIgen = true;
while (spelaIgen) // Sålänge spelaren vill fortsätta spela, loopa all kod.
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

    int nummer = slumpaNummer();

    int antalGissningar = 5;
    string ord = ordLista[nummer];
    string betydelse = betydelseLista[nummer];
    string gissning = ""; //Rensar och behåller den nuvarande loopens gissning
    string gissningarLista = ""; //Chars som har gissats
    bool ogiltigGissning = false;
    bool redanGissad = false;

    

    while (!kollaOrdKorrekt(gissningarLista, ord) && antalGissningar > 0) //Medans gissningarLista inte innehåller alla bokstäver i ord och gissningar är mer än 0, loopa.
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
            if (gissningarLista.Contains(gissning))
            {
                redanGissad = true;
            }

            else
            {
                string jämförOrd = byggUppOrd(gissningarLista, ord);
                gissningarLista += gissning[0];

                if (jämförOrd == byggUppOrd(gissningarLista, ord))
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

    else if (kollaOrdKorrekt(gissningarLista, ord))
    {
        byggSpelText();

        Console.WriteLine("\nOrdförklaring: " + betydelse + "\n");
        Console.WriteLine("Du har vunnit, vill du spela igen? y/n");

        do 
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

    void byggSpelText() // Bygger dynamiskt upp speltexten som visas i konsolen
    {
        gissning = ""; 
        Console.Clear(); 
        Console.WriteLine("Hänga gubbe!\nGissa ordet en bokstav i taget."); 
        Console.WriteLine("\nOrd: " + byggUppOrd(gissningarLista, ord));
        Console.WriteLine("\nAntal gissningar kvar: " + antalGissningar.ToString());
        Console.WriteLine("\nDina gissningar: " + visaGissningar(gissningarLista));
    }
}

int slumpaNummer() // Slumpa ett nummer mellan 0 och 13 och returnera som en int.
{
    Random rand = new Random();
    return rand.Next(13);

}

bool kollaOrdKorrekt(string gissLista, string ord) // Jämför spelarens gissade bokstäver med spelets ord
{

    string unikaChars = "";

    foreach (char bokstav in ord)
    {
        if (!unikaChars.Contains(bokstav))
        {
            unikaChars += bokstav;
        }

        else continue;
    }

    foreach (char givenBokstav in ord) 
    {
        foreach (char gissadBokstav in gissLista) 
        {
            if (gissadBokstav == givenBokstav) // Om den givna bokstaven har gissats, byt ut den mot Empty. När stringens Len är 0 vet vi att ordet har hittats
            {
                unikaChars = unikaChars.Replace(givenBokstav.ToString(), string.Empty);
            }
        }
    }

    return unikaChars.Length == 0; // Om ordet har hittats, skicka true för att avsluta eller fortsätta spelet
}

string byggUppOrd(string gissLista, string ord) //Bygger upp stringen för det som har gissats i ordet. 
{
    string byggtOrd = "";
    int i = 0; // Räknar om en bokstav har använts eller det behövs ett _
    foreach (char bokstav in ord) 
    {
        foreach (char gissning in gissLista) 
        {
            i = 0; 
            if (gissning == bokstav) 
            {
                byggtOrd += gissning; 
                i++; 
                break; 
            }
        }

        if (i != 1) // Om räknaren inte står på 1 (alltså att bokstaven inte finns i gissLista pga ovan loop) lägg till ett _ i ordet  
        {
            byggtOrd += "_";

        }
    }
    return byggtOrd;
}

bool verifieraInput(string input) // Enkel inputsanering, endast enstaka bokstäver kommer igenom
{
    if (input.Count() == 1 && Char.IsLetter(input[0]))
    {
        return true;
    }

    else return false;
}

string visaGissningar(string gissningarLista) // Returnera vilka bokstäver som finns i gissningarLista i formatet: a, b, c, d, 
{
    string gissningar = "";
    foreach (char gissning in gissningarLista)
    {
        gissningar += gissning + ", ";
    }
    return gissningar;
}