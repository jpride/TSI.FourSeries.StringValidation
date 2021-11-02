using System;
using System.Collections.Generic;
using System.Text;
using Crestron.SimplSharp;




namespace TSI.FourSeries.StringValidation
{
    public class IPAddressValidator

    {
        public IPAddressValidator()
        {
        }

        /// <summary>
        /// Returns 0 when ipString is not a valid IP address, 1 when it is
        /// </summary>
        /// <param name="ipa">enter the IP address to be validated</param>

  

        public ushort ValidateIPv4Address(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return 0;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return 0;
            }


            //Ternary Oparator  [condition or Bool ? value if true : value if false] **suing the "_" discarad operator in the output side of TryParse
            return (ushort) (System.Net.IPAddress.TryParse(ipString, out _) ? 1 : 0);
        }


        public string GetValidIPv4Address(string ipString)
        {
            if (ValidateIPv4Address(ipString) == 1)
            {
                return ipString;
            }
            else
            {
                CrestronConsole.PrintLine("Invalid IP Address: {0}", ipString);
                return string.Empty;
            }
        
        }
    }
}