using Godot;
using System;
using Microsoft.Data.Sqlite; // Importante para o banco
using System.IO;

public partial class CelularUI : CanvasLayer
{
	private TextEdit _inputTexto;
	private Control _containerCelular;
	private string _dbPath;

	public override void _Ready()
	{
		// Referências aos nós (ajuste os caminhos conforme sua cena)
		_inputTexto = GetNode<TextEdit>("Control/TextEdit");
		_containerCelular = GetNode<Control>("Control");

		// Configura o caminho do banco de dados na pasta do usuário (seguro para jogos)
		string userDir = ProjectSettings.GlobalizePath("user://");
		_dbPath = Path.Combine(userDir, "game_data.db");

		// Inicializa o banco e carrega dados salvos
		InicializarBanco();
		CarregarTexto();

		// Começa escondido
		this.Visible = false;
	}

	private void InicializarBanco()
	{
		using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
		{
			connection.Open();
			var command = connection.CreateCommand();
			command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Notas (
                    id INTEGER PRIMARY KEY,
                    conteudo TEXT
				);";
			command.ExecuteNonQuery();
		}
	}

	
	public void _on_btn_salvar_pressed()
	{
		GD.Print(">>> CLIQUEI NO BOTÃO SALVAR! <<<");
		string textoParaSalvar = _inputTexto.Text;

		using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
		{
			connection.Open();
			var command = connection.CreateCommand();
			command.CommandText = @"
				INSERT OR REPLACE INTO Notas (id, conteudo) VALUES (1, $texto);";
			command.Parameters.AddWithValue("$texto", textoParaSalvar);
			command.ExecuteNonQuery();
		}
		GD.Print("Texto salvo no SQLite!");
	}

	private void CarregarTexto()
	{
		using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
		{
			connection.Open();
			var command = connection.CreateCommand();
			command.CommandText = "SELECT conteudo FROM Notas WHERE id = 1;";
			
			using (var reader = command.ExecuteReader())
			{
				if (reader.Read())
				{
					_inputTexto.Text = reader.GetString(0);
				}
			}
		}
	}

	public void ToggleCelular()
	{
		GD.Print(">>> SUCESSO! ENTREI NA FUNÇÃO TOGGLE CELULAR! <<<"); 
		
		this.Visible = !this.Visible;
		GD.Print($"Agora a visibilidade é: {this.Visible}");
	}
}
