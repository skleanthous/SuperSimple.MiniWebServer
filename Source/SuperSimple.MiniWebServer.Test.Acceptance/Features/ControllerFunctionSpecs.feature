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

# Add more tests to test request data is filled properly in controller function
