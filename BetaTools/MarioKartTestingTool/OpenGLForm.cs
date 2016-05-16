using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DEX;

namespace MarioKartTestingTool
{
    public partial class OpenGLForm : Form
    {
        
        public OpenGLForm()
        {
            InitializeComponent();
        }

        public void SetCommands(F3DEXCommandCollection commands)
        {
            openGLControl.GraphicsCollections.Add(VO64F3DEXReader.ReadCommands(commands));
        }
    }
}
