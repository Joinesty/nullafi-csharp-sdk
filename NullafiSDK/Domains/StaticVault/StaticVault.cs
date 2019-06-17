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
    /// <summary>
    /// 
    /// </summary>
    public class StaticVault
    {
        internal readonly Client Client;
        private readonly Security _security;

        /// <summary>
        /// 
        /// </summary>
        public string VaultId { get; }

        /// <summary>
        /// 
        /// </summary>
        public string VaultName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string MasterKey { get; }

        /// <summary>
        /// 
        /// </summary>
        public AddressManager Address { get; }

        /// <summary>
        /// 
        /// </summary>
        public DateOfBirthManager DateOfBirth { get; }

        /// <summary>
        /// 
        /// </summary>
        public DriversLicenseManager DriversLicense { get; }

        /// <summary>
        /// 
        /// </summary>
        public FirstNameManager FirstName { get; }

        /// <summary>
        /// 
        /// </summary>
        public GenderManager Gender { get; }

        /// <summary>
        /// 
        /// </summary>
        public GenericManager Generic { get; }

        /// <summary>
        /// 
        /// </summary>
        public LastNameManager LastName { get; }

        /// <summary>
        /// 
        /// </summary>
        public PassportManager Passport { get; }

        /// <summary>
        /// 
        /// </summary>
        public PlaceOfBirthManager PlaceOfBirth { get; }

        /// <summary>
        /// 
        /// </summary>
        public RaceManager Race { get; }

        /// <summary>
        /// 
        /// </summary>
        public RandomManager Random { get; }

        /// <summary>
        /// 
        /// </summary>
        public SsnManager Ssn { get; }

        /// <summary>
        /// 
        /// </summary>
        public TaxPayerManager TaxPayer { get; }

        /// <summary>
        /// 
        /// </summary>
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

        internal AesEncryptedData Encrypt(string value)
        {
            var iv = _security.Aes.GenerateStringIv();
            var byteMasterKey = Convert.FromBase64String(MasterKey);
            return _security.Aes.Encrypt(MasterKey, iv, value);
        }

        internal string Decrypt(string iv, string authTag, string value)
        {
            return _security.Aes.Decrypt(MasterKey, iv, authTag, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns></returns>
        public static async Task<StaticVault> RetrieveStaticVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<StaticVaultResponse>($"/vault/static/{vaultId}");
            return new StaticVault(client, vaultId, response.Name, masterKey);
        }
    }
}
