Feature: DynamicServerSpecs
	In order to be able to mock web requests
	As a developer writting service tests that mock out a web server
	I want to be able to set the reply to a web request

@Acceptance.Dynamic
Scenario: Set dynamic resource reply
	Given I post to resource /MyResource/ResourceId with header Set-Reply:true
	When I attempt a get on resource /MyResource/ResourceId
	Then I should get back exactly what I set up

@Acceptance.Dynamic
Scenario: Clear a specific dynamic resource reply
	Given I post to resource /MyResource/ResourceId with header Set-Reply:true
	When I post to resource /MyResource/ResourceId with header Clear-Reply:this
	Then a get on /MyResource/ResourceId should return status 404

@Acceptance.Dynamic
Scenario: Clear a specific dynamic resource reply should only clear the specific path
	Given I post to resource /MyResource/ResourceId with header Set-Reply:true
	And I post to resource /MyResource/ResourceId2 with header Set-Reply:true
	When I post to resource /MyResource/ResourceId with header Clear-Reply:this
	Then a get on /MyResource/ResourceId2 should get back exactly what I set up

@Acceptance.Dynamic
Scenario: Clear all dynamic resource replies
	Given I post to resource /MyResource/ResourceId with header Set-Reply:true
	And I post to resource /MyResource/ResourceId2 with header Set-Reply:true
	When I post to resource /MyResource/ResourceId with header Clear-Reply:all
	Then a get on /MyResource/ResourceId should return status 404
	And a get on /MyResource/ResourceId2 should return status 404

@Acceptance.Dynamic
Scenario Outline: Set method for dynamic resource
	Given I set resource /MyResource/ResourceId with header Set-Reply-Method:<VerbToWork>
	When I attempt a <VerbToWork> on resource /MyResource/ResourceId
	Then I should get back exactly what I set up

	Examples:
	| VerbToWork | Verb that should not work |
	| GET        | POST                      |
	| POST       | GET                       |

@Acceptance.Dynamic
Scenario Outline: Set content type for dynamic resource
	Given I set resource /MyResource/ResourceId with header Set-Content-Type:<ContentType>
	When I attempt a GET on resource /MyResource/ResourceId
	Then the the reply should have a content type of <ContentType>

	Examples:
	| ContentType         |
	| MyCustomContentType |
