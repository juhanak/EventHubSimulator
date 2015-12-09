# Introduction

This is an Universal Windows Platform (UWP) app that can be used to send events to Azure Event Hub. AMQP.Net Lite library is used to transfer events.

# Running

First copy EventHubSettings-sample.txt to EventHubSettings.txt and fill in the correct settings. 
Open the solution file (`IoTCoreApp.sln`) in Visual Studio 2015 and hit run. Currently the application 
sends a json object to the server when slider value is changed.


