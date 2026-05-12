# 🔮 Grimório Digital — Parte do Tech Lead

## Estrutura de arquivos

```
GrimorioDigital/
├── Controllers/
│   ├── AuthController.cs        ← POST /api/auth/login
│   └── UsuariosController.cs    ← CRUD completo de usuários
├── Data/
│   └── AppDbContext.cs          ← DbContext + relacionamentos + seed
├── DTOs/
│   ├── AuthDtos.cs              ← LoginDto, TokenResponseDto
│   └── UsuarioDtos.cs           ← Create, Update, Response
├── Models/
│   ├── Usuario.cs
│   ├── EscolaDeMagia.cs
│   ├── Magia.cs
│   ├── Ingrediente.cs
│   ├── Pocao.cs
│   ├── PocaoIngrediente.cs      ← tabela pivot Many-to-Many
│   └── Feiticeiro.cs
├── Services/
│   └── TokenService.cs          ← geração do JWT
├── appsettings.json             ← strings de conexão + config JWT
├── GrimorioDigital.csproj       ← pacotes NuGet
└── Program.cs                   ← toda a configuração do pipeline
```

## Como rodar

### 1. Instalar pacotes
```bash
dotnet restore
```

### 2. Criar a migration inicial
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Rodar o projeto
```bash
dotnet run
```

### 4. Acessar o Swagger
Abra o navegador em: `http://localhost:5000`

---

## Como testar o fluxo completo

1. **Cadastrar um usuário** — `POST /api/usuarios` (sem autenticação)
2. **Fazer login** — `POST /api/auth/login` → copiar o token retornado
3. **Clicar em Authorize no Swagger** → colar `Bearer {token}`
4. **Testar rotas protegidas** normalmente

---

## Requisitos atendidos nesta parte

| Requisito | Status |
|-----------|--------|
| CRUD de Usuários | ✅ |
| Autenticação JWT (login) | ✅ |
| Swagger com Bearer | ✅ |
| CORS configurado | ✅ |
| Entity Framework Core | ✅ |
| ASP.NET Core MVC | ✅ |
| Validações (DataAnnotations) | ✅ |
| Relações entre tabelas (Models) | ✅ |
| Seed de dados iniciais | ✅ |

---

## Para os outros membros da equipe

- Clonem o repositório após o Tech Lead subir a base
- Rodem `dotnet ef database update` para criar o banco
- Criem seus próprios Controllers e DTOs seguindo o mesmo padrão
- O `AppDbContext` já tem todos os `DbSet<>` configurados — só usar
- Para rotas protegidas, adicionem `[Authorize]` no controller
