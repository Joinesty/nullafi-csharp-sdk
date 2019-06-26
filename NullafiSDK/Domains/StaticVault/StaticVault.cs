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
    /// Static Vault
    /// </summary>
    public class StaticVault
    {
        internal readonly Client Client;
        private readonly Security _security;

        /// <summary>
        /// Id of vault instantiated 
        /// </summary>
        public string VaultId { get; }

        /// <summary>
        /// Name of vault instantiated 
        /// </summary>
        public string VaultName { get; }

        /// <summary>
        /// Master key to vault instantiated
        /// </summary>
        public string MasterKey { get; }

        /// <summary>
        /// Address Manager to create aliases
        /// </summary>
        public AddressManager Address { get; }

        /// <summary>
        /// DateOfBirth Manager to create aliases
        /// </summary>
        public DateOfBirthManager DateOfBirth { get; }

        /// <summary>
        /// DriversLicense Manager to create aliases
        /// </summary>
        public DriversLicenseManager DriversLicense { get; }

        /// <summary>
        /// FirstName Manager to create aliases
        /// </summary>
        public FirstNameManager FirstName { get; }

        /// <summary>
        /// Gender Manager to create aliases
        /// </summary>
        public GenderManager Gender { get; }

        /// <summary>
        /// Generic Manager to create aliases
        /// </summary>
        public GenericManager Generic { get; }

        /// <summary>
        /// LastName Manager to create aliases
        /// </summary>
        public LastNameManager LastName { get; }

        /// <summary>
        /// Passport Manager to create aliases
        /// </summary>
        public PassportManager Passport { get; }

        /// <summary>
        /// PlaceOfBirth Manager to create aliases
        /// </summary>
        public PlaceOfBirthManager PlaceOfBirth { get; }

        /// <summary>
        /// Race Manager to create aliases
        /// </summary>
        public RaceManager Race { get; }

        /// <summary>
        /// Random Manager to create aliases
        /// </summary>
        public RandomManager Random { get; }

        /// <summary>
        /// SSN Manager to create aliases
        /// </summary>
        public SsnManager Ssn { get; }

        /// <summary>
        /// TaxPayer Manager to create aliases
        /// </summary>
        public TaxPayerManager TaxPayer { get; }

        /// <summary>
        /// VehicleRegistration Manager to create aliases
        /// </summary>
        public VehicleRegistrationManager VehicleRegistration { get; }

        /// <summary>
        /// Create an instance of StaticVault
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vaultId"></param>
        /// <param name="vaultName"></param>
        /// <param name="masterKey"></param>
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

        /// <summary>
        /// Generate a hash for the real data
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Hashed value</returns>
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
        /// Create the API to create a new static vault
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, name, tags, createdAt, iv, authTag, masterKey, sessionKey</returns>
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
        /// Retrieve the static vault from id
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns>Returns a promise containing: id, name, tags, createdAt</returns>
        public static async Task<StaticVault> RetrieveStaticVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<StaticVaultResponse>($"/vault/static/{vaultId}");
            return new StaticVault(client, vaultId, response.Name, masterKey);
        }

        /// <summary>
        /// Delete the static vault from id
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vaultId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public static async Task DeleteStaticVault(Client client, string vaultId)
        {
            await client.Delete($"/vault/static/{vaultId}");
        }
    }
}
