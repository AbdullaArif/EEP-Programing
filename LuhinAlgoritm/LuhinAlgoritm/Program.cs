namespace LuhinAlgoritm
{
   using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a card number (without spaces):");
        string cardNumber = Console.ReadLine();

        if (IsLuhnValid(cardNumber))
        {
            Console.WriteLine("The card number is valid.");
        }
        else
        {
            Console.WriteLine("The card number is not valid.");
        }
    }

    static bool IsLuhnValid(string cardNumber)
    {
        int total = 0;
        bool doubleDigit = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (doubleDigit)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            total += digit;
            doubleDigit = !doubleDigit;
        }

        return (total % 10 == 0);
    }
}

}