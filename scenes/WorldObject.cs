using Godot;

public partial class WorldObject : Area2D
{
	// 1. Criamos um espaço para arrastar a cena do POPUP
	[Export] public PackedScene InspectSceneToLoad;

	// 2. MUDANÇA AQUI: Criamos um espaço para arrastar a UILAYER
	// Em vez de procurar por texto, vamos referenciar direto
	[Export] public CanvasLayer UiLayerRef; 

	public override void _Ready()
	{
		// 3. Apague a linha antiga do "_mainUiLayer = ..."
		// Não precisamos fazer nada aqui no _Ready, pois vamos configurar no Editor
		
		InputEvent += OnInputEvent;
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			OpenInspection();
		}
	}

	private void OpenInspection()
	{
		// Verificação de segurança
		if (InspectSceneToLoad == null)
		{
			GD.PrintErr("Faltou colocar a Cena do Popup no Inspector!");
			return;
		}
		
		if (UiLayerRef == null)
		{
			GD.PrintErr("Faltou colocar a UILayer no Inspector!");
			return;
		}

		var popupInstance = InspectSceneToLoad.Instantiate() as InspectPopupBase;
		
		if (popupInstance != null)
		{
			// Agora usamos a referência que arrastamos
			UiLayerRef.AddChild(popupInstance);
		}
	}
}
