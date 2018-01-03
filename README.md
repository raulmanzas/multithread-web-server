## Multithreaded web server


### Sobre o desenvolvimento
Este projeto foi implementado usando C# .NET Core inteiramente em Linux, apesar de ser multiplataforma.

### Como usar
[Veja aqui como instalar o .NET Core na sua distribuição Linux.](https://www.microsoft.com/net/learn/get-started/linuxredhat)

Acesse o diretório */server* do projeto e, pelo terminal, execute:

`$ dotnet run ../config.json`

Verifique que está funcionando acessando "http://localhost:5000/" ou qualquer que seja a URL base que você definiu no arquivo *config.json*.

Para customizar o funcionamento do servidor, basta alterar o arquivo de configurações *config.json*.

* Para aumentar ou diminuir a quantidade de threads disponíveis no threadpool do servidor, basta alterar a propriedade *numberOfThreads*;
* Para definir quais tipos de arquivos o servidor pode acessar e retornar, deve-se alterar a propriedade *validExtensions*;
* A URL de acesso ao servidor é definida na propriedade *baseUrl*;
* O diretório base para o servidor, onde todos os arquivos que serão acessados devem ser mantidos, é definido na propriedade *staticFilesDirectory*.

___
*Primeiro trabalho prático da disciplina Programação Paralela e Distribuída no DCC/UFLA.*
