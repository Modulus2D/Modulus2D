using Modulus2D.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class OrthoCamera
    {
        private Matrix4 proj;

        private Matrix4 translationView;
        private Matrix4 rotationView;
        private Matrix4 view;

        private Matrix4 combined;

        private float aspect = 1f;
        private float size = 1f;
        private float near = 0f;
        private float far = 1f;

        private float angle = 0f;

        private Vector3 center;

        private bool centerDirty = true;
        private bool angleDirty = true;

        private bool projDirty = true;

        /// <summary>
        /// Aspect ratio of the camera (width/height). This value may be changed by resizing the camera
        /// </summary>
        public float Aspect { get => aspect; }

        /// <summary>
        /// Distance in world units from the top of the camera to the center of the camera
        /// </summary>
        public float Size {
            get => size;
            set
            {
                projDirty = true;
                size = value;
            }
        }

        public float Far {
            get => far;
            set
            {
                projDirty = true;
                far = value;
            }
        }

        public float Near
        {
            get => near;
            set
            {
                projDirty = true;
                near = value;
            }
        }

        public Vector3 Center
        {
            get => center;
            set
            {
                centerDirty = true;

                center = value;
            }
        }

        public float Angle {
            get => angle;
            set {
                angleDirty = true;
                
                angle = value;
            }
        }

        public OrthoCamera(int width, int height)
        {
            Resize(width, height);
        }

        /// <summary>
        /// Call this before using the camera
        /// </summary>
        public Matrix4 Update()
        {
            bool recalc = false;

            bool recalcView = false;
            if (angleDirty)
            {
                rotationView = Matrix4.RotateZ(angle);

                angleDirty = false;
                recalcView = true;
            }

            if (centerDirty)
            {
                translationView = Matrix4.Translate(-Center);

                centerDirty = false;
                recalcView = true;
            }

            if(recalcView)
            {
                view = rotationView * translationView;
                recalc = true;
            }

            if (projDirty)
            {
                proj = Matrix4.Ortho(aspect * Size, -aspect * Size, Size, -Size, far, near);

                projDirty = false;
                recalc = true;
            }

            if (recalc)
            {
                combined = proj * view;
            }

            return combined;
        }

        public void Resize(int width, int height)
        {
            aspect = (float)width / height;

            projDirty = true;
        }
    }
}
