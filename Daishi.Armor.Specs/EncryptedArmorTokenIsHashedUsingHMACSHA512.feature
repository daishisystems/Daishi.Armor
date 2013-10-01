Feature: EncryptedArmorTokenIsHashedUsingHMACSHA512
	In order to prove that an encrypted ArmorToken is hashed using HMACSHA512
	As an ArmorTokenHasher
	I want to hash an encryptedArmotToken using HMACSHA512

@armorTokenHasher
Scenario: Hash an encrypted ArmorToken using HMACSHA512
	Given I have supplied an encrypted ArmorToken for hash using HMACSHA512
	And I have hashed the encrypted ArmorToken using HMACSHA512
	When I verify the hashed ArmorToken's HMACSHA512 signature
	Then The HMACSHA512 hash signature will be valid
