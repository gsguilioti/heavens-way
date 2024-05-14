# HeavensWay

Controle e comunicação de Eventos para igrejas.

---

## Sobre
Esta aplicação tem como objetivo ser a comunicação com o banco de dados de um aplicativo de controle de eventos para igrejas localizadas no Brasil.

Os eventos podem ser desde reuniões normais, à acampamentos, ações sociais e confraternizações. Possue uma data de início e uma data de fim para controle de eventos que demoram dias.

Os usuários são membros das igrejas, que podem atuar em papel de Administrador ou membro comum.

- Usuários comuns podem listar os eventos para encontrar um de seu gosto e ver os participantes e se inscrever em algum evento a seu desejo.
- Administradores também podem agir como usuários comuns porém tem total controle sobre o restante da aplicação, como exemplificado no arquivo doc/usecase-diagram.png.

Cada igreja possui um endereço, os endereços podem ser cadastrados manualmente para uso posterior ou no próprio cadastro da igreja, informando o Cep, para essa funcionalidade foi utilizado o consumo de uma api externa: https://opencep.com/.

As demais relações entre as entidades podem ser verificadas no arquivo doc/er-diagram.png.

---

## Instalação
É possível utilizar o projeto por meio do Docker.
> docker build -t "nomedaimagem" .

O banco de dados utilizado foi o SQL Server, fica à configuração usuário seja por docker ou instalado na máquina(necessário configurar a DefaultConnection string para acessar).

Para configurar o banco  acesse a pasta src/HeavensWayApi/ e utilize o comando **dotnet ef database update** em seu terminal.

---

## Recomendações
A aplicação conta com Autenticação e Autorização, para utilizar todas as funcionalidades, entre com um usuário e se atende aos Papéis(Role).


---

## Tecnologias

- Docker
- .Net 8
- SQL Server
- Azure
- Swagger (documentação)
- Autenticação e Autorização com JWT.
- Cache (Middleware nativo .Net 8)
- Testes Unitários (projeto em src/HeavensWayTest/)

---

## Implementações Futuras

- Maior amplitude dos casos de testes.
- Novos filtros para os endpoint em geral.
- Separação de Eventos por meio de papéis dos usuários.
- Eventos por distrito.
