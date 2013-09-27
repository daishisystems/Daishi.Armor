Feature: ArmorTokenIsEncryptedUsingRijndael
	In order to prove that a given ArmorToken is encrypted using Rijndael encryption
	As an ArmorTokenEncryptor
	I want to encrypt an ArmorToken using Rijndael encryption

@armorTokenEncryptor
Scenario: Encrypt an ArmorToken using Rijndael encryption
	Given I have supplied an ArmorToken for encryption
	And I have serialised the ArmorToken
	And I have encrypted the token using Rijndael encryption
	When I decrypt the token using Rijndael encryption
	And I parse the decrypted token
	Then the parsed token should equal the original