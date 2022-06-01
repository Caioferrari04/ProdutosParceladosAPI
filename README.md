# ProdutosParceladosAPI

API criada para realização de uma versão em .NET, C# do teste técnico de Back-End da Via Varejo.

## Dependências
- .NET Core 6.0
- Swagger 6.2
- FluentValidation
- RestSharp
- Newtonsoft.Json

## Utilização

- Clone o projeto localmente em seu computador: `git clone https://github.com/Caioferrari04/ProdutosParceladosAPI.git`.

- Execute o comando `dotnet restore` para restaurar as dependências do projeto.

- Por fim, utilize `dotnet run` para inicializar a API no IIS, e a acesse na rota local `https://localhost:7005` por requisição GET.

### Corpo de requisição

```json
{
    "produto": {
        "codigo": 1,
        "nome": "Produto 1",
        "valor": 200.00
    },
    "condicaoPagamento": {
        "valorEntrada": 0.00,
        "qtdeParcelas": 10
    }
}
```