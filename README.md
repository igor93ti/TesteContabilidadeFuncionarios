# ContabilidadeFuncionarios

Para subir a aplicação basta navegar até a pasta raiz e executar o comando "docker-compose up --build"


Considerações gerais:
Devido ao pouco tempo que pude me dedicar a construção da aplicação, não consegui fazer tudo o que eu queria. Normalmente tenho mais tempo livre nos finais de semana e nesse último eu tinha alguns compromissos agendados.

#Quanto ao desenvolvimento e funcionamento da aplicação:
Inicialmente comecei fazendo a criação do funcionário juntamente com um fluxo de autenticação para a obtenção do JWT que seria utilizado para garantir que quem estava operando tinha permissão. Como vi que possivelmente eu não teria tempo para fazer tudo o que eu estava imaginando, decidi fazer primeiro o fluxo básico que estava sendo solicitado e depois ir ajustando e melhorando.

Na parte do salário eu havia ficado em dúvida se era pra mostrar no contracheque apenas os valores do funcionário de acordo com cada mês posterior a data de admissão (todos os registros seriam iguais). Então modifiquei um pouco para que ficasse possível adicionar manualmente a remuneração, portanto, o operador pode adicionar as remunerações do mês de cada funcionário. Com isso, o contracheque é gerado levando em consideração todas as receitas do ano/mes do funcionário.

Utilizei de forma simples CQRS com mediatr para separar os comandos e as queries. Para uma aplicação tão simples não vi necessidade de criar 2 bases de dados (leitura e escrita).

Com um pouco mais de tempo para trabalhar na aplicação seria possível efetuar algumas melhorias:
<ul>
  <li><b>Incremento na cobertura dos testes</b></li>
  <li><b>Validações de modelo ao persistir informações</b></li>
  <li><b>Tratamento adequado de exceções</b></li>
  <li><b>Adição de circuit breaker</b></li>
  <li><b>Adição de logs</b></li>
  <li><b>Ajustes finos na comunicação entre as camadas</b></li>
</ul>
