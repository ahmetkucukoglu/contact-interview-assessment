# Contact app with distributed microservice

<em>This repo was created for interview assessment.</em>

## Information

There are 3 internal services: Company, Person and Report. External access to these services was disabled. Only the gateway can access them. There are also 2 applications, Outbox Processor and Consumers.

E2E test was written for Company, Person and Report services. XUnit tool was used for testing.

The message queue system was used for the asynchronous execution of the reporting process. Masstransit was used and RabbitMQ was preferred as transport.

Refit library was used for http call. Polly was used for the retry mechanism.

MongoDb was preferred for database.

<table>
  <tr>
    <th colspan="2">Unit Test Coverage</th>
  </tr>
  <tr>
    <th>Services</th>
    <th>Coverage (%)</th>
  </tr>
  <tr>
    <td>Company</td>
    <td align="right">99%</td>
  </tr>
  <tr>
    <td>Person</td>
    <td align="right">96%</td>
  </tr>
  <tr>
    <td>Report</td>
    <td align="right">99%</td>
  </tr>
</table>

## Installation

You can run the app via Docker.

    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d 
    
You can access Gateway's swagger address the following link.

http://localhost:6010/swagger