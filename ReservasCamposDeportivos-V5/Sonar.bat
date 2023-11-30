dotnet sonarscanner begin /k:"CBO" /d:sonar.login="squ_599c11cf0edfcd1ab3111cced5caab1e1b9bbf79" /d:sonar.host.url="https://sonarqube.bit2bitamericas.com"
dotnet build 
dotnet sonarscanner end /d:sonar.login="squ_599c11cf0edfcd1ab3111cced5caab1e1b9bbf79"