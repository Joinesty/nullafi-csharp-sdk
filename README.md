<!-- Nullafi C# SDK
=============== -->

<!-- A C# interface to the [Nullafi API](http://enterprise-api.nullafi.com/docs).

- [Installation](#installation)
- [Getting Started](#getting-started)
- [Copyright and License](#copyright-and-license) -->


Installation
------------

Nullafi SDK is supported on .NET Core 2.1 or .NET Framework 4.6.

Getting Started
---------------
To get started with the SDK as a new developer, one must create a developer account. Please talk to a member of our team to get access to our signup page (<a href="https://calendly.com/chicago-management-team?__hstc=38388668.72758c7115c158dc83fe59c7e8a250d7.1572635006384.1583341260553.1583770309230.9&__hssc=38388668.1.1583928653788&__hsfp=853366638&hsCtaTracking=0f86f8a4-1077-4bcd-8810-716fb9522d51%7C4e4f872c-8023-433d-90e0-8c42d12f14fb" target="_blank">HERE</a>). As an account owner, you can retrieve the API key by going to the settings page, and selecting the 'API Key' tab. You may manage API key generation for the SDK from here. Create a new key and store the key value somewhere secure, as Nullafi will not store this key.

The full documentation can be found <a href="NullafiSDK/NullafiSDK.md"> here </a>

**Note:** Make sure to implement the nullafi-sdk in back end products only. Implementing the nullafi key on a front end product will expose the key to the public, and risk exposing private data. 

```c#

public async Task RunExample() {
	//We recommend storing your key in a secure non-public facing env file
	static readonly string API_KEY = Environment.GetEnvironmentVariable("NULLAFI_APIKEY");

	// Initialize the SDK with your API credentials
	var SDK = new NullafiSDK(API_KEY);

	// Create a basic API client, which will also authenticate your client. 
	// Client authentication will expire after 60 minutes
	var client = await SDK.CreateClient();

	// Get your own user object from the Nullafi API
	// All client methods return a promise that resolves to the results of the API call,
	// or rejects when an error occurs
	// Adding tags is an important way to retrieve data
	StaticVault staticVault = await client.CreateStaticVault("my-static-vault",  new List<string>() {"my-tag-1", "my-tag-2" });
	String name = "example";
	String gender = "male";
	FirstNameResponse created = await vault.FirstName.Create(name, gender);

	Console.WriteLine("//// FirstNameExample.CreateWithGender:");
	Console.WriteLine("/// Name: " + name);
	Console.WriteLine(created);
	/*
	//// FirstNameExample.CreateWithGender:
	/// Name: example
	{	
		"Id":"28afbd3d-05d4-4357-962d-ed562c49b776",
		"firstname":"example",
		"firstnameAlias":"Cameron",
		"Iv":"0SK+dWKGlX1mkK+veDarHw==",
		"AuthTag":"D9GcqI+WzlHfYr0u9ff04g==",
		"Tags":['my-tag-1', 'my-tag-2'],
		"CreatedAt":"2019-06-24T14:24:07.018Z"
	}
	*/
	return FirstNameResponse;
}
```

Aliases may be retrieved and deleted using these methods:

```c#
	// An alias can be retrieved by it's ID
	FirstNameResponse retrieved = await vault.FirstName.Retrieve('e490157b23534215b0369a2685aab47g');

	// An alias may also be retrieved using it's real value, as well as any tags that may help identify the data point. This will return an array of values matching the given parameters. 
	FirstNameRealDataResponse retrieved = await vault.FirstName.RetrieveFromRealData('John', new List<string>() {'my-tag-1', 'my-tag-2'})

	// Deleting an alias will also use ID
	var deleteFirstNameAliasResponse = await vault.FirstName.Delete('e490157b23534215b0369a2685aab47g');
```

Authentication
------------
When a client is created, the client instance will be authenticated for a 60 minute period. After this time, you may either create a new client or refresh the existing client. 
```c#
client.Authenticate(NULLAFI_API_KEY);
```


Static Vaults
------------
Static vaults are used to hold all created aliases for non transactional data. Static Vaults can be managed through the Static Vault class.

There is no limit on how many types of data may be stored in one static vault. It is up to users to determine how to split their data into vaults. Note that the master key must be stored to retrieve the vault at later times.  
A Static Vault can be created like this:
```c#
public async Task CreateStaticVault() {
	var client = await SDK.CreateClient();
	StaticVault staticVault = await client.CreateStaticVault("my-static-vault",  new List<string>() {"my-tag-1", "my-tag-2" });
	Console.WriteLine("//// FirstNameExample.CreateWithGender:");
	Console.WriteLine(staticVault);
	/*
		output example:
		{ 
			"Id":'e490157b23534215b0369a2685aab47g', 
			"Name":'my-static-vault',
			"MasterKey":'MASTER_KEY',
			"Tags":['my-tag-1', 'my-tag-2'], 
			"CreatedAt": '2018-07-13 T01:00:00Z' 
		}
	*/
	return staticVault;
}
```
The **ID** as well as the **Master Key** from the output will be used to retrieve the vault. These values must be stored in your database to retrieve the vault.
Retrieving a vault looks like this: 

```c#
public async Task RetrieveStaticVault() {
	//Authenticated client
	var client = await SDK.CreateClient();
	// ID and Master key should be stored and retrieved from database
	static readonly staticVaultID = 'e490157b23534215b0369a2685aab47g';
	static readonly staticVaultMasterKey = 'MASTER_KEY';
	var staticVault = await client.RetrieveStaticVault(staticVaultID, staticVaultMasterKey);
	return staticVault;
}
```

You can also delete a vault using the vault ID. Deleting the vault will also remove all aliases stored within, so make sure data is properly saved before deleting a vault. Deleting a vault will return a response with a key of 'ok' and a boolean value. 

```c#
public async Task DeleteStaticVault() {
	//Authenticated client
	var client = await SDK.CreateClient();
	// ID should be stored and retrieved from database
	static readonly staticVaultID = 'e490157b23534215b0369a2685aab47g';
	var staticVaultResponse = await client.DeleteStaticVault(staticVaultID);
	return staticVaultResponse;
}
```

Static Data Types
------------
### Address
Generates a fake address that will not trace to a real location. An optional parameter of state may be provided to choose the state associated with the fake address.

Address example:
```c#
// Example format/output
// street, city, state abbreviation zipcode, USA
// 43520 Hills Flat, East Aricchester, AK 99761, USA

// example call
var addressAliasObj = await staticVault.Address.Create('138 Congress St, Portland, ME 04101', 'ME', new List<string>() {"my-tag-1", "my-tag-2" });
```

Providing an incorrect state abbreviation will return a random state. The list of acceptable inputs is below.
```text
'AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DC', 'DE', 'FL', 'GA', 'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY',
'LA', 'MA', 'MD', 'ME', 'MI', 'MN', 'MO', 'MS', 'MT', 'NC', 'ND', 'NE', 'NH', 'NJ', 'NM', 'NV', 'NY', 'OH',
'OK', 'OR', 'PA', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
```
### Date of birth
Will generate a new date between the year span of 1949 and 2001. Year(YYYY) and month(MM) are both optional parameters that will set the date to the corresponding year and/or month. 

Date of birth example:
```c#
// example format/output
// YYYY-MM-DD
// 1980-12-20

//providing the optional year and month arguments 
var dobAliasObj = await staticVault.DateOfBirth.Create('1999-07-02', '1999', '07', new List<string>() {'my-dob-tag1', 'my-dob-tag2'});
```
### Driver's license
Generates a randomly generated combination of numbers and letters based on the format of each state's format. A state may be provided as an optional parameter to return a license for that state. A list of formats may be viewed [**here**](https://ntsi.com/drivers-license-format/).

Providing an incorrect state abbreviation will return a random state. The list of acceptable inputs is below.
```text
'AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DC', 'DE', 'FL', 'GA', 'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY',
'LA', 'MA', 'MD', 'ME', 'MI', 'MN', 'MO', 'MS', 'MT', 'NC', 'ND', 'NE', 'NH', 'NJ', 'NM', 'NV', 'NY', 'OH',
'OK', 'OR', 'PA', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
```

Example call: 
```c#
//example call with optional state
var driverslicenseAliasObj = await staticVault.DriversLicense.Create('123456789', 'NY', new List<string>()  {'my-driversLicense-tag1', 'my-driversLicense-tag2'});
```
### First name
Generates a random name with the optional input of gender. 

Genders available are:
```text
"male", 
"female"
```
Example call:
```c#
var firstNameAliasObj = await staticVault.FirstName.Create('John', new List<string>() {'my-fName-tag1', 'my-fName-tag2'});
```
### Gender
Generates a random gender from a list.
Output options are: 
```text
"Male",
"Female",
"Other",
"Don't want to say"
```

Example call:
```c#
var genderAliasObj = await staticVault.Gender.Create('male', new List<string>() {'my-gender-tag1', 'my-gender-tag2'});
```
### Generic
Generic takes a regular expression as input and will generate a value matching that expression. Use this to create formats not currently supported. Some example usages are for prescriptions, nations, and non-supported passport numbers. The template used to generate values will not be saved.

Example Generic Values:
```c#
// IP Number: [0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}
// Mac Address: [0-9A-F]{2}\:[0-9A-F]{2}\:[0-9A-F]{2}\:[0-9A-F]{2}\:[0-9A-F]{2}\:[0-9A-F]{2}
// IMEI: \d{15}
// ICD9 CODE: \d{3}\.\d
// URL: https://www\.[a-z]{12}\.(com|net|io)

//example call
var genericAliasObj = await staticVault.Generic.Create('192.0.2.1', '[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}', new List<string>() {'my-generic-tag1', 'my-generic-tag2'});
```

### Last name
Generates a random last name with optional input of gender. 

Example call:
```c#
//example call
var lastNameAliasObj = await staticVault.LastName.Create('smith', new List<string>() {'my-lName-tag1', 'my-lName-tag2'});
```
### Passport number
Generates a random nine digit number. Currently only generates formats matching US passports.

Example call:
```c#
//example call
var passportAliasObj = await staticVault.Passport.Create('123456789', new List<string>() {'my-passport-tag1', 'my-passport-tag2'});
```
### Place of birth
Generates a random place of birth. An optional parameter of state may be provided to choose the state associated with the place of birth.

Place of birth example:
```c#
// example format/output
// city, state
// Odachester, Washington

//example call with optional state param
var pobAliasObj = await staticVault.PlaceOfBirth.Create('Atlanta, Georgia', 'GA', new List<string>() {'my-pob-tag1', 'my-pob-tag2'});
```

Providing an incorrect state abbreviation will return a random state. The list of acceptable inputs is below.
```text
'AK', 'AL', 'AR', 'AZ', 'CA', 'CO', 'CT', 'DC', 'DE', 'FL', 'GA', 'HI', 'IA', 'ID', 'IL', 'IN', 'KS', 'KY',
'LA', 'MA', 'MD', 'ME', 'MI', 'MN', 'MO', 'MS', 'MT', 'NC', 'ND', 'NE', 'NH', 'NJ', 'NM', 'NV', 'NY', 'OH',
'OK', 'OR', 'PA', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VA', 'VT', 'WA', 'WI', 'WV', 'WY'
```
### Race
Generates a random race from a list. 

Race example:
```c#
var raceAliasObj = await staticVault.Race.Create('Native Hawaiian or Other Pacific Islander', new List<string>() {'my-race-tag1', 'my-race-tag2'});
```

Output options are: 
```text
"American Indian or Alaska Native",
"Asian",
"Black or African American",
"Native Hawaiian or Other Pacific Islander",
"White",
"Other",
"Hispanic or Latino"
``` 
### Random
Generates a random string value consisting of upper and lower case letters that will be 20, 35, 50, or 80 characters long. Similar to the generic generator, but does not need a template.

Random example:
```c#
var randomAliasObj = await staticVault.Race.Create('random value', new List<string>() {'my-race-tag1', 'my-race-tag2'});
```
### Social security number
Generates a random social security number. An optional parameter of state may be provided to choose the state used to generate the ssn.

Output format:
```C#
// output format
// ###-##-####

//example call
var ssnAliasObj = await staticVault.Ssn.Create('123-45-6789', new List<string>() {'my-ssn-tag1', 'my-ssn-tag2'});
```
### Tax payer ID
Generates a random tay payer ID. Currently only produces ITIN(Individual Taxpayer Identification Number) values.

Output format: 
```c#
// 9#-##-####

//example call
var taxPayerIDAliasObj = await staticVault.TaxPayer.Create('92-45-6789', new List<string>() {'my-taxPayerID-tag1', 'my-taxPayerID-tag2'});
```
### Vehicle registration
Generates a random vehicle registration. Vehicle registration is 3 Capitalized letters followed by 4 digits.

Example Output: 
```c#
// example output
// ABC·1234

//example call
var vehicleRegistrationAliasObj = await staticVault.VehicleRegistration.Create('KJB-6797', new List<string>() {'my-vehicleRegistration-tag1', 'my-vehicleRegistration-tag2'});
```

Communication Vaults
------------
Communicataion vaults will store aliases for data types that will need to maintain their transactional integrity. Creating a communication vault is a similar process to a static vault, but the data aliased inside will be different. 

The alias generated for communication emails will be a functioning email. Nullafi will handle receiving messages to this address and relaying them to the real email address. White list senders and domains are added to control who may contact these users. Control for these emails may be found in the <a href="https://dashboard.nullafi.com/login" target="_blank">Nullafi Dashboard</a> under the **'System'** tab.

```c#
public async Task CreateCommunicationVault() {
	var client = await SDK.CreateClient();
	CommunicationVault communicationVault = await client.CreateCommunicationVault('my-communication-vault', new List<string>() {'my-tag-1', 'my-tag-2'});
	Console.WriteLine(communicationVault);
	/*
		output example:
		{ 
			"Id":"e490157b23534215b0369a2685aab47g", 
			"name":"my-communication-vault",
			"masterKey":"MASTER_KEY",
			"tags":['my-tag-1', 'my-tag-2'], 
			"createdAt":"2018-07-13 T01:00:00Z" 
		}
	*/
	return communicationVault;
}
```
The **ID** as well as the **Master Key** from the output will be used to retrieve the vault. These values must be stored in your database to retrieve the vault.
Retrieving a vault looks like this: 

```c#
public async Task RetrieveCommunicationVault() {
	//Authenticated client
	var client = await SDK.CreateClient();
	// ID and Master key should be stored and retrieved from database
	static readonly communicationVaultID = 'e490157b23534215b0369a2685aab47g';
	static readonly communicationVaultMasterKey = 'MASTER_KEY';
	// ID and Master key should be stored and retrieved from database
	CommunicationVault communicationVault = await client.retrieveCommunicationVault(communicationVaultID, communicationVaultMasterKey);
	return communicationVault;
}
```

You can also delete a vault using the vault ID. Deleting the vault will also remove all aliases stored within, so make sure data is properly saved before deleting a vault. Deleting a vault will return a response with a key of 'ok' and a boolean value. 

```c#
public async Task DeleteCommunicationVault() {
	//Authenticated client
	var client = await SDK.CreateClient();
	// ID should be stored and retrieved from database
	static readonly communicationVaultID = 'e490157b23534215b0369a2685aab47g';
	var communicationVaultResponse = await client.DeleteCommunicationVault(communicationVaultID);
}
```

Communication Data Types
------------
### Email
Generating email aliases will provide a new functional email to use in place of the real email. These alias addresses will work as relays to the real address, while also providing the ability to white list approved sender domains and addresses. 

Email example:
```c#
// input
// realEmail@gmail.com
// output
// cizljfhxrazvcy@fipale.com

// example call
var emailAlias = await communicationVault.Email.Create('realEmail@gmail.com', new List<string>() {'my-tag-1', 'my-tag-2'});
```

Copyright and License
---------------------

Copyright 2019 Joinesty, Inc. All rights reserved.
