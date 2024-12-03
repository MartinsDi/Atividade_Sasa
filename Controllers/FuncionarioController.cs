using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using SystemLock___Projeto_2024.Models;

namespace SystemLock___Projeto_2024.Controllers
{
	public class FuncionarioController : Controller
	{

		public static string GerarHashMd5(string input)
		{
			MD5 md5Hash = MD5.Create();
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));// Converter a String para array de bytes, que é como a biblioteca trabalha.
			StringBuilder sBuilder = new StringBuilder();// Cria-se um StringBuilder para recompôr a string.
			for (int i = 0; i < data.Length; i++)// Loop para formatar cada byte como uma String em hexadecimal
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}

		[HttpPost]
		public String Logout()
		{
			try
			{
				HttpContext.Session.Remove("nome");
				HttpContext.Session.Clear();
				return "ok";
			}
			catch (Exception ex)
			{
				return "falha: " + ex.Message;
			}

		}

		[HttpPost]
		public string Login(string email, string senha)
		{
			/*
            HttpContext.Session.SetString("nome", "Leandro");
			string resultado = "ok";
            return resultado;
            */


			String resultado = "";
			SQLiteConnection? sqlite_conn = null;
			SQLiteDataReader? dr = null;
			try
			{
				sqlite_conn = Conexao.Nova();
				sqlite_conn.Open();
				senha = GerarHashMd5(senha);
				string sql = $"select * from funcionario where email='{email}' and senha='{senha}'";

				SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
				dr = comandoSQL.ExecuteReader();

				if (dr.Read())
				{
					HttpContext.Session.SetString("nome", (string)dr["nome"]);
					resultado = "ok";
				}
				else
				{
					resultado = "falha";
				}
				dr.Close();
				sqlite_conn.Close();

				return resultado;
			}
			catch (Exception ex)
			{
				if (dr != null)
				{
					dr.Close();
				}
				if (sqlite_conn != null)
				{
					sqlite_conn.Close();
				}
				return "falha";
			}

		}

		[HttpPost]
		public String Buscar()
		{
			SQLiteConnection? sqlite_conn = null;
			try
			{
				sqlite_conn = Conexao.Nova();
				sqlite_conn.Open();

				string sql = $"select * from funcionario where Status = 1";

				SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
				SQLiteDataReader dr = comandoSQL.ExecuteReader();
				String linhas = "";
				while (dr.Read())
				{
					String linha = "<tr>";
					linha += $"<td><img src='/img//icon.gerais/icon.editor.png'style='width: 40px; height: 40px;' onclick='editar(\"{dr.GetInt32(0)}\", \"{dr.GetString(2)}\", \"{dr.GetString(1)}\")'/></td>";
					linha += $"<td><img src='/img/icon.gerais/icon.excluir.png'style='width: 40px; height: 40px;' onclick='confirmarExcluir(\"{dr.GetInt32(0)}\")'/></td>";
					linha += $"<td>{dr.GetInt32(0)}</td>";
					linha += $"<td>{dr.GetString(1)}</td>";
					linha += $"<td>{dr.GetString(2)}</td>";
					linha += "</tr>";

					linhas += linha;
				}
				dr.Close();
				sqlite_conn.Close();

				return linhas;
			}
			catch (Exception ex)
			{
				if (sqlite_conn != null)
				{
					sqlite_conn.Close();
				}
				return "Não foi possível consultar!!!";
			}
		}

		[HttpPost]
		public String Inserir(string nome, string email, string senha)
		{
			senha = GerarHashMd5(senha);

			SQLiteConnection? sqlite_conn = null;
			try
			{
				sqlite_conn = Conexao.Nova();
				sqlite_conn.Open();
				string sql = $"insert into funcionario(nome,email,senha,Status) values ('{nome}','{email}','{senha}',1)";
				SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
				string resposta;
				if (comandoSQL.ExecuteNonQuery() > 0)
				{
					resposta = "ok";
				}
				else
				{
					resposta = "Não foi possível inserir!!!";
				}
				sqlite_conn.Close();
				return resposta;
			}
			catch (Exception ex)
			{
				if (sqlite_conn != null)
				{
					sqlite_conn.Close();
				}
				return "Não foi possível inserir";
			}
		}

		[HttpPost]
		public String Alterar(int matricula, string nome, string email, string senha)
		{
			senha = GerarHashMd5(senha);

			SQLiteConnection? sqlite_conn = null;
			try
			{	
				Console.WriteLine(matricula + " x");
				sqlite_conn = Conexao.Nova();
				sqlite_conn.Open();
				string sql = $"update funcionario set nome='{nome}', email='{email}', senha='{senha}' where Matrícula={matricula}";
				SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
				string resposta;
				if (comandoSQL.ExecuteNonQuery() > 0)
				{
					resposta = "ok";
				}
				else
				{
					resposta = "Não foi possível alterar!!!";
				}
				sqlite_conn.Close();
				return resposta;
			}
			catch (SQLiteException ex)
			{
				if (sqlite_conn != null)
				{
					sqlite_conn.Close();
				}
				return $"Não foi possível alterar {ex.Message}";
			}
		}


		[HttpPost]
		[Route("Funcionario/Excluir")]
		public string Excluir(int matricula)
		{
			SQLiteConnection sqlite_conn = null;
			try
			{
				string sql = $"update funcionario set Status = 0 where Matrícula={matricula}";  // SQL para exclusão

				sqlite_conn = Conexao.Nova();
				sqlite_conn.Open();  // Abre a conexão com o banco

				SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
				string resposta;

				if (comandoSQL.ExecuteNonQuery() > 0)
				{
					resposta = "ok";
				}
				else
				{
					resposta = "Não foi possível excluir!!!";
				}

				sqlite_conn.Close();
				return resposta;
			}
			catch (Exception ex)
			{
				if (sqlite_conn != null)
				{
					sqlite_conn.Close();
				}
				return "Não foi possível excluir";
			}
		}


	}

}
