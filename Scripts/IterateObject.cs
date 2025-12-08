using Godot;
using System;

public partial class IterateObject : Area2D
{
	
	public AudioStreamPlayer2D _soundEffect;
	public Font _defaultFont;
	public virtual String GetCustomText()
	{
		return "Objeto"; 
	}
	public virtual String DefineSoundEffect()
	{
		return "";
	}
	private String texto = "";
	private Vector2 _posicaoMouseGlobal = Vector2.Zero;
	public Texture2D _mao;
	private readonly Vector2 sizeImg = new Vector2(32, 32);
	private Character _playerRef;
	
	public override void _Ready()
	{
		_defaultFont = GD.Load<Font>("res://seu_caminho/sua_fonte.ttf") ?? ThemeDB.FallbackFont;
		_mao = GD.Load<Texture2D>("res://assets/Cursor/CursorPegar.png");
		ZIndex = 1000;
		foreach (Node child in GetChildren())
			{
				if (child is CanvasItem visualItem)
				{
					visualItem.ShowBehindParent = true;
				}
			}
		this.MouseEntered += OnMouseEntered;
		this.MouseExited += OnMouseExited;
		this.InputEvent += OnInputEvent;
		
		_playerRef = GetTree().Root.GetNode<Character>("Node2D/Character");	
		
		QueueRedraw();
	}
	
	public void PlaySound()
	{

		_soundEffect.Play();

	}
	
	public override void _Process(double delta)
	{
		_posicaoMouseGlobal = GetViewport().GetMousePosition();
		
		if (!string.IsNullOrEmpty(texto))
		{
			QueueRedraw();
		}
	}
	
	
	
	public void OnMouseEntered()
	{
		GD.Print("Ta na area do objeto");
		//Detecta que o Mouse entrou na area do objeto e muda o cursor de acordo
		texto = GetCustomText();
		QueueRedraw();
		//SetDefaultCursorShape
		Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
		Input.SetCustomMouseCursor(_mao, Input.CursorShape.PointingHand, new Vector2(16, 16));
	}
	
	public void OnMouseExited()
	{
		//Detecta que o Mouse saiu da area do obj e muda o cursor ao padrao
		texto = "";
		QueueRedraw();
		
		Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
	}
	
	
	public void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		
		//Verifica se foi clicado
		if (@event.IsActionPressed("mouse1_click"))
		{
			GD.Print("Objeto foi clicado");
			Acao();
		}
	}
	
	
	public virtual void Acao()
	{
	}
	
	public override void _Draw()
	{
		if (_playerRef != null && _playerRef.emPopup)
		{
			// Se estiver em popup, não desenhe o texto flutuante.
			return; 
		}
		// Se o texto local estiver vazio (após OnMouseExited), o bloco nem será executado.
		if (_defaultFont != null && !string.IsNullOrEmpty(texto)) 
		{
			Vector2 posicaoTexto = ToLocal(_posicaoMouseGlobal);
			posicaoTexto.Y -= 10;
			posicaoTexto.X -= 45; 
			DrawString(_defaultFont, 
			posicaoTexto, 
			texto, 
			HorizontalAlignment.Center, 
			90, 
			8, 
			Colors.Red);
		}
	}
}
