using System;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes
using System.Text.RegularExpressions;

namespace TSI.FourSeries.StringValidation
{
    public class NVXDeviceNameValidator
    {

        public NVXDeviceNameValidator()
        {

        }

        /// <summary>
        /// This method will replace all occurrences of illegal characters defined in the illegalPattern string with the substituteString. It will also remove more than 1 sequential occurrence of the substituteString
        /// </summary>
        /// <param name="testString">This is the string to validate</param>
        /// <param name="substituteString">This is the replacement string for illegal characters</param>
        /// <returns>Returns a valid string</returns>
        public string ValidateDeviceName(string testString, string substituteString)
        {
            string illegalPattern = @"[\\~`!@#$%^&*()_=+{}|,./\?;:\s]";  //list of illegal characters and space '\s'
            string repeatCharsPattern = "[" + substituteString + "]{2,}"; //patterns to determine sequential characters of the substitute string, these will be filtered out        

            try
            {
                //Remove leading and trailing white space
                testString = testString.Trim();

                //remove illegal characters from the pattern above
                string validString = Regex.Replace(testString, illegalPattern, substituteString);

                //remove any repeated substitution chars
                validString = Regex.Replace(validString, repeatCharsPattern, substituteString);

                //remove replacement string from end of string
                //validString = entire validString up to last char + a 1 char string that is either not replaced because it is valid, or the empty string because it is the substitution string (Director doesnt like even the allowed '-' at the end of a name
                validString = validString.Substring(0, validString.Length - 1) + Regex.Replace(validString.Substring(validString.Length - 1), substituteString, String.Empty);

                return validString;
            }

            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error in ValidateString: {0}", e.Message);
                return String.Empty;
            }
        }

        //overloaded method that does not define the substituteString, assumes "-" and calls first method definition
        public string ValidateDeviceName(string testString)
        {
            return ValidateDeviceName(testString, "-");
        }
    }
            
}
