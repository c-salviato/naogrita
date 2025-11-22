using Godot;

public partial class InspectPopupBase : Control
{
	// Sinal para avisar o sistema que este popup fechou (opcional, mas útil)
	[Signal] public delegate void OnCloseEventHandler();

	public override void _Ready()
	{
		// Conecta o botão de fechar (supondo que o nome do nó seja "CloseButton")
		GetNode<Button>("CloseButton").Pressed += ClosePopup;
	}

	public virtual void ClosePopup()
	{
		EmitSignal(SignalName.OnClose);
		QueueFree(); // Destrói o objeto ampliado para limpar memória
	}

	// Método virtual que você vai sobrescrever nos filhos para interações específicas
	public virtual void Interact()
	{
		GD.Print("Interação genérica base");
	}
}
