Feature: ArmorTokenIsSerialised
	In order to prove that a given ArmorToken is serialised
	As an ArmorTokenSerialisor
	I want to serialise an ArmorToken

@armorTokenSerialisor
Scenario: Serialise an ArmorToken
	Given I have supplied an ArmorToken	
	When I serialise the ArmorToken
	Then ArmorTokenSerialisor creates a JSON representation of the ArmorToken
