/*
 * Graphics Dispenser 
 * 
 * Used to load in any and all graphics and fonts.
 * 
 * To Use: First, call initialize(). Then use the LoadTextureData() function, using a search string. 
 * Then use getTexture() to retrieve the texture (which, if using the Sprite class, should be done automatically.)
 * 
 */


using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using System.IO;

namespace Zomgame.Graphics
{
    class GraphicsDispenser
    {
        static Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();
        static Dictionary<String, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        private const String TEXTURE_NOT_FOUND = "error";

        public static void initialize()
        {
            textures = new Dictionary<String, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
        }

        /// <summary>
        /// Loads any texture data in the Content directory. 
        /// With no search pattern this function loads pretty much everything.
        /// </summary>
        /// <param name="Content">The ContentManager for the program.</param>
        public static void LoadTextureData(ContentManager Content)
        {
            DirectoryInfo di = new DirectoryInfo(Content.RootDirectory);

            LoadTextureData(Content, "*");
        }

        /// <summary>
        /// Loads any texture data in the Content directory.
        /// </summary>
        /// <param name="Content">The ContentManager for the program.</param>
        /// <param name="searchPattern">The pattern in the filename to search for, ie "*_png". Will always search for '.xnb' files</param>
        public static void LoadTextureData(ContentManager Content, string searchPattern)
        {
            DirectoryInfo di = new DirectoryInfo(Content.RootDirectory);

            foreach (FileInfo fi in di.GetFiles(searchPattern + ".xnb"))
            {
                try
                {
                    GraphicsDispenser.AddTexture(fi.Name.Remove(fi.Name.Length - 4), Content.Load<Texture2D>(fi.Name.Remove(fi.Name.Length - 4)));
                }
                catch (ContentLoadException eCLE)
                {
                    Console.WriteLine("CONTENT_LOAD_EXCEPTION - Could not load file: " + eCLE);
                }
            }
        }

        /// <summary>
        /// Loads any font data in the Content directory.
        /// </summary>
        /// <param name="Content">The ContentManager for the program.</param>
        public static void LoadFontData(ContentManager Content)
        {
            DirectoryInfo di = new DirectoryInfo(Content.RootDirectory);
           
            foreach (FileInfo fi in di.GetFiles("Font/*.xnb", SearchOption.AllDirectories))
            {
                AddFont(fi.Name.Remove(fi.Name.Length - 4), Content.Load<SpriteFont>("Font/" + fi.Name.Remove(fi.Name.Length - 4)));
            }

        }

        /// <summary>
        /// Used to add a texture that has not been loaded into the program yet. Creates a space so that other objects can use the texture eventually.
        /// </summary>
        /// <param name="_textureName">The name of the texture</param>
        public static void AddNullTexture(string _textureName)
        {
            AddTexture(_textureName, null);
        }

        /// <summary>
        /// Adds the texture into the textures hash. A reference can be retrieved by using GetTexture.
        /// </summary>
        /// <param name="_textureName">The name of the texture. Typically the filename minus the filetype</param>
        /// <param name="_texture">The texture reference</param>
        public static void AddTexture(string _textureName, Texture2D _texture)
        {
            if (!textures.ContainsKey(_textureName))
            {
                textures.Add(_textureName, _texture);
            }
        }

        /// <summary>
        /// Adds the font into the font hash. A reference can be retrieved by using GetFont.
        /// </summary>
        /// <param name="_textureName">The name of the font. Typically the filename minus the filetype</param>
        /// <param name="_texture">The SpriteFont reference</param>
        public static void AddFont(string _fontName, SpriteFont _font)
        {
            if (GetFont(_fontName) == null)
            {
                fonts.Add(_fontName, _font);
            }
        }

        /// <summary>
        /// Removes a Texture2D from the texture Dictionary.
        /// </summary>
        /// <param name="_textureName">The key of the texture.</param>
        public static void RemoveTexture(string _textureName)
        {
            textures.Remove(_textureName);
        }

        /// <summary>
        /// Removes a SpriteFont from the font Dictionary.
        /// </summary>
        /// <param name="_fontName">The key of the font.</param>
        public static void RemoveFont(string _fontName)
        {
            fonts.Remove(_fontName);
        }

        /// <summary>
        /// Put a texture into the texture Dictionary, given a key and a Texture2D.
        /// Replaces any previous textures with the same name.
        /// </summary>
        /// <param name="_fontName">The name of the texture.</param>
        /// <param name="_font">The Texture2D object.</param>
        public static void LoadTexture(string _textureName, Texture2D _texture)
        {
            textures[_textureName] = _texture;
        }

        /// <summary>
        /// Put a font into the font Dictionary, given a key and a SpriteFont.
        /// Replaces any previous fonts with the same name.
        /// </summary>
        /// <param name="_fontName">The name of the font.</param>
        /// <param name="_font">The SpriteFont object.</param>
        public static void LoadFont(string _fontName, SpriteFont _font)
        {
            fonts[_fontName] = _font;
        }

        /// <summary>
        /// Returns a Texture2D in the texture Dictionary.
        /// </summary>
        /// <param name="_fontName">The name key of the texture.</param>
        /// <returns>The related Texture2D.</returns>
        public static Texture2D GetTexture(string _textureName)
        {
            Texture2D returnTexture;
            try
            {
                returnTexture = textures[_textureName];
            }
            catch (KeyNotFoundException)
            {
                // make damn sure that TEXTURE_NOT_FOUND is the name of an actual texture
                //
                returnTexture = textures[GraphicsDispenser.TEXTURE_NOT_FOUND];
                Console.WriteLine("KEY_NOT_FOUND - Could not find texture with name [" + _textureName + "].");
            }
            return returnTexture;
        }

        /// <summary>
        /// Returns a SpriteFont in the font Dictionary.
        /// </summary>
        /// <param name="_fontName">The name key of the font.</param>
        /// <returns>The related SpriteFont.</returns>
        public static SpriteFont GetFont(string _fontName)
        {
            SpriteFont returnFont;
            fonts.TryGetValue(_fontName, out returnFont);
            return returnFont;
        }

        /// <summary>
        /// Returns a list of all the texture names.
        /// </summary>
        public static Dictionary<String, Texture2D>.KeyCollection TextureNames
        {
            get { return textures.Keys; }
        }

        /// <summary>
        /// Returns a list of all the Texture objects.
        /// </summary>
        public static Dictionary<String, Texture2D>.ValueCollection Textures
        {
            get { return textures.Values; }
        }

    }
}
