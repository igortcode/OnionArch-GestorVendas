using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Usuario;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Gestao.Data.Context;
using Gestao.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Gestao.Data.Identity.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public UserServices(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<MessageDTO> AddUsuario(UsuarioDTO dto)
        {
            try
            {
                var user = new User(dto.UserName, dto.Email);

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _userManager.CreateAsync(user, dto.Senha);

                    if (!result.Succeeded)
                        return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                    result = await _userManager.AddToRoleAsync(user, dto.RoleName);

                    if (!result.Succeeded)
                        return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                    transaction.Complete();
                }

                return new MessageDTO("Cadastro de usuário efetuado com sucesso!");

            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao cadastrar o usuário!", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> AlterarSenhaComTokenSenha(UsuarioAlteraSenhaDTO dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user == null)
                    throw new InvalidOperationException("Não foi encontrado nenhum usuário com esse email.");

                var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Senha);

                if (!result.Succeeded)
                    return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                return new MessageDTO("Senha alterada com sucesso!");
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao alterar a senha do usuário!", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> AtualizarUsuario(AtualizaUsuarioDTO dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.Id);

                if (user == null)
                    throw new InvalidOperationException("Não foi encontrado nenhum usuário com esse identificador.");

                if (!await _userManager.CheckPasswordAsync(user, dto.Senha))
                    return new MessageDTO("Senha inválida!", TipoNotificacao.Alert);

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    IdentityResult result = null;

                    if (!string.IsNullOrWhiteSpace(dto.NovaSenha))
                    {
                        result = await _userManager.ChangePasswordAsync(user, dto.Senha, dto.NovaSenha);

                        if (!result.Succeeded)
                            return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);
                    }

                    if (!string.IsNullOrWhiteSpace(dto.RoleName))
                    {
                        var roles = await _userManager.GetRolesAsync(user);


                        if (!roles.Contains(dto.RoleName))
                        {
                            result = await _userManager.RemoveFromRolesAsync(user, roles);
                            
                            if (!result.Succeeded)
                                return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                            result = await _userManager.AddToRoleAsync(user, dto.RoleName);

                        }
                    }

                    user.Atualizar(dto.UserName, dto.Email);

                    result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                        return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                    transaction.Complete();
                }

                return new MessageDTO("Usuário atualizado com sucesso!");

            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao atualizar o usuário!", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> InativarAtivarUsuario(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                    throw new InvalidOperationException("Não foi encontrado nenhum usuário com esse identificador.");

                var result = await _userManager.SetLockoutEnabledAsync(user, user.LockoutEnabled ? false : true);

                if (!result.Succeeded)
                    return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                return new MessageDTO("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao inativar/ativar o usuário!", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GList<ObterUsuarioDTO>> ListarUsuarios()
        {
            try
            {
                var result = from user in _context.Users
                             join userRoles in _context.UserRoles
                             on user.Id equals userRoles.UserId

                             let role = _context.Roles.Where(a => a.Id == userRoles.RoleId).FirstOrDefault()

                             select new ObterUsuarioDTO
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 UserName = user.UserName,
                                 RoleId = role.Id,
                                 RoleName = role.Name,
                                 Habilitado = user.LockoutEnabled
                             };


                return new GList<ObterUsuarioDTO>
                {
                    DTOs = await result.ToListAsync(),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };
            }
            catch (Exception ex)
            {
                return new GList<ObterUsuarioDTO> { Message = new MessageDTO("Erro ao buscar o usuário!", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GGet<string>> ObterTokenEsqueciSenha(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    throw new InvalidOperationException("Não foi encontrado nenhum usuário com esse email.");


                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                return new GGet<string>
                {
                    DTO = token,
                    Message = new MessageDTO("Token gerado com sucesso!")
                };

            }
            catch (InvalidOperationException ioe)
            {
                return new GGet<string> { Message = new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe) };
            }
            catch (Exception ex)
            {
                return new GGet<string> { Message = new MessageDTO("Erro ao gerar o token", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GGet<ObterUsuarioDTO>> ObterUsuarioPorEmail(string email)
        {
            try
            {
                var result = from user in _context.Users
                             join userRoles in _context.UserRoles
                             on user.Id equals userRoles.UserId

                             let role = _context.Roles.Where(a => a.Id == userRoles.RoleId).FirstOrDefault()

                             where user.Email.Equals(email)

                             select new ObterUsuarioDTO
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 UserName = user.UserName,
                                 RoleId = role.Id,
                                 RoleName = role.Name,
                                 Habilitado = user.LockoutEnabled
                             };

                return new GGet<ObterUsuarioDTO>
                {
                    DTO = await result.FirstOrDefaultAsync(),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterUsuarioDTO> { Message = new MessageDTO("Erro ao efetuar a busca", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GGet<ObterUsuarioDTO>> ObterUsuarioPorIdAsync(string id)
        {
            try
            {
                var result = from user in _context.Users
                             join userRoles in _context.UserRoles
                             on user.Id equals userRoles.UserId

                             let role = _context.Roles.Where(a => a.Id == userRoles.RoleId).FirstOrDefault()

                             where user.Id.Equals(id)

                             select new ObterUsuarioDTO
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 UserName = user.UserName,
                                 RoleId = role.Id,
                                 RoleName = role.Name,
                                 Habilitado = user.LockoutEnabled
                             };

                return new GGet<ObterUsuarioDTO>
                {
                    DTO = await result.FirstOrDefaultAsync(),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterUsuarioDTO> { Message = new MessageDTO("Erro ao efetuar a busca", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GGet<ObterUsuarioDTO>> ObterUsuarioPorUserNameAsync(string userName)
        {
            try
            {
                var result = from user in _context.Users
                             join userRoles in _context.UserRoles
                             on user.Id equals userRoles.UserId

                             let role = _context.Roles.Where(a => a.Id == userRoles.RoleId).FirstOrDefault()

                             where user.UserName.Equals(userName)

                             select new ObterUsuarioDTO
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 UserName = user.UserName,
                                 RoleId = role.Id,
                                 RoleName = role.Name,
                                 Habilitado = user.LockoutEnabled
                             };

                return new GGet<ObterUsuarioDTO>
                {
                    DTO = await result.FirstOrDefaultAsync(),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterUsuarioDTO> { Message = new MessageDTO("Erro ao efetuar a busca", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<MessageDTO> ResetarSenha(UsuarioAlteraSenhaDTO dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user == null)
                    throw new InvalidOperationException("Não foi encontrado nenhum usuário com esse email.");

                var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Senha);

                if (!result.Succeeded)
                    return new MessageDTO(result.Errors.Select(a => a.Description).ToList(), TipoNotificacao.Alert);

                return new MessageDTO("Senha alterada com sucesso!");
            }
            catch (InvalidOperationException ioe)
            {
                return new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe);
            }

            catch (Exception ex)
            {
                return new MessageDTO("Erro ao resetar as senha!", TipoNotificacao.Erro, ex);
            }
        }
    }
}
