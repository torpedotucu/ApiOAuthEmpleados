using ApiOAuthEmpleados.Helpers;
using ApiOAuthEmpleados.Models;
using ApiOAuthEmpleados.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ApiOAuthEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryHospital repo;
        //CUANDO GENEREMOS EL TOKEN DEBEMOS INTEGRAR ALGUNOS DATOS COMO ISSUER Y DEMAS
        private HelperActionServicesOAuth helper;

        public AuthController(RepositoryHospital repo, HelperActionServicesOAuth helper)
        {
            this.repo=repo;
            this.helper=helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Empleado empleado = await this.repo.LogInEmpleadoAsync(model.UserName, int.Parse(model.Password));
            if (empleado==null)
            {
                return Unauthorized();
            }
            else
            {
                //DEBEMOS CREAR UNAS CREDENCIALES PARA INCLUIRLAS DENTRO DEL TOKEN Y QUE ESTARÁN COMPUESTAS POR EL SECRET KEY CIFRADO 
                //Y EL TIPO DE CIFRADO QUQE INCLUIREMOS EN EL TOKEN
                SigningCredentials credentials = new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.RsaSsaPssSha384Signature);
                /*EL TOKEN SE GENERA CON UNA CLASE Y DEBEMOS INDICAR LOS DATOS QUE ALMACENARÁ EN SU INTERIOR*/
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    notBefore:DateTime.UtcNow
                    );
                
            }
        }

    }
}
