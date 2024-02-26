using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleComplexiteit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Voer een woord in (druk op Enter om te stoppen): ");
                string woord = Console.ReadLine().ToLower();
                // Controleer of het woord leeg is
                if (woord == string.Empty)
                {
                    Console.WriteLine("Bedankt en tot ziens.");
                    break;
                }

                int aantalLetters = woord.Length;
                int aantalLettergrepen = AantalLettergrepen(woord);
                double complexiteit = Complexiteit(woord, aantalLetters, aantalLettergrepen);

                Console.WriteLine($"de Complexiteit: {complexiteit}");
                Console.WriteLine($"aantal lettergrepen: {aantalLettergrepen}");
                Console.WriteLine($"aantal karakters: {aantalLetters}");

            }
            Console.ReadLine();
        }

        static bool IsKlinker(char letter)
        {
            return "aeiou".IndexOf(letter) != -1;
        }

        static int AantalLettergrepen(string woord)
        {
            int aantalLettergrepen = 0;
            bool vorigeWasKlinker = false;

            foreach (char letter in woord)
            {
                if (IsKlinker(letter) && !vorigeWasKlinker)
                {
                    aantalLettergrepen++;
                }

                vorigeWasKlinker = IsKlinker(letter);
            }

            return aantalLettergrepen;
        }

        static double Complexiteit(string woord, int aantalLetters, int aantalLettergrepen)
        {
            double complexiteit = aantalLetters / 3.0 + aantalLettergrepen;

            if (woord.Contains('x') || woord.Contains('y') || woord.Contains('q'))
            {
                complexiteit++;
            }

            return Math.Round(complexiteit, 1);
        }
    }
}

