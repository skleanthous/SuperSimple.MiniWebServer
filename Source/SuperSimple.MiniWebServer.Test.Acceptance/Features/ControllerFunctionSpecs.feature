Feature: ControllerFunctionSpecs
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

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
