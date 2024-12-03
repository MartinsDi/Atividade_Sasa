using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.IO;

namespace SystemLock___Projeto_2024.Models
{
	public class Conexao
	{
		// Método para criar uma nova conexão SQLite
		public static SQLiteConnection Nova()
		{
			// Obtém o caminho absoluto do arquivo SystemLock.db dentro do wwwroot
			string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "SystemLock.db");

			// Define a string de conexão com o caminho correto do banco de dados
			string stringConnection = $"Data Source={dbPath}; Version=3; New=False; Compress=True;";

			// Retorna a conexão SQLite com a string configurada
			return new SQLiteConnection(stringConnection);
		}
	}
}
