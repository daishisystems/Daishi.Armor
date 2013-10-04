Feature: ArmorTokenIsGenerated
	In order to prove that an ArmorToken is generated
	As an ArmorTokenGenerator
	I want to generate an ArmorToken

@ArmorTokenGenerator
Scenario: Generate an ArmorToken
	Given I have supplied a raw ArmorToken for generation	
	When I have generated the ArmorToken
	Then I should be able to successfully validate the generated ArmorToken