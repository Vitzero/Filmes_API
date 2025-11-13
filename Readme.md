# 🎬 Filmes API
API RESTful para gerenciamento de filmes desenvolvida com ASP.NET Core, implementando operações CRUD completas e boas práticas de desenvolvimento.


## 📋 Sobre o Projeto
Esta API foi desenvolvida para gerenciar um catálogo de filmes, permitindo criar, listar, atualizar e deletar informações sobre filmes. O projeto foi construído seguindo princípios SOLID e padrões de arquitetura limpa.


## 🚀 Tecnologias Utilizadas

C# 8 - Linguagem de programação
ASP.NET Core - Framework web
Entity Framework Core - ORM para acesso a dados
SQL Server - Banco de dados relacional
Data Annotations - Validação de modelos
HTTPS - Protocolo seguro de comunicação
Git - Controle de versão

## 🏗️ Arquitetura e Padrões

API RESTful - Arquitetura de serviços web
SOLID - Princípios de design orientado a objetos
Manual Mapping - Mapeamento manual entre DTOs e entidades
Migrations - Versionamento do banco de dados


## 📦 Pré-requisitos
Antes de começar, você precisará ter instalado em sua máquina:

.NET Core SDK 3.1+
SQL Server
Git
Editor de código (recomendado: Visual Studio ou VS Code)

## 🔧 Instalação

1. Clone o repositório

git clone https://github.com/Vitzero/Filmes_API.git
cd Filmes_API

2. Configure a connection string
Edite o arquivo appsettings.json e configure a string de conexão com seu SQL Server:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FilmesDB;Trusted_Connection=True;"
  }
}

3. Execute as migrations

dotnet ef database update

4. Execute a aplicação

dotnet run


## 🛠️ Funcionalidades

✅ CRUD completo de filmes

✅ Validação de dados com Data Annotations

✅ Persistência de dados com Entity Framework Core

✅ API RESTful seguindo boas práticas

✅ Comunicação segura via HTTPS

✅ Migrations para versionamento do banco

✅ Mapeamento manual entre camadas

✅ Implementação de princípios SOLID

## 📝 Validações
O projeto utiliza Data Annotations para validação:
Campos obrigatórios
Tamanho máximo/mínimo de strings
Validação de formatos
Validação de ranges numéricos


- GitHub: @Vitzero
- Projeto: Filmes_API
