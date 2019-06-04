using NullafiSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault.Managers.Address;
using NullafiSDK.Domains.StaticVault.Managers.DateOfBirth;
using NullafiSDK.Domains.StaticVault.Managers.DriversLicense;
using NullafiSDK.Domains.StaticVault.Managers.FirstName;
using NullafiSDK.Domains.StaticVault.Managers.Gender;
using NullafiSDK.Domains.StaticVault.Managers.Generic;
using NullafiSDK.Domains.StaticVault.Managers.LastName;
using NullafiSDK.Domains.StaticVault.Managers.Passport;
using NullafiSDK.Domains.StaticVault.Managers.PlaceOfBirth;
using NullafiSDK.Domains.StaticVault.Managers.Race;
using NullafiSDK.Domains.StaticVault.Managers.Random;
using NullafiSDK.Domains.StaticVault.Managers.Ssn;
using NullafiSDK.Domains.StaticVault.Managers.TaxPayer;
using NullafiSDK.Domains.StaticVault.Managers.VehicleRegistration;

namespace NullafiSDK.Domains.StaticVault
{
    public class StaticVault
    {
        public readonly Client client;
        readonly Security security;

        public string VaultId { get; set; }
        public string VaultName { get; set; }
        public string MasterKey { get; set; }

        public AddressManager address { get; }
        public DateOfBirthManager dateOfBirth { get; }
        public DriversLicenseManager driversLicense { get; }
        public FirstNameManager firstName { get; }
        public GenderManager gender { get; }
        public GenericManager generic { get; }
        public LastNameManager lastName { get; }
        public PassportManager passport { get; }
        public PlaceOfBirthManager placeOfBirth { get; }
        public RaceManager race { get; }
        public RandomManager random { get; }
        public SsnManager ssn { get; }
        public TaxPayerManager taxPayer { get; }
        public VehicleRegistrationManager vehicleRegistration { get; }



        private StaticVault(Client client, string vaultId, string vaultName, string masterKey)
        {
            this.client = client;
            this.VaultId = vaultId;
            this.VaultName = vaultName;
            this.MasterKey = masterKey;
            this.security = new Security();

            this.address = new AddressManager(this);
            this.dateOfBirth = new DateOfBirthManager(this);
            this.driversLicense = new DriversLicenseManager(this);
            this.firstName = new FirstNameManager(this);
            this.gender = new GenderManager(this);
            this.generic = new GenericManager(this);
            this.lastName = new LastNameManager(this);
            this.passport = new PassportManager(this);
            this.placeOfBirth = new PlaceOfBirthManager(this);
            this.race = new RaceManager(this);
            this.random = new RandomManager(this);
            this.ssn = new SsnManager(this);
            this.taxPayer = new TaxPayerManager(this);
            this.vehicleRegistration = new VehicleRegistrationManager(this);
        }

        public string Hash(string value)
        {
            return this.security.HMACHash(value, this.client.HashKey);
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = this.security.AesGenerateInitializationVector();
            return this.security.AesEncrypt(this.MasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            return this.security.AesDecrypt(this.MasterKey, iv, authTag, value);
        }

        public async static Task<StaticVault> CreateStaticVault(Client client, string name, List<string> tags)
        {
            var security = new Security();

            var payload = new StaticVaultPayload()
            {
                Name = name,
                Tags = tags
            };

            var response = await client.Post<StaticVaultPayload, StaticVaultResponse>("/vault/static", payload);

            return new StaticVault(client, response.Id, response.Name, security.AesGenerateMasterKey());
        }

        public async static Task<StaticVault> RetrieveStaticVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<StaticVaultResponse>($"/vault/static/${vaultId}");
            return new StaticVault(client, vaultId, response.Name, masterKey);
        }
    }
}
