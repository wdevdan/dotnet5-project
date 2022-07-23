using Microsoft.Extensions.Options;
using DW.Company.Contracts.Helpers;
using DW.Company.Entities.Value;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace DW.Company.Services.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly HashingOptions _options;
        private const int SaltSize = 16;
        private const int KeySize = 32;
        public PasswordHasher(IOptions<HashingOptions> options)
        {
            _options = options.Value;
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, _options.Iterations, HashAlgorithmName.SHA512))
            {
                var _key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var _salt = Convert.ToBase64String(algorithm.Salt);
                return BuildHash(_options.Iterations, _salt, _key);
            }
        }

        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
        {
            var _parts = SplitHash(hash);
            var _iterations = Convert.ToInt32(_parts[0]);
            var _salt = Convert.FromBase64String(_parts[1]);
            var _key = Convert.FromBase64String(_parts[2]);
            var _needsUpgrade = _iterations != _options.Iterations;
            using (var algorithm = new Rfc2898DeriveBytes(password, _salt, _iterations, HashAlgorithmName.SHA512))
            {
                var _keyToCheck = algorithm.GetBytes(KeySize);
                var _verified = _keyToCheck.SequenceEqual(_key);
                return (_verified, _needsUpgrade);
            }
        }

        private string[] SplitHash(string hash)
        {
            var _parts = hash.Split('.', 3);
            ValidateHashParts(_parts);
            return _parts;
        }

        private void ValidateHashParts(string[] parts)
        {
            if (parts.Length != 3)
                throw new FormatException("Unexpected hash format. Should be formatted as `{iterations}.{salt}.{hash}`");
        }

        private string BuildHash(int iterations, string salt, string key)
        {
            return $"{iterations}.{salt}.{key}";
        }
    }
}
