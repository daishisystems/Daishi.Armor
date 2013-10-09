Feature: SecureArmorTokenValidatorValidatesSecureArmorToken
	In order to prove that an SecureArmorTokenValidator validates a secure ArmorToken
	As a SecureArmorTokenValidator
	I want to validate a secure ArmorToken

@SecureArmorTokenValidator
Scenario: Validate a secure ArmorToken
	Given I have provided a secure ArmorToken	
	When I validate the secure ArmorToken
	Then The validated ArmorToken should be valid.
