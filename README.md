# API de Gerenciamento de Motos

##Integrantes
Pedro Henrique Bergara RM556639
Henrique Izzi de São José 555413

## Descrição do Projeto

A ideia do nosso projeto é ter um controle total sobre o galpão sabendo onde esta cada moto e como elas estão por isso fizemos essa API para ter um controle sobre nossas motos que usaremos em outras partes do projeto para mapear e localizar dentro do galpao com precisão.

**Observação:** Esta versão utiliza um banco de dados em memória para persistência de dados, o que significa que os dados serão perdidos quando a aplicação for reiniciada.

## Tecnologias Utilizadas

*   .NET 8 (ou a versão do seu SDK)
*   ASP.NET Core Minimal API
*   Entity Framework Core (com provedor In-Memory)
*   C#
*   OpenAPI (via Swashbuckle/Scalar) para documentação

## Rotas da API (Endpoints)

A base da URL para todos os endpoints é `/motos`.

| Verbo HTTP | Rota             | Descrição                                    | Corpo da Requisição (JSON) | Respostas Possíveis                                    |
| :--------- | :--------------- | :------------------------------------------- | :------------------------- | :----------------------------------------------------- |
| `GET`      | `/`              | Lista todas as motos cadastradas.            | N/A                        | `200 OK` (com a lista de motos)                        |
| `GET`      | `/{id}`          | Busca uma moto específica pelo seu ID.       | N/A                        | `200 OK` (com a moto), `404 Not Found`                 |
| `POST`     | `/`              | Cria uma nova moto.                          | Objeto `Moto`              | `201 Created` (com a moto criada), `400 Bad Request` |
| `PUT`      | `/{id}`          | Atualiza uma moto existente pelo seu ID.     | Objeto `Moto`              | `204 No Content`, `404 Not Found`, `400 Bad Request` |
| `DELETE`   | `/{id}`          | Deleta uma moto existente pelo seu ID.       | N/A                        | `204 No Content`, `404 Not Found`                      |

**Estrutura do Objeto `Moto` (JSON):**

```json
{
  "id": 0, // Ignorado na criação (POST), usado na resposta
  "modelo": "string (obrigatório, max 50)",
  "status": true, // ou false
  "placa": "string (obrigatório, max 7)"
}
```

## Instalação e Execução

1.  **Pré-requisitos:**
    *   SDK do .NET instalado (versão 8 ou compatível).
2.  **Clonar o Repositório (se aplicável):**
    ```bash
    git clone <url-do-seu-repositorio>
    cd <pasta-do-projeto>
    ```
3.  **Restaurar Dependências:**
    ```bash
    dotnet restore
    ```
4.  **Executar a Aplicação:**
    ```bash
    dotnet run
    ```
    A API estará rodando geralmente em `https://localhost:xxxx` e `http://localhost:yyyy` (as portas serão exibidas no console).

## Como Usar

*   Após executar a aplicação, você pode acessar a documentação interativa da API (Scalar UI) no seu navegador, geralmente em `/scalar` (ex: `http://localhost:yyyy/scalar`).
*   Você pode usar ferramentas como `curl`, Postman, Insomnia ou a própria interface do Scalar para enviar requisições para os endpoints listados acima.
Proposta de Arquitetura e Estrutura do Projeto

Este documento detalha a arquitetura proposta e a estrutura do projeto para a API RESTful em C# .NET, focando nas boas práticas REST e nos requisitos de avaliação. Serão consideradas as três entidades principais: Moto, Motorista e Funcionário.

1. Entidades Principais

A API será construída em torno das seguintes três entidades principais:

1.1. Moto

Esta entidade já existe no projeto CSharp-DotNet-Mottu e representa uma motocicleta no sistema. Seus atributos são:

Atributo
Tipo
Descrição
Id
int
Identificador único da moto (chave primária).
Modelo
string
Modelo da moto (obrigatório, máximo 50 caracteres).
Status
bool
Indica o status da moto (ativo/disponível ou inativo/indisponível).
Placa
string
Placa da moto (obrigatório, máximo 7 caracteres).


1.2. Motorista

Esta nova entidade representará os motoristas que utilizam as motos. Seus atributos serão:

