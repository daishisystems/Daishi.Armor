Feature: ArmorTokenConstructorConstructsStandardSecureArmorToken
	In order to prove that an ArmorTokenConstructor constructs a standard, secure ArmorToken
	As an ArmorTokenConstructor
	I want to construct a standard, secure ArmorToken

@ArmorTokenConstructor
Scenario: Construct a standard, secure ArmorToken
	Given I have supplied a StandardArmorTokenConstructor	
	When I invoke the StandardArmorTokenConstructor
	Then The result should yield a valid, standard, secure ArmorToken
