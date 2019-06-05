using Nullafi.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
        public readonly Client client;
        readonly Security security;

        public string VaultId { get; set; }
        public string VaultName { get; set; }
        public string MasterKey { get; set; }

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
            this.client = client;
            this.VaultId = vaultId;
            this.VaultName = vaultName;
            this.MasterKey = masterKey;
            this.security = new Security();

            this.Address = new AddressManager(this);
            this.DateOfBirth = new DateOfBirthManager(this);
            this.DriversLicense = new DriversLicenseManager(this);
            this.FirstName = new FirstNameManager(this);
            this.Gender = new GenderManager(this);
            this.Generic = new GenericManager(this);
            this.LastName = new LastNameManager(this);
            this.Passport = new PassportManager(this);
            this.PlaceOfBirth = new PlaceOfBirthManager(this);
            this.Race = new RaceManager(this);
            this.Random = new RandomManager(this);
            this.Ssn = new SsnManager(this);
            this.TaxPayer = new TaxPayerManager(this);
            this.VehicleRegistration = new VehicleRegistrationManager(this);
        }

        public string Hash(string value)
        {
            return this.security.hmac.Hash(value, this.client.HashKey);
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = this.security.aes.GenerateIv();
            byte[] byteMasterKey = Convert.FromBase64String(this.MasterKey);
            return this.security.aes.Encrypt(byteMasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            byte[] byteIv = Convert.FromBase64String(iv);
            byte[] byteAuthTag = Convert.FromBase64String(authTag);
            byte[] byteMasterKey = Convert.FromBase64String(this.MasterKey);

            return this.security.aes.Decrypt(byteMasterKey, byteIv, byteAuthTag, value);
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

            return new StaticVault(client, response.Id, response.Name, security.aes.GenerateStringMasterKey());
        }

        public async static Task<StaticVault> RetrieveStaticVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<StaticVaultResponse>($"/vault/static/{vaultId}");
            return new StaticVault(client, vaultId, response.Name, masterKey);
        }
    }
}
