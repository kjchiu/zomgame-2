using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Zomgame
{
    class GraphicsDispenser
    {
        static Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();
        static Dictionary<String, SpriteFont> fonts = new Dictionary<String, SpriteFont>();

        public static void initialize()
        {
            textures = new Dictionary<String, Texture2D>();
        }

        /// <summary>
        /// Used to add a texture that has not been loaded into the program yet. Creates a space so that other objects can use the texture eventually.
        /// </summary>
        /// <param name="_textureName">The name of the texture</param>
        public static void addNullTexture(string _textureName)
        {
            addTexture(_textureName, null);
        }

        public static void addTexture(string _textureName, Texture2D _texture)
        {
            if (getTexture(_textureName) == null)
            {
                textures.Add(_textureName, _texture);
            }
        }

        public static void removeTexture(string _textureName)
        {
            textures.Remove(_textureName);
        }

        public static void loadTexture(string _textureName, Texture2D _texture)
        {
            textures[_textureName] = _texture;
        }

        public static Texture2D getTexture(string _textureName)
        {
            Texture2D returnTexture;
            textures.TryGetValue(_textureName, out returnTexture);
            return returnTexture;
        }

        public static Dictionary<String, Texture2D>.KeyCollection TextureNames
        {
            get { return textures.Keys; }
        }

        public static Dictionary<String, Texture2D>.ValueCollection Textures
        {
            get { return textures.Values; }
        }

        public static void AddFont(string key, SpriteFont font)
        {
            fonts.Add(key, font);
        }

        public static SpriteFont GetFont(string key)
        {
            SpriteFont font;
            fonts.TryGetValue(key, out font);
            return font;
        }

    }
}
