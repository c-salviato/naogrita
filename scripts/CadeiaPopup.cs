using Godot;
using System.Collections.Generic; // Necessário para usar Listas

public partial class CadeiaPopup : InspectPopupBase
{
	[Export] public int MaxDigitos = 4;
	
	// Referências
	private HBoxContainer _displayContainer;
	private GridContainer _botoesContainer;

	// Armazena a senha atual que o jogador digitou (como nomes de textura ou IDs)
	private List<string> _senhaAtual = new List<string>();
	private readonly List<string>  _senhaCorreta = new List<string>(new string[]
{
	"res://assets/ObjetosInteragidos/Botoes/5.png",//5
	"res://assets/ObjetosInteragidos/Botoes/1.png",//1
	"res://assets/ObjetosInteragidos/Botoes/9.png",//9
	"res://assets/ObjetosInteragidos/Botoes/4.png"//4
});
	
	
	
	
	public override void _Ready()
	{
		base._Ready(); // Chama o _Ready da classe pai (fecha botão, etc)
		
		// Pegamos as referências dos containers
		// Adicionamos o "KeyPad/" no começo do caminho
		_displayContainer = GetNode<HBoxContainer>("KeyPad/VBoxContainer/DisplayContainer");
		_botoesContainer = GetNode<GridContainer>("KeyPad/VBoxContainer/BotoesContainer");

		ConectarBotoes();
	}
	
	
	private void ConectarBotoes()
	{
		// Percorre todos os filhos do container de botões
		foreach (var child in _botoesContainer.GetChildren())
		{
			// Verifica se o filho é um botão
			if (child is Button botao)
			{
				// Conecta o sinal Pressed dinamicamente usando uma função anônima (lambda)
				// Isso envia o ÍCONE (Textura) do botão clicado para a função
				botao.Pressed += () => AoClicarTecla(botao.Icon);
			}
		}
	}

	private void AoClicarTecla(Texture2D imagemSimbolo)
	{
		// 1. Verifica se já atingiu o limite
		if (_senhaAtual.Count >= MaxDigitos)
		{
			// Opcional: Tocar um som de "Erro" ou balançar a tela
			return;
		}

		// 2. Adiciona visualmente no display
		AdicionarImagemNoDisplay(imagemSimbolo);

		// 3. Adiciona na lógica (para conferir a senha depois)
		// Vamos usar o caminho do arquivo da imagem como "ID" único
		_senhaAtual.Add(imagemSimbolo.ResourcePath);
		
		// Debug
		GD.Print("Digitou. Total: " + _senhaAtual.Count);
		
		// Se encheu os 4 digitos, verifica a senha
		if (_senhaAtual.Count == MaxDigitos)
		{
			VerificarSenha();
		}
	}

	private void AdicionarImagemNoDisplay(Texture2D textura)
	{
		// Cria um novo TextureRect (um lugar para por a imagem)
		var rect = new TextureRect();
		
		// Define a imagem dele
		rect.Texture = textura;
		
		// Define o modo de expansão para não distorcer
		rect.ExpandMode = TextureRect.ExpandModeEnum.FitWidth;
		rect.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
		
		// Adiciona ao container horizontal
		_displayContainer.AddChild(rect);
	}

	private void VerificarSenha()
	{
		//verifica quantos digitos estao certos
		int corretos = 0;
		GD.Print("Senha digitada. Falta verificar aq");
		for(int index = 0; index < MaxDigitos; index++)
		{
			if(_senhaCorreta[index] == _senhaAtual[index])
			{
				GD.Print("Digito Correto YAYYYYY");
				corretos++;
			}
			else
			{
				GD.Print("Digito Incorreto");
			}
		}
		if(corretos == MaxDigitos)
		{
			GD.Print("Senha Correta");
			ClosePopup();
			EstadoCadeia();
		}
		else
		{
			GD.Print("Senha Incorreta");
			LimparDisplay();
		}
	}

	private void EstadoCadeia()
	{
		Cadeia cadeia = GetTree().GetFirstNodeInGroup("Objetos") as Cadeia;
		
		if (cadeia == null)
		{
			GD.PrintErr("ERRO DE Grupo: Não encontrei a Cadeia no grupo Objetos. Verifique a árvore remota.");
			// Dica: Se mudou o nome da cena Main ou do nó da Cadeia, isso falha.
			return;
		}
		cadeia.EstadoCadeia(true);
	}

	// Função útil para limpar o display (caso erre a senha)
	public void LimparDisplay()
	{
		_senhaAtual.Clear();
		
		// Destrói todas as imagens que estão no display
		foreach (var child in _displayContainer.GetChildren())
		{
			child.QueueFree();
		}
	}
}
