using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

namespace SystemLock___Projeto_2024.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult ValidarLogin()
        {
            return View(); // Carrega a view de login
        }

        // Valida as credenciais (POST)
        [HttpPost]
        public IActionResult ValidarLogin(string email, string senha)
        {
            // Verificação das credenciais
            if (email == "systemlock@gmail.com" && senha == "1234")
            {
                // Se as credenciais forem válidas, redireciona para a página de administração
                return RedirectToAction("Admin");
            }
            else
            {
                // Se as credenciais forem inválidas, retorna à view de login com mensagem de erro
                ViewBag.ErrorMessage = "Email ou senha inválidos, digite novamente.";
                return RedirectToAction("Funcionarios", "Pagina_Home"); // Retorna à mesma view de login com a mensagem de erro
            }
        }

        // Página de Administração
        public IActionResult Admin()
        {
            return View(); // Exibe a página de administração
        }
        public IActionResult Criar_perfil()
        {
            ViewBag.Layout = "~/Views/Shared/_Layout-funcionarios.cshtml";
            return View(); // Carrega a view Admin.cshtml
        }
		public IActionResult Retornar()
		{
            ViewBag.Layout = "~/Views/Shared/_Layout-geral.cshtml";
            return RedirectToAction("Funcionarios","Pagina_Home");

        }
		public IActionResult Banco_funcionario()
        {
            return View();
        }
    }
}


