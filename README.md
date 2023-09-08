# finance.core

O intuito desse projeto é utilizar alguns conceitos/padrões/tecnologias que estou estudando.
Mesmo sendo um projeto simples e não necessitando de algumas implementações, procurei utilizar os assuntos estudados para reforçar o aprendizado.

Seguem alguns itens:

 - Observabilidade com Kibana/APM
 - Log com Kibana/ElasticSearch
 - Injeção de dependência/MediatR/CQRS
 - Documentação de API com Swagger
 - MongoDB
 - AutoMapper
 - SOLID
 - DDD
 - Unit tests com XUnit
 - SonarQube v10.2.0.77647
	- Requirements
		jdk-17.0.8.1+1
	- Tools
		dotnet tool install --global dotnet-sonarscanner
		dotnet tool install --global dotnet-coverage
	- Execution 
		dotnet sonarscanner begin /k:"finance.core" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_2f702accb62ab01500ea54c0fefef3508f5710f3" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
		dotnet build 
		dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
		dotnet sonarscanner end /d:sonar.token="sqp_2f702accb62ab01500ea54c0fefef3508f5710f3"