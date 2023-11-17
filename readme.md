# UserSecurityManagerProjectApi 🛡️

![.NET](https://img.shields.io/badge/.NET-6-blue) ![Identity](https://img.shields.io/badge/Identity-blue) ![EntityFramework](https://img.shields.io/badge/EntityFramework-blue)

Este projeto foi construido para revisar e aprofundar conhecimentos em autorização, autenticação e JWT Tokens em uma API backend utilizando .NET. 

1. **ASP.NET Core Identity**: Esta é uma framework integrada ao .NET que facilita a implementação de funcionalidades relacionadas à gestão de usuários, como registro, autenticação e autorização. No Identity, cada usuário é representado por uma entidade `ApplicationUser`, que pode ser estendida para incluir mais informações conforme necessário. O Identity gerencia de forma segura as senhas dos usuários, armazenando apenas hashes destas, e fornece uma série de funcionalidades prontas para uso, como confirmação de e-mail e recuperação de senha.

2. **JWT (JSON Web Token)**: JWT é um padrão aberto (RFC 7519) que define uma maneira compacta e autossuficiente de transmitir informações entre partes como um objeto JSON. Essas informações podem ser verificadas e confiáveis porque são assinadas digitalmente. No contexto desta aplicação, após o usuário se autenticar com sucesso usando suas credenciais, um token JWT é gerado e enviado de volta ao usuário.

3. **Geração de Token**: Ao realizar o login, o sistema utiliza o `UserManager` do ASP.NET Core Identity para validar as credenciais do usuário. Se a autenticação for bem-sucedida, um token JWT é gerado usando a classe `JwtSecurityTokenHandler`. Este token inclui claims (afirmações) que armazenam informações do usuário, como o ID e outros dados relevantes, e é assinado com uma chave secreta configurada no servidor.

4. **Validação e Autorização**: Para acessar os endpoints protegidos na API, o cliente deve enviar o token JWT obtido no cabeçalho `Authorization` das requisições HTTP. O middleware de autenticação do ASP.NET processa esse token, valida sua assinatura e, se válido, estabelece o contexto do usuário com base nas claims presentes no token. Isso habilita a aplicação a executar controles de acesso baseados em roles ou outras claims.

5. **Configuração**: A configuração do JWT é realizada no arquivo `Startup.cs` ou `Program.cs`, onde especificamos as opções de validação do token, como a chave de assinatura, o emissor, o público e o tempo de expiração. Essas configurações são críticas para garantir que os tokens sejam válidos apenas se correspondem exatamente às configurações do servidor.

6. **Segurança**: Para maximizar a segurança, é essencial manter a chave de assinatura do JWT segura e não expô-la em repositórios públicos ou locais inseguros. Além disso, o uso de HTTPS é recomendado para proteger os tokens durante sua transmissão entre o cliente e o servidor.

A combinação do ASP.NET Core Identity com JWT oferece um sistema de autenticação e autorização robusto, flexível e seguro, essencial para aplicações modernas baseadas em web.

## 🚀 Tecnologias Utilizadas

- **.NET 6**
- **Identity**
- **EntityFramework (InMemory)**

## 📝 Fluxo de Funcionamento

1. **Registro do Usuário** 📌
   - O usuário se registra fornecendo `username` e `email`.

2. **Geração e Envio do Token** 📧
   - Um token é gerado pelo Identity e enviado ao usuário via email.

3. **Validação do Email** ✔️
   - O usuário clica no link enviado por email, que o direciona para um endpoint da API.
   - A API valida o token e confirma a validação do email do usuário.

4. **Emissão do Token JWT** 🎫
   - Após a validação, um token de acesso JWT é fornecido, permitindo que o usuário defina sua senha através de um endpoint específico da API.

5. **Finalização do Registro** ✅
   - Com a senha definida, o registro do usuário é completo.
   - O usuário agora pode utilizar o endpoint de Login, que fornece um token específico para acessar outros endpoints da aplicação.