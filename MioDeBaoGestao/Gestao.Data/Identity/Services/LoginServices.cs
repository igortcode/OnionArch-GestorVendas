using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Login;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Gestao.Core.Validations.Exceptions;
using Gestao.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Gestao.Data.Identity.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginServices(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<MessageDTO> LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return new MessageDTO("Logout efetuado com sucesso!");
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao efetuar o logout", Application.Enums.TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> SignInAsync(LoginDTO dto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);
                if (user == null)
                    return new MessageDTO("Usuário ou senha inválidos!", TipoNotificacao.Alert);                 

                if (!user.LockoutEnabled)
                    return new MessageDTO("Usuário bloqueado!", TipoNotificacao.Alert);            

                if (!user.EmailConfirmed)
                    return new MessageDTO("Aguardando confirmação de email!", TipoNotificacao.Alert);
            
                var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);

                if(!result.Succeeded)
                    return new MessageDTO("Usuário ou senha inválidos!", TipoNotificacao.Alert);

                return new MessageDTO("Login efetuado com sucesso!");

            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao efetuar o login!", Application.Enums.TipoNotificacao.Erro, ex);
            }
        }
    }
}
