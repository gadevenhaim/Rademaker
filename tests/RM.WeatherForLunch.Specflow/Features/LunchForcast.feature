Feature: LunchForcast
Getting the forcast in order to see if the weather is good for lunch

@tag1
Scenario: Can sit outside
	Given the weather is:
		| temp_C | cloudcover | humidity | winddirDegree | windspeedKmph | precipMM |
		| 19.0   | 10         | 10       | 40            | 4             | 0.0      |
	When I check the current weather forcast
	Then sit outside is true

Scenario: Cannot sit outside because of the wind speed
	Given the weather is:
		| temp_C | cloudcover | humidity | winddirDegree | windspeedKmph | precipMM |
		| 19.0   | 10         | 10       | 40            | 12            | 0.0      |
	When I check the current weather forcast
	Then sit outside is false

Scenario: Cannot sit outside because of it is too cold
	Given the weather is:
		| temp_C | cloudcover | humidity | winddirDegree | windspeedKmph | precipMM |
		| 17.0   | 10         | 10       | 40            | 4             | 0.0      |
	When I check the current weather forcast
	Then sit outside is false

Scenario: Cannot sit outside because there are too many clouds
	Given the weather is:
		| temp_C | cloudcover | humidity | winddirDegree | windspeedKmph | precipMM |
		| 19.0   | 60         | 10       | 40            | 4             | 0.0      |
	When I check the current weather forcast
	Then sit outside is false

Scenario: Cannot sit outside because it is rainy
	Given the weather is:
		| temp_C | cloudcover | humidity | winddirDegree | windspeedKmph | precipMM |
		| 19.0   | 10         | 10       | 40            | 4             | 0.5      |
	When I check the current weather forcast
	Then sit outside is false