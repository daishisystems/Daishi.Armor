Feature: ArmorTokenIsDeserialised
	In order to prove that an ArmorToken is deserialised
	As an ArmorTokenDeserialiser
	I want to deserialise an ArmoreToken

@armorTokenDeserialiser
Scenario: Deserialise an ArmorToken
	Given I have supplied a serialised ArmorToken	
	When I deserialise the ArmorToken
	Then the result should match the original ArmorToken