Atributo
Tipo
Descrição
Id
int
Identificador único do motorista (chave primária).
Nome
string
Nome completo do motorista (obrigatório).
DataNascimento
DateTime
Data de nascimento do motorista (obrigatório).
UltimaMotoUsadaId
int
Chave estrangeira para a última moto utilizada pelo motorista (opcional, pode ser nulo).
Endereco
string
Endereço do motorista (opcional).


1.3. Funcionário

Esta nova entidade representará os funcionários da empresa. Seus atributos serão:

| Atributo           | Tipo     | Descrição                                                                                             |
| :----------------- | :------- | :---------------------------------------------------------------------------------------------------- |\n| Id               | int    | Identificador único do funcionário (chave primária).                                                  |
| Nome             | string | Nome completo do funcionário (obrigatório).                                                           |
| DataNascimento   | DateTime| Data de nascimento do funcionário (obrigatório).                                                      |
| Funcao           | string | Função ou cargo do funcionário na empresa (obrigatório).                                              |
| Endereco         | string | Endereço do funcionário (opcional).                                                                   |

2. Estrutura do Projeto

Para manter a organização e seguir as boas práticas, o projeto CSharp-DotNet-Mottu será refatorado para uma estrutura mais modular, inspirada em projetos .NET Core comuns e no exemplo dotenet-fiap-aulas. A estrutura proposta incluirá:

Plain Text


CSharp-DotNet-Mottu/
├── src/
│   ├── Mottu.API/                  # Projeto principal da API (Minimal API ou Web API)
│   │   ├── Controllers/            # Endpoints para cada entidade (se usar Web API)
│   │   ├── Endpoints/              # Definição dos endpoints (se usar Minimal API)
│   │   ├── Program.cs              # Configuração da aplicação e injeção de dependências
│   │   ├── appsettings.json        # Configurações da aplicação
│   │   └── Properties/             # Configurações de lançamento
│   ├── Mottu.Domain/               # Contratos e interfaces (Entidades, Interfaces de Repositório, DTOs)
│   │   ├── Entities/               # Definição das entidades (Moto, Motorista, Funcionario)
│   │   ├── Interfaces/             # Interfaces para repositórios e serviços
│   │   └── DTOs/                   # Data Transfer Objects para entrada e saída de dados
│   ├── Mottu.Application/          # Lógica de negócio e serviços de aplicação
│   │   ├── Services/               # Implementações dos serviços de negócio
│   │   └── Mappers/                # Mapeamento entre entidades e DTOs
│   └── Mottu.Infrastructure/       # Implementação de persistência de dados e infraestrutura
│       ├── Data/                   # Contexto do banco de dados (DbContext) e configurações
│       │   ├── ApplicationDbContext.cs
│       │   └── Migrations/
│       ├── Repositories/           # Implementações dos repositórios (EF Core)
│       └── Extensions/             # Extensões para configuração de serviços
├── tests/
│   ├── Mottu.API.Tests/
│   └── Mottu.Application.Tests/
├── .dockerignore
├── .gitignore
├── CSharp-DotNet-Mottu.sln         # Solution file
└── README.md


3. Considerações Arquiteturais

•
Camadas (Layers): A arquitetura será dividida em camadas claras (Domain, Application, Infrastructure, API) para garantir a separação de responsabilidades e facilitar a manutenção.

•
Entity Framework Core: Será utilizado para acesso a dados, com um DbContext centralizado e migrações para gerenciar o esquema do banco de dados.

•
Padrão Repositório: Implementação de repositórios genéricos ou específicos para cada entidade para abstrair a lógica de acesso a dados.

•
Serviços de Aplicação: Camada responsável por orquestrar a lógica de negócio, utilizando os repositórios e DTOs.

•
Minimal API / Web API: O projeto Mottu.API será configurado para expor os endpoints RESTful. A escolha entre Minimal API e Web API será baseada na complexidade e preferência, mas ambas suportam os requisitos.

•
Boas Práticas REST: Implementação de paginação, HATEOAS (Hypermedia as the Engine of Application State) e códigos de status HTTP adequados para cada operação CRUD.

•
Swagger/OpenAPI: Configuração completa para documentação automática da API, incluindo descrições de endpoints, parâmetros, exemplos de payloads e modelos de dados.

Esta estrutura fornecerá uma base sólida para o desenvolvimento da API, promovendo a manutenibilidade, escalabilidade e aderência às boas práticas de desenvolvimento de software.


