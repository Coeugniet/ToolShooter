using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shooter
{
    public class LevelWindow : EditorWindow
    {

        LevelProfile level;
        bool isMousePressed = false;

        public static void Init(LevelProfile level)
        {
            LevelWindow window = EditorWindow.GetWindow(typeof(LevelWindow)) as LevelWindow;

            window.level = level;

            window.Show();
            window.titleContent = new GUIContent(level.name + " editor");
            window.position = new Rect(Screen.currentResolution.width * 0.30f, Screen.currentResolution.height * 0.20f, Screen.currentResolution.width * 0.40f, Screen.currentResolution.height * 0.65f);
        }

        public void OnGUI()
        {
            if (level == null)
            {
                EditorGUILayout.LabelField("Level is null : ");
                return;
            }

            EditorGUILayout.LabelField("Currently displayed level : " + level.name);
            if (GUILayout.Button("Erase")) Erase();

            if (level.matrix.Length > 0)
            {
                int matrixSize = LevelProfile.levelSize;
                int offset = 2;
                int marginTop = 50;
                Vector2 tileSize = new Vector2(
                    (position.width - (matrixSize * offset)) / matrixSize,
                    (position.height - (matrixSize * offset + marginTop)) / matrixSize
                );

                Rect tilePosition;
                Event e = Event.current;

                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        tilePosition = new Rect(
                            j * (tileSize.x + offset) + (offset / 2),
                            i * (tileSize.y + offset) + (offset / 2) + marginTop,
                            tileSize.x,
                            tileSize.y);

                        if (!isMousePressed && e.type == EventType.MouseDown) isMousePressed = true;
                        if (isMousePressed && e.type == EventType.MouseUp) isMousePressed = false;

                        if (tilePosition.Contains(e.mousePosition) && isMousePressed) level.matrix[i * matrixSize + j] = 1;

                        if (level.matrix[i * matrixSize + j] == 1) EditorGUI.DrawRect(tilePosition, Color.red);
                        else EditorGUI.DrawRect(tilePosition, Color.blue);
                    }
                }

                Repaint();
            }

        }

        public void Erase()
        {
            int matrixSize = LevelProfile.levelSize;
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    level.matrix[i * matrixSize + j] = 0;
                }
            }
        }

        private void DrawSprite(Rect position, Sprite sprite)
        {
            // on récupère la taille du sprite, en pixels, dans le référentiel de la texture d'où il est issu
            Vector2 fullSize = new Vector2(sprite.texture.width, sprite.texture.height);
            Vector2 size = new Vector2(sprite.textureRect.width, sprite.textureRect.height);

            // on récupère les coordonnées du sprite au sein de la texture au format UV, c'est à dire entre 0 et 1
            Rect coords = sprite.textureRect;
            coords.x /= fullSize.x;
            coords.width /= fullSize.x;
            coords.y /= fullSize.y;
            coords.height /= fullSize.y;

            // quelle différence d'échelle de taille entre la texture réelle et l'espace qu'on a en GUI pour la dessiner ?
            Vector2 ratio;
            ratio.x = position.width / size.x;
            ratio.y = position.height / size.y;
            float minRatio = Mathf.Min(ratio.x, ratio.y);

            // on corrige la position/taille du rectangle où tracer la texture en GUI
            Vector2 center = position.center;
            position.width = size.x * minRatio;
            position.height = size.y * minRatio;
            position.center = center;

            // enfin, on dessine le morceau de texture correspondant au sprite
            GUI.DrawTextureWithTexCoords(position, sprite.texture, coords, true);
        }

    }
}
