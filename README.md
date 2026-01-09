<h1>📋 ToDoFlow</h1>
<p>Um sistema completo de gerenciamento de tarefas com autenticação segura, desenvolvido com .NET 8 no backend e Angular 17 no frontend.</p>

<h2>📌 Sobre o Projeto</h2>
<p>O ToDoFlow é uma aplicação web que permite aos usuários criarem uma conta, realizarem login e gerenciarem suas tarefas, separando por categorias. O projeto foi desenvolvido com foco em boas práticas de arquitetura, segurança e padrões de desenvolvimento.</p>

<h2>🛠️ Funcionalidades</h2>
<ul>
  <li>Autenticação com JWT (Access Token + Refresh Token)</li>
  <li>Registro de usuários</li>
  <li>Criptografia de senhas</li>
  <li>CRUD de tarefas e categorias</li>
  <li>Relacionamento entre tarefas e categorias</li>
  <li>Acesso protegido por autenticação</li>
  <li>Recuperação de senha via e-mail (SMTP)</li>
</ul>

<h2>🏗️ Arquitetura</h2>
<ul>
  <li>API: Controllers, autenticação e endpoints</li>
  <li>Application: Dtos de cada caso</li>
  <li>Domain: Entidades e regras de negócio puras</li>
  <li>Services: Serviços de domínio e integrações</li>
  <li>Infrastructure: Acesso a dados, EF Core, SMTP, JWT e repositórios</li>
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
<h3>Frontend</h3>
<ul>
  <li>Angular 17.3.11</li>
  <li>HTML5 / CSS3</li>
  <li>Typescript 5.4.2</li>
</ul>

<h2>📎 Links</h2>
<p>Linkedin: "www.linkedin.com/in/arthurazevedo18"</p>

<h2>📄 Licença</h2>
<p>Este projeto foi desenvolvido para fins de estudo e portfólio profissional.</p>
