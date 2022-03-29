# LanguageSchoolApi
Api criada para o processo seletivo da Marlin

## Aplicação

Esse projeto consistia em fazer uma api para uma escola de idiomas. Ela tem 3 funcionalidades principais:

* Crud de Alunos (Cadastro,Listagem,Alteração e Deleção)
* Crud de Turmas (Cadastro,Listagem,Alteração e Deleção)
* Crud de Matriculas(Cadastro,Listagem, e Deleção)

Cada crud com suas validações específicas. O Projeto foi feito usando o conceito de Code first e migrations.

A aplicação já tem o Swagger instalado para que possa ser testada e utilizada diretamente no Navegador.


## Tecnologias Utilizadas

* Linguagem C#
* Asp.net core
* Banco de dados SqlServer
* Entity Framework Core
* Swagger

## Observações

* Dentro do projeto tem um arquivo *scriptDatabase.sql* que é o arquivo com os scripts para criação do banco de dados.
* No arquivo *appsettings.json* que é o arquivo que contem algumas configurações da aplicação é necessário preencher a string de conexão 
para que a aplicação conecte-se com o sqlServer instalado no computador ao qual vai ser rodado a aplicação. São dados simples como o nome do servidor
o usário e a senha para conectar no Sqlserver.
