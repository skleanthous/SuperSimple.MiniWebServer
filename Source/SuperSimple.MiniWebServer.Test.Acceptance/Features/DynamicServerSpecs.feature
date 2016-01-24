Feature: DynamicServerSpecs
	In order to be able to mock web requests
	As a developer writting service tests that mock out a web server
	I want to be able to set the reply to a web request

@Acceptance.Dynamic
Scenario: Set dynamic resource reply
	Given I post to resource /MyResource/ResourceId with header Set-Reply:true
	When I attempt a get on resource /MyResource/ResourceId
	Then I should get back exactly what I set up
