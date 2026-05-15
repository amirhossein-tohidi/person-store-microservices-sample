using System.Text.RegularExpressions;

namespace PersonService.Api.API.Validators;

public static class NationalCodeValidator
{
    public static bool IsValid(string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode))
            return false;

        if (!Regex.IsMatch(nationalCode, @"^\d{10}$"))
            return false;

        var check = nationalCode[9] - '0';

        var sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += (nationalCode[i] - '0') * (10 - i);
        }

        var remainder = sum % 11;

        return remainder < 2
            ? check == remainder
            : check == 11 - remainder;
    }
}