Feature: NonceGeneratorCreatesRandomNumber
	In order to prove to a degree that the same number is never generated twice
	As a NonceGenerator
	I want to generate 10,000,000 random, unique numbers

@nonceGenerator
Scenario: Generate 10000000 Random Numbers
	Given I have generated 10000000 random numbers	
	Then each number should be unique
