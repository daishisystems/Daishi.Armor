Feature: ArmorTokenIsValidated
	In order to prove that an ArmorToken is validated
	As an ArmorTokenValidator
	I want to validate an ArmorToken

@armorTokenValidator
Scenario: Validate an ArmorToken
	Given I have supplied a valid ArmorToken
	And I have encrypted the valid ArmorToken
	And I have hashed the valid ArmorToken
	When I validate the valid ArmorToken
	Then I should return a valid result
