# UserSecurityManagerProjectApi 🛡️

![.NET](https://img.shields.io/badge/.NET-6-blue) ![Identity](https://img.shields.io/badge/Identity-blue) ![EntityFramework](https://img.shields.io/badge/EntityFramework-blue)

Este projeto é uma jornada para revisar e aprofundar conhecimentos em autorização, autenticação e JWT Tokens em uma API backend utilizando .NET. Ideal para quem busca compreender melhor estas tecnologias em um contexto prático.

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