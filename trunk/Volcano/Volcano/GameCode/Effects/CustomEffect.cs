using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Volcano
{
    public class CustomEffects
    {
        #region Variables

        public Effect MondoEffect;
        private float elapsedTime;

        /// <summary>
        /// under the new organization we can use a full set of
        /// variables to express our effect
        /// </summary>
        public Matrix gWorld;
        public Matrix gWIT;
        public Matrix gWInv;
        public Matrix gWVP;

        public Vector3 gEyePosW;
        public Vector3 gLightVecW;

        public float gTime;

        public int gViewportHeight;

        #endregion

        #region Constructors

        public CustomEffects()
        {
            elapsedTime = 0.0f;
        }

        #endregion

        #region Methods

        #region LavaAttacks

        #region Initialize
        public void Init()
        {
        }
        #endregion

        public void Update_LavaAttacks(Matrix world, Matrix view, Matrix projection, Vector3 eyePos)
        {
            MondoEffect.Parameters["gWorld"].SetValue(world);
            MondoEffect.Parameters["gWIT"].SetValue(Matrix.Invert(Matrix.Transpose(world)));
            MondoEffect.Parameters["gWInv"].SetValue(Matrix.Invert(world));
            MondoEffect.Parameters["gWVP"].SetValue(world * view * projection);
            MondoEffect.Parameters["gEyePosW"].SetValue(eyePos);
            MondoEffect.Parameters["gLightVecW"].SetValue(new Vector3(0.0f, -1.0f, 0.0f));
            MondoEffect.CommitChanges();
        }

        #region Phong Special Effects
        public void Update_Time(GameTime time)
        {
            elapsedTime += (float)time.ElapsedGameTime.Milliseconds / 100;
            MondoEffect.Parameters["gTime"].SetValue(elapsedTime);
            this.gTime = elapsedTime;
            MondoEffect.CommitChanges();
        }
        public void Set_Viewport_Height(int height)
        {
            MondoEffect.Parameters["gViewportHeight"].SetValue(height);
        }
        #endregion

        #region Set Phong Lighting
        public void Set_Phong_Diffuse(Vector3 diffuseMtrl, Vector4 diffuseLight)
        {
            MondoEffect.Parameters["gDiffuseMtrl"].SetValue(new Vector4(diffuseMtrl, 1.0f));
            MondoEffect.Parameters["gDiffuseLight"].SetValue(diffuseLight);
        }
        public void Set_Phong_Ambient(Vector4 ambientMtrl, Vector4 ambientLight)
        {
            MondoEffect.Parameters["gAmbientMtrl"].SetValue(ambientMtrl);
            MondoEffect.Parameters["gAmbientLight"].SetValue(ambientLight);
        }
        public void Set_Phong_Specular(Vector4 specularMtrl, Vector4 specularLight, float specularPower)
        {
            MondoEffect.Parameters["gSpecularMtrl"].SetValue(specularMtrl);
            MondoEffect.Parameters["gSpecularLight"].SetValue(specularLight);
            MondoEffect.Parameters["gSpecularPower"].SetValue(specularPower);
        }
        #endregion

        #region Set Phong Greymap

        public void Set_IsGreymapped(bool isGreyMapped)
        {
            MondoEffect.Parameters["gGreyMap"].SetValue(isGreyMapped);
            MondoEffect.CommitChanges();
        }
        public void Set_GreyMapColors(Vector3 colorA, Vector3 colorB)
        {
            MondoEffect.Parameters["gGreyMapColorA"].SetValue(colorA);
            MondoEffect.Parameters["gGreyMapColorB"].SetValue(colorB);
            MondoEffect.CommitChanges();
        }
        public void Set_GreyMapColors(Vector3 colorA, Vector3 colorB, Vector3 colorC)
        {
            MondoEffect.Parameters["gGreyMapColorA"].SetValue(colorA);
            MondoEffect.Parameters["gGreyMapColorB"].SetValue(colorB);
            MondoEffect.Parameters["gGreyMapColorC"].SetValue(colorC);
            MondoEffect.CommitChanges();
        }

        #endregion

        #region Set Phong Normalmap

        public void SetNormalMap(Texture2D normal_map)
        {
            MondoEffect.Parameters["gTexN"].SetValue(normal_map);

        }

        #endregion

        #endregion

        #region ChangeModelEffect

        /// <summary>
        /// Alters a model so it will draw using a custom effect, while preserving
        /// whatever textures were set on it as part of the original effects.
        /// </summary>
        public static void ChangeEffectUsedByModel(Model model, Effect replacementEffect)
        {
            // Table mapping the original effects to our replacement versions.
            Dictionary<Effect, Effect> effectMapping = new Dictionary<Effect, Effect>();

            foreach (ModelMesh mesh in model.Meshes)
            {
                // Scan over all the effects currently on the mesh.

                if (!Globals.convertedModels.ContainsKey(model))
                {
                    foreach (BasicEffect oldEffect in mesh.Effects)
                    {
                        // If we haven't already seen this effect...
                        if (!effectMapping.ContainsKey(oldEffect))
                        {
                            // Make a clone of our replacement effect. We can't just use
                            // it directly, because the same effect might need to be
                            // applied several times to different parts of the model using
                            // a different texture each time, so we need a fresh copy each
                            // time we want to set a different texture into it.
                            Effect newEffect = replacementEffect.Clone(
                                                        replacementEffect.GraphicsDevice);

                            // Copy across the texture from the original effect.
                            //newEffect.Parameters["gTex0"].SetValue(oldEffect.Texture);

                            effectMapping.Add(oldEffect, newEffect);
                        }
                    }
                    // Now that we've found all the effects in use on this mesh,
                    // update it to use our new replacement versions.
                    foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    {
                        meshPart.Effect = effectMapping[meshPart.Effect];
                    }
                    Globals.convertedModels.Add(model, model);

                    //System.Console.WriteLine("Model converted!");

                }
                else
                {
                    foreach (Effect oldEffect in mesh.Effects)
                    {
                        // If we haven't already seen this effect...
                        if (!effectMapping.ContainsKey(oldEffect))
                        {
                            BasicEffect tempEffect = (BasicEffect)oldEffect.Clone(oldEffect.GraphicsDevice);
                            // Make a clone of our replacement effect. We can't just use
                            // it directly, because the same effect might need to be
                            // applied several times to different parts of the model using
                            // a different texture each time, so we need a fresh copy each
                            // time we want to set a different texture into it.
                            Effect newEffect = replacementEffect.Clone(
                                                        replacementEffect.GraphicsDevice);

                            // Copy across the texture from the original effect.
                            newEffect.Parameters["gTex0"].SetValue(tempEffect.Texture);

                            effectMapping.Add(oldEffect, newEffect);
                        }
                    }
                    // Now that we've found all the effects in use on this mesh,
                    // update it to use our new replacement versions.
                    foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    {
                        meshPart.Effect = effectMapping[meshPart.Effect];
                    }
                }


            }
        }

        #endregion

        #endregion
    }

    #region Vertex Declarations

    #region VPNTTB
    public struct VertexPositionNormalTextureTangentBinormal
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoordinate;
        public Vector3 Tangent;
        public Vector3 Binormal;

        public static readonly VertexElement[] VertexElements =
        new VertexElement[]
        {
            new VertexElement(0, 0, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Position, 0),
            new VertexElement(0, sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Normal, 0),
            new VertexElement(0, sizeof(float) * 6, VertexElementFormat.Vector2, VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(0, sizeof(float) * 8, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Tangent, 0),
            new VertexElement(0, sizeof(float) * 11, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Binormal, 0),
        };


        public VertexPositionNormalTextureTangentBinormal(Vector3 position, Vector3 normal, Vector2 textureCoordinate, Vector3 tangent, Vector3 binormal)
        {
            Position = position;
            Normal = normal;
            TextureCoordinate = textureCoordinate;
            Tangent = tangent;
            Binormal = binormal;
        }

        public static int SizeInBytes { get { return sizeof(float) * 14; } }
    }
    #endregion

    #endregion

    #region Lights

    /// <summary>
    /// Describes the lights that we can have active in gameplay.
    /// </summary>
    public struct Lights
    {
        public Vector3 _position;

        ///Diffuse
        public Vector4 _diffuse_material;
        public Vector4 _diffuse_light;
        public Vector4 _diffuse_intensity;

        ///Specular
        public Vector4 _specular_material;
        public Vector4 _specular_light;

        /// <summary>
        /// Construct a Lights object.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="diffuse_material"></param>
        /// <param name="diffuse_light"></param>
        /// <param name="diffuse_intensity"></param>
        /// <param name="specular_material"></param>
        /// <param name="specular_light"></param>
        public Lights(Vector3 pos,
                      Vector4 diffuse_material,
                      Vector4 diffuse_light,
                      Vector4 diffuse_intensity,
                      Vector4 specular_material,
                      Vector4 specular_light)
        {
            _position = pos;
            _diffuse_material = diffuse_material;
            _diffuse_light = diffuse_light;
            _diffuse_intensity = diffuse_intensity;
            _specular_material = specular_material;
            _specular_light = specular_light;
        }

        public Lights(Lights light)
        {
            _position = light._position;
            _diffuse_material = light._diffuse_material;
            _diffuse_light = light._diffuse_light;
            _diffuse_intensity = light._diffuse_intensity;
            _specular_material = light._specular_material;
            _specular_light = light._specular_light;
        }

        /// <summary>
        /// Return distance of incoming point from this
        /// point.
        /// </summary>
        /// <param name="otherPosition"></param>
        /// <returns></returns>
        public float GetDistance(Vector3 otherPosition)
        {
            float x = otherPosition.X - _position.X;
            float y = otherPosition.Y - _position.Y;
            float z = otherPosition.Z - _position.Z;

            return (float)Math.Sqrt(x * x - y * y - z * z);
        }

        public static void AddLight(Lights light)
        {
            if (Globals.numLights + 1 <= Globals.maxLights)
            {
                Globals.lights[Globals.numLights] = new Lights(light);
                Globals.numLights++;
            }
        }
    }

    #endregion
}
