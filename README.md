# SuperSimple.MockWebServer

h2. How to use

_Comming soon. For now just have a look in the acceptance test project and in the console host project._

h2. Purpose of this project.

The purpose of this project is to allow for the easy setup of mock web servers for use in acceptance tests. Obviously this means that this has to be a fully funcitoning web server, but the focus of development will be to provide features for use in testing scenarios.

h2. General info.

This project uses SemVer.

It will be provided as a Nuget as soon as I setup AppVeyor to do so. Nuget packages will be created for all versions that pass the acceptance tests.

A roadmap will be shared to show the features that will get into version 1.

h2. Reason for development

I dislike the huge number of projects that exist just because devs did not like what was there before. There are certainly cases where what existed before was less than ideal, so the undertaking of a similar project does have some merit, but more ofter than not, I see situations where projects get started just because the developers did not put the effort into learning the tools available for them. 

The reasons for starting development on this project is to provide an easy, customizable and embeddable web server for acceptance tests while at the same time having the least nuber of dependencies. Running web api inside acceptance test is feasible, but too heavyweight to be fit for purpose. One could self-host an OWIN server into the acceptance test project (which is the approach I am using for some time), but that is missing some boilerplate code to make it easy out-of-the-box.

h2. Priorites

Due to the above reasons, the priorities for this project are:

  #. Easy to use - The web server should provide functionality out of the box to allow for really easy mocking of third party services.
  #. Embeddable - The web server should be able to run in-proc with the acceptance tests so that they can be able to interact if need be.
  #. Be able to be used as a normal web serer - It has to provide the same functionality as a full-fledged web server to be able to mock any service possible,
  so the target of being able to function as a normal web server is really important. Due to this it should be also able to host a service normally with as much functionality as possible.
  #. Performant - It has to be able to serve as much requests as possible so as to have the smaller possible footprint and impact on the actual operation being tested. Additionally if this server is used for hosting a service then it has to be as performant as possible.