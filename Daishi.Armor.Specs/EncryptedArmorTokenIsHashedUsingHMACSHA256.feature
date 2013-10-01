Feature: EncryptedArmorTokenIsHashedUsingHMACSHA256
	In order to prove that an encrypted ArmorToken is hashed using HMACSHA256
	As an ArmorTokenHasher
	I want to hash an encryptedArmotToken using HMACSHA256

@armorTokenHasher
Scenario: Hash an encrypted ArmorToken using HMACSHA256
	Given I have supplied an encrypted ArmorToken for hash using HMACSHA256
	And I have hashed the encrypted ArmorToken using HMACSHA256
	When I verify the hashed ArmorToken's HMACSHA256 signature
	Then The HMACSHA256 hash signature will be valid
