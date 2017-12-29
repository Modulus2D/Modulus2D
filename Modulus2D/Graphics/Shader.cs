using Modulus2D.Math;
using NLog;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modulus2D.Graphics
{
    public class Shader
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private uint program;
        private List<uint> shaders;

        private Dictionary<string, Uniform> uniforms;
        
        public Shader()
        {
            shaders = new List<uint>();

            uniforms = new Dictionary<string, Uniform>();
        }
                
        public void AddVertex(string source)
        {
            AddShader(source, ShaderType.VertexShader);
        }

        public void AddFragment(string source)
        {
            AddShader(source, ShaderType.FragmentShader);
        }

        public void Link()
        {
            // Create program
            program = Gl.CreateProgram();

            // Attach shaders
            for(int i = 0; i < shaders.Count; i++)
            {
                Gl.AttachShader(program, shaders[i]);
            }

            // Link program
            Gl.LinkProgram(program);

            // Check for errors
            Gl.GetProgram(program, ProgramProperty.LinkStatus, out int compiled);

            if (compiled != Gl.TRUE)
            {
                // Get error
                Gl.GetProgram(program, ProgramProperty.InfoLogLength, out int length);

                StringBuilder builder = new StringBuilder(length);
                Gl.GetProgramInfoLog(program, length, out length, builder);

                logger.Error("Unable to link program: " + builder);
            }

            GetUniforms();
        }

        internal void Bind()
        {
            Gl.UseProgram(program);
        }

        private void AddShader(string source, ShaderType type)
        {
            // Create shader
            uint id = Gl.CreateShader(type);

            Gl.ShaderSource(id, new string[] { source });
            Gl.CompileShader(id);

            // Check if shader compiled
            Gl.GetShader(id, ShaderParameterName.CompileStatus, out int compiled);
            
            if (compiled == Gl.TRUE)
            {
                // Add to shaders list
                shaders.Add(id);
            } else
            {
                // Get error
                Gl.GetShader(id, ShaderParameterName.InfoLogLength, out int length);
                
                StringBuilder builder = new StringBuilder(length);
                Gl.GetShaderInfoLog(id, length, out length, builder);

                Console.WriteLine("Unable to comile shader: " + builder);
            }
        }

        private void GetUniforms()
        {
            Gl.GetProgram(program, ProgramProperty.ActiveUniforms, out int numUniforms);
            Gl.GetProgram(program, ProgramProperty.ActiveUniformMaxLength, out int bufferSize);

            for (uint i = 0; i < numUniforms; i++)
            {
                StringBuilder builder = new StringBuilder();
                Gl.GetActiveUniform(program, i, bufferSize, out int length, out int size, out int type, builder);

                int location = Gl.GetUniformLocation(program, builder.ToString());

                uniforms.Add(builder.ToString(), new Uniform(location, type));
            }
        }

        /// <summary>
        /// Gets a reference to a uniform with the given name
        /// 
        /// Supported types include: float, Vector2, Vector3, Vector4, Matrix3, and Matrix4
        /// </summary>
        /// <param name="name">Uniform name</param>
        /// <returns>A reference to the uniform</returns>
        public Uniform GetUniform(string name)
        {
            return uniforms[name];
        }

        // Uniform setters
        public void Set(Uniform uniform, float value)
        {
            Gl.Uniform1(uniform.location, value);
        }

        public void Set(Uniform uniform, Vector2 value)
        {
            Gl.Uniform2(uniform.location, value.X, value.Y);
        }

        public void Set(Uniform uniform, Vector3 value)
        {
            Gl.Uniform3(uniform.location, value.X, value.Y, value.Z);
        }

        public void Set(Uniform uniform, Vector4 value)
        {
            Gl.Uniform4(uniform.location, value.X, value.Y, value.Z, value.W);
        }

        public void Set(Uniform uniform, Matrix3 mat)
        {
            Gl.UniformMatrix3(uniform.location, false, mat.Elements);
        }

        public void Set(Uniform uniform, Matrix4 mat)
        {
            Gl.UniformMatrix4(uniform.location, false, mat.Elements);
        }
    }
    
    /// <summary>
    /// Uniform reference class
    /// </summary>
    public class Uniform
    {
        internal int location;
        internal int type;

        internal Uniform(int location, int type) {
            this.location = location;
            this.type = type;
        }
    }
}
