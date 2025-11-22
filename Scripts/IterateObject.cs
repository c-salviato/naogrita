using Godot;
using System;

public partial class IterateObject : Area2D
{
	
	private Font _defaultFont;
	private String texto = "";
	private Vector2 _posicaoMouseGlobal = Vector2.Zero;
	private Texture2D _mao;
	private readonly Vector2 sizeImg = new Vector2(32, 32);
	
	public override void _Ready()
	{
		_defaultFont = GD.Load<Font>("res://seu_caminho/sua_fonte.ttf") ?? ThemeDB.FallbackFont;
		_mao = GD.Load<Texture2D>("res://assets/Cursor/Porno.png");
		

		
		ZIndex = 1000;
		
		QueueRedraw();
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
		texto = "Objeto";
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
	
	
	public void Acao()
	{
		//Nome Auto Explicativo por padrao vai fazer algo =O
		
	}
	
	
	public override void _Draw()
	{
		if (_defaultFont != null)
		{
			Vector2 posicaoTexto = ToLocal(_posicaoMouseGlobal);
			posicaoTexto.Y -= 5;
			posicaoTexto.X -= 45; 
			DrawString(_defaultFont, 
					   posicaoTexto, 
					   texto, 
					   HorizontalAlignment.Center, 
					   90, 
					   22, 
					   Colors.Red);
		}
	}
	
	
}
