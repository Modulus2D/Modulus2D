using NLog;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics.Core
{
    /// <summary>
    /// A shader program
    /// </summary>
    public class Shader
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public uint programId;

        public Shader(string vertexSource, string fragSource)
        {
            // Create shaders
            uint vertexId = CreateShader(vertexSource, ShaderType.VertexShader);
            uint fragId = CreateShader(fragSource, ShaderType.FragmentShader);

            // Create program
            programId = CreateProgram(vertexId, fragId);

            // Destroy shaders
            // DestroyShader(vertexId);
            // DestroyShader(fragId);
        }

        private uint CreateShader(string source, ShaderType type)
        {
            uint id = Gl.CreateShader(type);

            // Compile vertex shader
            string[] src = { source };
            Gl.ShaderSource(id, src);
            Gl.CompileShader(id);

            // Check shader
            Gl.GetShader(id, ShaderParameterName.CompileStatus, out int compiled);

            if (compiled == 0)
            {
                // Get length
                Gl.GetShader(id, ShaderParameterName.InfoLogLength, out int length);

                // Get error log
                StringBuilder builder = new StringBuilder(length);
                Gl.GetShaderInfoLog(id, builder.Capacity, out length, builder);
                
                logger.Error("Unable to compile shader: " + builder);
            }

            return id;
        }

        private void DestroyShader(uint id)
        {
            Gl.DeleteShader(id);
        }

        private uint CreateProgram(uint vertex, uint frag)
        {
            uint id = Gl.CreateProgram();

            // Attach shaders
            Gl.AttachShader(id, vertex);
            Gl.AttachShader(id, frag);

            // Link program
            Gl.LinkProgram(id);

            // Check program
            Gl.GetProgram(id, ProgramProperty.LinkStatus, out int linked);

            if (linked == 0)
            {
                // Get length
                Gl.GetProgram(id, ProgramProperty.InfoLogLength, out int length);

                // Get error log
                StringBuilder builder = new StringBuilder(length);
                Gl.GetProgramInfoLog(id, builder.Capacity, out length, builder);

                logger.Error("Unable to link program: " + builder);
            }

            Gl.GetProgram(id, ProgramProperty.ActiveAttributes, out int count);
            Gl.GetProgram(id, ProgramProperty.ActiveAttributeMaxLength, out int max);

            for (uint i = 0; i < count; i++)
            {
                StringBuilder builder = new StringBuilder(max);
                Gl.GetActiveAttrib(id, i, max, out int length, out int size, out int type, builder);
                
                Console.WriteLine(builder);
            }


            Gl.GetProgram(id, ProgramProperty.ActiveUniforms, out count);
            Gl.GetProgram(id, ProgramProperty.ActiveUniformMaxLength, out max);

            for (uint i = 0; i < count; i++)
            {
                StringBuilder builder = new StringBuilder(max);
                Gl.GetActiveUniform(id, i, max, out int length, out int size, out int type, builder);

                Console.WriteLine(builder);
            }

            // Detach shaders
            //Gl.DetachShader(id, vertex);
            //Gl.DetachShader(id, frag);

            return id;
        }

        public void Bind()
        {
            Gl.UseProgram(programId);
        }
    }
}
