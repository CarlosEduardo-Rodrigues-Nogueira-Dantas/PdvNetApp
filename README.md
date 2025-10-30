# 🧠 PDVNetApp - Sistema de Gestão de Produtos

Este projeto foi desenvolvido com **C#**, **WPF**, **Entity Framework Core**, **SQL Server** e o padrão **MVVM**.  
O objetivo é demonstrar um sistema completo de **gestão de estoque** com **CRUD de produtos**, **validações**, e **dashboard de relatórios**.

---

## 🧱 Estrutura da Solução

A aplicação segue uma arquitetura limpa (Clean Architecture), dividida em camadas com responsabilidades bem definidas:


<img width="742" height="377" alt="image" src="https://github.com/user-attachments/assets/aa29d085-f60f-45b2-8822-013bac62c632" />


---

## ⚙️ Tecnologias e Ferramentas Utilizadas

| Componente | Tecnologia |
|-------------|-------------|
| **Frontend (UI)** | WPF com MVVM |
| **Backend (Core)** | .NET 8 / C# |
| **Banco de Dados** | Microsoft SQL Server |
| **ORM** | Entity Framework Core |
| **Injeção de Dependência (IoC)** | Microsoft.Extensions.DependencyInjection |
| **Configurações** | appsettings.json |
| **IDE** | Visual Studio / Visual Studio Code |

---

<img width="928" height="377" alt="image" src="https://github.com/user-attachments/assets/684d4478-3149-42e5-810b-4edd6aa1f1e2" />



## 🚀 Como Configurar e Executar o Projeto

### 🔹 1️⃣ Clonar o repositório

```bash
cd PdvNetApp

Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.Extensions.Configuration
Install-Package Microsoft.Extensions.Configuration.Json
Install-Package Microsoft.Extensions.DependencyInjection

# Cria a primeira migration
Add-Migration InitialCreate

# Aplica as alterações no banco
Update-Database

✅Add-Migration → gera o script (plano de mudança).
✅Update-Database → executa o plano (cria ou atualiza o banco).



🧩 Funcionalidades
🟢 CRUD de Produtos

Permite:

Criar novos produtos

Editar produtos existentes

Excluir produtos

Listar todos os produtos do banco

📋 Validações de Negócio

As validações estão implementadas no ProdutoFormViewModel, antes de salvar:


Campo	Regra
Nome	Obrigatório
Preço	Obrigatório e maior que 0
Quantidade	Obrigatória e maior que 0
Descrição	Opcional

Exemplo:
if (string.IsNullOrWhiteSpace(Produto.Nome))
{
    MessageBox.Show("O nome do produto é obrigatório.");
    return;
}

if (Produto.Preco <= 0)
{
    MessageBox.Show("O preço deve ser maior que zero.");
    return;
}

if (Produto.Quantidade <= 0)
{
    MessageBox.Show("A quantidade deve ser maior que zero.");
    return;
}

📊 Dashboard de Relatórios

A tela de Dashboard (DashboardWindow) exibe informações analíticas sobre o estoque:

Relatório	Descrição
Total de produtos	Quantidade total de produtos cadastrados
Valor total do estoque	Soma de Preço * Quantidade
Baixo estoque	Produtos com quantidade inferior a 5 unidades

O cálculo é feito automaticamente no DashboardViewModel:

TotalProdutos = produtos.Count();
ValorTotalEstoque = produtos.Sum(p => p.Preco * p.Quantidade);
ProdutosBaixoEstoque = produtos.Where(p => p.Quantidade < 5).ToList();

📦 Camadas e Responsabilidades
Camada	Responsabilidade
Domain	Entidades e contratos (IProdutoRepository)
Infra	Implementações concretas e acesso ao banco (ProdutoRepository)
Application	Serviços intermediários e lógica de negócio (ProdutoService)
UI (WPF)	Interface gráfica e interação com o usuário (MVVM)

🧮 Códigos-chave do Projeto
RelayCommand.cs

Implementa o padrão ICommand, permitindo vincular ações de UI ao ViewModel.

ProdutoRepository.cs

CRUD completo utilizando EF Core (AddAsync, UpdateAsync, DeleteAsync, ToListAsync).

ProdutoService.cs

Camada intermediária entre UI e Repositório, responsável por expor métodos assíncronos para o ViewModel.

MainViewModel.cs

Gerencia os produtos exibidos na tela principal, comandos e abertura do Dashboard.

DashboardViewModel.cs

Realiza cálculos estatísticos e apresenta relatórios de estoque.

🧠 Principais Regras de Negócio

✅ Nenhum produto pode ser cadastrado sem Nome, Preço e Quantidade.
✅ Preço e Quantidade não podem ser negativos ou zero.
✅ Dashboard alerta automaticamente produtos com estoque menor que 5 unidades.
✅ Cálculo do valor total do estoque feito em tempo real.
✅ Dados sincronizados automaticamente após CRUD.

🧠 Funcionalidades

✅ CRUD completo de produtos
✅ Validações de entrada de dados
✅ Cálculo automático do valor total do estoque
✅ Alerta de baixo estoque
✅ Dashboard com relatórios dinâmicos
✅ Arquitetura limpa e de fácil manutenção

👨‍💻 Autor

Carlos Eduardo Rodrigues Nogueira Dantas
💼 Desenvolvedor .NET / WPF / SQL Server
📧 Projeto desenvolvido para processo seletivo - Desenvolvedor Júnior
