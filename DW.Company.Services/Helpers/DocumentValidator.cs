using DW.Company.Common;
using DW.Company.Contracts.Helpers;
using System.Linq;

namespace DW.Company.Services.Helpers
{
    public class DocumentValidator : IDocumentValidator
    {
        private string[] _cnpjWhiteList = new string[] { Constants.VALIDDOCUMENT };
        private bool ValidateModule11CPF(string value, string digits)
        {
            var _dititToValidate = int.Parse(digits.ElementAt(0).ToString());
            var _valueLength = value.Length;
            var _multiplicator = _valueLength + 1;
            var _sum = 0;
            for (var i = 0; i < _valueLength; i++)
            {
                var _number = int.Parse(value.ElementAt(i).ToString());
                _sum += _multiplicator * _number;
                _multiplicator--;
            }

            var _module = _sum % 11;
            var _digit = 11 - _module;
            if (_digit >= 10)
                _digit = 0;

            if (_dititToValidate != _digit)
                return false;

            if (digits.Length > 1)
                return ValidateModule11CPF($"{value}{_digit}", digits.Substring(1));
            return true;
        }

        private bool ValidadeModule11CNPJ(string value, string digits)
        {
            var _dititToValidate = int.Parse(digits.ElementAt(0).ToString());
            var _valueLength = value.Length;
            var _multiplicator = 2;
            var _sum = 0;
            for (var i = _valueLength - 1; i >= 0; i--)
            {
                var _number = int.Parse(value.ElementAt(i).ToString());
                if (_multiplicator > 9)
                    _multiplicator = 2;
                _sum += _multiplicator * _number;
                _multiplicator++;
            }

            var _module = _sum % 11;
            var _digit = 11 - _module;
            if (_digit >= 10)
                _digit = 0;

            if (_dititToValidate != _digit)
                return false;

            if (digits.Length > 1)
                return ValidadeModule11CNPJ($"{value}{_digit}", digits.Substring(1));
            return true;
        }

        public bool IsCnpjDocumentValid(string value)
        {
            if (value.Length != 14)
                return false;
            if (_cnpjWhiteList.Any(a => a.Equals(value)))
                return true;
            return ValidadeModule11CNPJ(value.Substring(0, 12), value.Substring(12, 2));
        }

        public bool IsCpfDocumentValid(string value)
        {
            if (value.Length != 11)
                return false;
            return ValidateModule11CPF(value.Substring(0, 9), value.Substring(9, 2));
        }

        public bool IsAValidDocument(string value)
        {
            if (value.Length == 14)
                return IsCnpjDocumentValid(value);
            if (value.Length == 11)
                return IsCpfDocumentValid(value);
            return false;
        }
    }
}
