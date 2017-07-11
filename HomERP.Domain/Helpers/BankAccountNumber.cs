using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomERP.Domain.Helpers
{
    public class BankAccountNumber
    {
        private string accountNumber;

        public BankAccountNumber() { }

        private BankAccountNumber(string number)
        {
            this.accountNumber = number;
        }

        public static bool TryParse(string numberstring, out BankAccountNumber accNumber)
        {
            //normalize
            numberstring = Regex.Replace(numberstring, "[^0-9]", "");
            string numberToCheck = "PL" + numberstring;
            numberToCheck = numberToCheck.Substring(4) + numberToCheck.Substring(0, 4);

            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder builder = new StringBuilder();
            foreach (char letter in numberToCheck)
            {
                if(letters.Contains(letter))
                {
                    string value = (letters.IndexOf(letter) + 10).ToString();
                    builder.Append(value);
                }
                else
                {
                    builder.Append(letter);
                }
            }
            int checksum = 0;
            foreach(char sign in builder.ToString())
            {
                checksum *= 10;
                checksum += int.Parse(sign.ToString());
                checksum %= 97;
            }
            if (checksum == 1)
            {
                accNumber = new BankAccountNumber(numberstring);
            }
            else
            {
                accNumber = new BankAccountNumber("");
            }
            return checksum == 1;
        }

        public override string ToString()
        {
            if (this.accountNumber.Length!=26)
            { return String.Empty; }
            StringBuilder formattedNumber = new StringBuilder();
            formattedNumber.Append(this.accountNumber.Substring(0, 2));
            for (byte i=1; i<7; i++)
            {
                formattedNumber.Append(" " + this.accountNumber.Substring(i * 4-2, 4));
            }
            return formattedNumber.ToString();
        }
    }
}
