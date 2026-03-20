<h1>📋 ToDoFlow</h1>
<p>Um sistema completo de gerenciamento de tarefas com autenticação segura, desenvolvido com .NET 8 no backend e Angular 17 no frontend.</p>

<h2>📌 Sobre o Projeto</h2>
<p>O ToDoFlow é uma aplicação web que permite aos usuários criarem uma conta, realizarem login e gerenciarem suas tarefas, separando por categorias. O projeto foi desenvolvido com foco em boas práticas de arquitetura, segurança e padrões de desenvolvimento.</p>

<h2>🛠️ Funcionalidades</h2>
<ul>
  <li>Autenticação com JWT (Access Token + Refresh Token)</li>
  <li>Registro de usuários</li>
  <li>Hash de senhas</li>
  <li>CRUD de tarefas e categorias</li>
  <li>Relacionamento entre tarefas e categorias</li>
  <li>Acesso protegido por autenticação</li>
  <li>Recuperação de senha via e-mail (SMTP)</li>
  <li>Realização de testes unitários</li>
</ul>

<h2>🏗️ Arquitetura</h2>
<ul>
  <li>API: Controllers, autenticação e endpoints</li>
  <li>Application: Dtos, Serviços de domínio, integrações, SMTP, JWT</li>
  <li>Domain: Entidades e regras de negócio puras</li>
  <li>Infrastructure: Acesso a dados, EF Core e repositórios</li>
  <li>Tests: Testes unitários</li>
</ul>

<h2>🔐 Segurança</h2>
<ul>
  <li>Senhas armazenadas com hash seguro</li>
  <li>JWT com tempo de expiração definido</li>
  <li>Refresh Token persistido no banco de dados</li>
  <li>Endpoints protegidos por autenticação</li>
</ul>

<h2>💻 Tecnologias utilizadas</h2>
<h3>Backend</h3>
<ul>
  <li>C# 12</li>
  <li>.NET 8.0</li>
  <li>Entity Framework 9.0.0</li>
  <li>SQL Server</li>
</ul>

<h2>Frontend</h2>
<ul>
  <li>Angular 17.3.11</li>
  <li>HTML5 / CSS3</li>
  <li>Typescript 5.4.2</li>
</ul>

<h2>🚀 Como Executar o Projeto</h2>
<h3>📋 Pré-requisitos</h3> 
<p>Antes de começar, certifique-se de ter instalado em sua máquina:</p>
<ul>
  <li><a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">.NET 8 SDK</a> (para o backend)</li>
  <li><a href="https://nodejs.org/">Node.js</a> (que inclui o npm, para o frontend Angular)</li>
  <li><a href="https://angular.io/cli">Angular CLI</a> versão 17</li>
  <li><a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads">SQL Server</a> (ou SQL Server Express)</li>
</ul>

<h3>⚙️ Configuração e Execução</h3>

<h4>1. Clone o repositório</h4>
<pre><code>git clone https://github.com/ArthurSantos18/ToDoFlow.git</code>
cd ToDoFlow</code></pre>
<h4>2. Configurar e executar o Backend</h4>
<ol>
  <li>
    <p>Navegue até a pasta do backend:</p>
    <pre><code>cd ToDoFlow.Backend</code></pre>
  </li>
  <li>
    <p><strong>Configurar a string de conexão</strong>: Edite o arquivo <code>appsettings.json</code>) e atualize a propriedade <code>DefaultConnection</code> com os dados do seu SQL Server.</p>
    <pre><code>{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=ToDoFlowDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
    </code></pre>
  </li>
    <li>
      <p><strong>Frontend Url</strong>: No mesmo arquivo preencha a seção <code>FrontEnd</code> para a Url do Frontend</p>
    </li>
  <li>
    <p><em>⚠️ Este projeto utiliza User Secrets do .NET para armazenar dados sensíveis (como chave JWT e senha de e-mail). O arquivo `appsettings.json` contém apenas valores de exemplo.
</em></p>
    <p>Execute os comandos abaixo na pasta do projeto da API:</p>
    <pre><code>dotnet user-secrets init</code>
dotnet user-secrets set "JwtSettings:SecretKey" "sua-chave-aqui"</code>
dotnet user-secrets set "EmailSettings:SmtpPassword" "sua-senha-aqui"</code></pre>  
  </li>
  <li>
    <p><strong>Aplicar as migrações do banco de dados</strong>:</p>
    <pre><code>cd ToDoFlow.Infrastructure
dotnet ef database update --startup-project ../ToDoFlow.API</code></pre>
   <p><em>⚠️ As migrations são executadas a partir do projeto <code>ToDoFlow.Infrastructure</code>, que contém o DbContext.</em></p>
  </li>
  <li>
    <p><strong>Executar o backend</strong>:</p>
    <pre><code>dotnet run</code></pre>
  </li>
</ol>

<h4>3. Configurar e executar o Frontend</h4>
<ol>
  <li>
    <p>Abra um <strong>novo terminal</strong> e navegue até a pasta do frontend (dentro do diretório do projeto):</p>
    <pre><code>cd ToDoFlow.Frontend</code></pre>
  </li>
  <li>
    <p><strong>Instalar as dependências</strong>:</p>
    <pre><code>npm install</code></pre>
  </li>
  <li>
    <p><strong>Configurar a URL da API</strong>: Verifique (ou crie) o arquivo <code>src/environments/environment.ts</code> e <code>environment.prod.ts</code>. Altere a propriedade <code>apiUrl</code> para apontar para a URL onde seu backend está rodando (ex: <code>https://localhost:5001/api</code>).</p>
  </li>
  <li>
    <p><strong>Executar o frontend</strong>:</p>
    <pre><code>ng serve</code></pre>
  </li>
</ol>

<h3>✅ Verificação</h3>
<p>Após seguir os passos, você poderá acessar a aplicação, criar um novo usuário e começar a gerenciar suas tarefas e categorias.</p>

<h2>📎 Links</h2>
 <p>
  <a href="https://www.linkedin.com/in/arthurazevedo18">🔗 LinkedIn: Arthur Azevedo</a>
</p>

<h2>📄 Licença</h2>
<p>Este projeto foi desenvolvido para fins de estudo e portfólio profissional.</p>
