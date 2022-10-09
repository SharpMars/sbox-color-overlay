namespace Sandbox;

public class ColorOverlay : RenderHook
{
	public Color Color { get; set; }
	
	/// <summary>
	/// 1 is fully visible, 0 is invisible
	/// </summary>
	public float Amount { get; set; }
	
	/// <summary>
	/// Blend mode used
	/// </summary>
	public BlendMode Mode { get; set; } = BlendMode.Normal;

	private readonly RenderAttributes _attributes = new();
	
	public override void OnStage( SceneCamera target, Stage renderStage )
	{
		if ( renderStage == Stage.AfterPostProcess )
		{
			_attributes.Set( "coloroverlay.color", Color );
			_attributes.Set( "coloroverlay.amount", Amount );
			_attributes.Set( "coloroverlay.mode", (uint) Mode );
			
			Graphics.GrabFrameTexture( "ColorBuffer", _attributes );
			Graphics.Blit( Material.FromShader( "color_overlay.vfx" ), _attributes );
		}
		
		base.OnStage( target, renderStage );
	}
	
	public enum BlendMode
	{
		Normal = 0,
		Multiply,
		Addition,
		Subtract,
		Screen,
		Overlay,
		HardLight,
		SoftLight,
		Lighten,
		Darken
	}
}
