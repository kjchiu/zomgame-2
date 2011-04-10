
using Microsoft.Xna.Framework.Graphics;
using Graphics;
namespace Zomgame
{
	public class Sprite
	{
		private string imgLoc;
		private Texture2D texture;

		public Sprite(string nImgLoc)
		{
			imgLoc = nImgLoc;
			texture = GraphicsDispenser.getTexture(nImgLoc);
		}

		public string ImgLoc
		{
			get { return imgLoc; }
			set { imgLoc = value; }
		}

		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
		}
	}
}
