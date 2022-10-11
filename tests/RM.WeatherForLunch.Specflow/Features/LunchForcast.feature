Feature: LunchForcast
Getting the forcast in order to see if the weather is good for lunch

@tag1
Scenario: Get current forcast
	Given I request for the current forcast	
	Then I should get a result
