using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Zomgame.Props
{        
    public class Light : Prop
    {
		float strength; //1 to 100 power, which translates to 1 to 20 blocks of light (dimming as it travels)
        Color lightColor;

        public Light(int nStr)
            : this (nStr, Color.White)
        {
        }

		public Light(float nStr, Color nCol)
        {
            strength = nStr;
            lightColor = nCol;
            this.Graphic = new Sprite("light_source_bmp");
            this.Passable = true;
            this.SeeThrough = true;
        }

        /// <summary>
        /// Strength of a light, ranging from 1 to 100. This translates to 1 to 20 feet of lit distance, dimming as the light travels.
        /// </summary>
        public float Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public Color LightColor
        {
            get { return lightColor; }
            set { lightColor = value; }
        }

    }
}
