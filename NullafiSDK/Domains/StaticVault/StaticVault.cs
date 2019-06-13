using Nullafi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault.Managers.Address;
using Nullafi.Domains.StaticVault.Managers.DateOfBirth;
using Nullafi.Domains.StaticVault.Managers.DriversLicense;
using Nullafi.Domains.StaticVault.Managers.FirstName;
using Nullafi.Domains.StaticVault.Managers.Gender;
using Nullafi.Domains.StaticVault.Managers.Generic;
using Nullafi.Domains.StaticVault.Managers.LastName;
using Nullafi.Domains.StaticVault.Managers.Passport;
using Nullafi.Domains.StaticVault.Managers.PlaceOfBirth;
using Nullafi.Domains.StaticVault.Managers.Race;
using Nullafi.Domains.StaticVault.Managers.Random;
using Nullafi.Domains.StaticVault.Managers.Ssn;
using Nullafi.Domains.StaticVault.Managers.TaxPayer;
using Nullafi.Domains.StaticVault.Managers.VehicleRegistration;

namespace Nullafi.Domains.StaticVault
{
    public class StaticVault
    {
        internal readonly Client Client;
        private readonly Security _security;

        public string VaultId { get; }
        public string VaultName { get; }
        public string MasterKey { get; }

        public AddressManager Address { get; }
        public DateOfBirthManager DateOfBirth { get; }
        public DriversLicenseManager DriversLicense { get; }
        public FirstNameManager FirstName { get; }
        public GenderManager Gender { get; }
        public GenericManager Generic { get; }
        public LastNameManager LastName { get; }
        public PassportManager Passport { get; }
        public PlaceOfBirthManager PlaceOfBirth { get; }
        public RaceManager Race { get; }
        public RandomManager Random { get; }
        public SsnManager Ssn { get; }
        public TaxPayerManager TaxPayer { get; }
        public VehicleRegistrationManager VehicleRegistration { get; }


        private StaticVault(Client client, string vaultId, string vaultName, string masterKey)
        {
            Client = client;
            VaultId = vaultId;
            VaultName = vaultName;
            MasterKey = masterKey;
            _security = new Security();

            Address = new AddressManager(this);
            DateOfBirth = new DateOfBirthManager(this);
            DriversLicense = new DriversLicenseManager(this);
            FirstName = new FirstNameManager(this);
            Gender = new GenderManager(this);
            Generic = new GenericManager(this);
            LastName = new LastNameManager(this);
            Passport = new PassportManager(this);
            PlaceOfBirth = new PlaceOfBirthManager(this);
            Race = new RaceManager(this);
            Random = new RandomManager(this);
            Ssn = new SsnManager(this);
            TaxPayer = new TaxPayerManager(this);
            VehicleRegistration = new VehicleRegistrationManager(this);
        }

        public string Hash(string value)
        {
            return _security.Hmac.Hash(value, Client.HashKey);
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = _security.Aes.GenerateStringIv();
            var byteMasterKey = Convert.FromBase64String(MasterKey);
            return _security.Aes.Encrypt(MasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            return _security.Aes.Decrypt(MasterKey, iv, authTag, value);
        }

        public static async Task<StaticVault> CreateStaticVault(Client client, string name, List<string> tags)
        {
            var security = new Security();

            var payload = new StaticVaultPayload()
            {
                Name = name,
                Tags = tags
            };

            var response = await client.Post<StaticVaultPayload, StaticVaultResponse>("/vault/static", payload);

            return new StaticVault(client, response.Id, response.Name, security.Aes.GenerateStringMasterKey());
        }

        public static async Task<StaticVault> RetrieveStaticVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<StaticVaultResponse>($"/vault/static/{vaultId}");
            return new StaticVault(client, vaultId, response.Name, masterKey);
        }
    }
}
