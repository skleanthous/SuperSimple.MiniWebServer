Feature: ControllerFunctionSpecs
	In order to be able to adapt the responses
	As a tester
	I want to respond to requests dynamically by inspecting the request

@Acceptance.ControllerFunction
Scenario: Controller function gets hit if path and method matches
	Given a controller function on GET - /MyResource/ResourceId that returns
	| Name   | Value |
	| MyName | 5     |
	When I attempt a get on resource /MyResource/ResourceId
	Then the controller function should be called

@Acceptance.ControllerFunction
Scenario: Controller function returns susccessfully
	Given a controller function on GET - /MyResource/ResourceId that returns
	| Name   | Value |
	| MyName | 5     |
	When I attempt a get on resource /MyResource/ResourceId
	Then the reply should return
	| Name   | Value |
	| MyName | 5     |
	Then the reply should have a status code of 200

@Acceptance.ControllerFunction
Scenario: Controller function returns custom status code with content
	Given a controller function on GET - /MyResource/ResourceId that returns status code 201 and
	| Name   | Value |
	| MyName | 5     |
	When I attempt a get on resource /MyResource/ResourceId
	Then the reply should have a status code of 201
	And the reply should return
	| Name   | Value |
	| MyName | 5     |

@Acceptance.ControllerFunction
Scenario: Controller function returns HttpContent
	Given a controller function on GET - /MyResource/ResourceId that returns as HttpContent
	| Name   | Value |
	| MyName | 5     |
	When I attempt a get on resource /MyResource/ResourceId
	Then the reply should return
	| Name   | Value |
	| MyName | 5     |

# Add more tests to test request data is filled properly in controller function
