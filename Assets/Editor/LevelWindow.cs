using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Shooter
{
    public class LevelWindow : EditorWindow
    {

        LevelProfile level;
        bool isMousePressed = false;
        int layer = 1;
        int selectedObject = 0;

        public static void Init(LevelProfile level)
        {
            LevelWindow window = EditorWindow.GetWindow(typeof(LevelWindow)) as LevelWindow;

            window.level = level;

            window.Show();
            window.titleContent = new GUIContent(level.name + " editor");
            window.position = new Rect(Screen.currentResolution.width * 0.35f, Screen.currentResolution.height * 0.17f, Screen.currentResolution.width * 0.30f, Screen.currentResolution.height * 0.65f);
        }

        public void OnGUI()
        {
            if (level == null)
            {
                EditorGUILayout.LabelField("Level is null : ");
                return;
            }
            GUIStyle style = new GUIStyle();
            style.fontSize = 25;
            style.normal.textColor = Color.white;
            EditorGUILayout.LabelField("Level Name : " + level.name, style);
            EditorGUILayout.Space(15);
            if (GUILayout.Button("Erase")) Erase();

            Rect spritesRect = EditorGUILayout.GetControlRect();
            spritesRect.y += 8;
            spritesRect.height = 60;
            Event e = Event.current;
            float spriteWidth = (spritesRect.width / (level.objects.Length + 1));
            float spriteHeight = (spritesRect.height);

            Rect eraserSpriteRect = new Rect(spritesRect.x, spritesRect.y, spriteWidth, spriteHeight);
            GUI.DrawTextureWithTexCoords(eraserSpriteRect, level.eraser, new Rect(0, 0, 1, 1), true);
            if (eraserSpriteRect.Contains(e.mousePosition) && isMousePressed) selectedObject = -1;

            for (int i = 0; i < level.objects.Length; i++) {
                Sprite s = level.objects[i].GetComponent<SpriteRenderer>().sprite;
                Rect spriteRect = new Rect(spritesRect.x + (i + 1) * spriteWidth, spritesRect.y, spriteWidth,spriteHeight);
                DrawSprite(spriteRect, level.objects[i].GetComponent<SpriteRenderer>().sprite);

                if (!isMousePressed && e.type == EventType.MouseDown) isMousePressed = true;
                if (isMousePressed && e.type == EventType.MouseUp) isMousePressed = false;

                if (spriteRect.Contains(e.mousePosition) && isMousePressed) {
                    selectedObject = i;
                }
            }

            int layerSize = LevelProfile.levelSize;
            int offset = 2;
            int marginTop = 145;
            Vector2 tileSize = new Vector2(
                (position.width - (layerSize * offset)) / layerSize,
                (position.height - (layerSize * offset + marginTop)) / layerSize
            );

            Rect tilePosition;

            for (int i = 0; i < layerSize; i++)
            {
                for (int j = 0; j < layerSize; j++)
                {
                    tilePosition = new Rect(
                        j * (tileSize.x + offset) + (offset / 2),
                        i * (tileSize.y + offset) + (offset / 2) + marginTop,
                        tileSize.x,
                        tileSize.y);

                    if (tilePosition.Contains(e.mousePosition)) 
                    {
                        if (selectedObject > 0) DrawSprite(tilePosition, level.objects[selectedObject].GetComponent<SpriteRenderer>().sprite);
                        if (isMousePressed) {
                            if (selectedObject >= 0) {
                                level.layer1[i * layerSize + j] = level.objects[selectedObject];
                            } else {
                                level.layer1[i * layerSize + j] = null;
                            }
                        }
                    }

                    EditorGUI.DrawRect(tilePosition, new Color(1, 1, 1, 0.2f));
                    if (level.layer1[i * layerSize + j] != null) DrawSprite(tilePosition, level.layer1[i * layerSize + j].GetComponent<SpriteRenderer>().sprite);
                    else EditorGUI.DrawRect(tilePosition, new Color(0, 0, 0, 0));
                }
            }
            Repaint();
        }

        public void Erase()
        {
            int layerSize = LevelProfile.levelSize;
            for (int i = 0; i < layerSize; i++)
            {
                for (int j = 0; j < layerSize; j++)
                {
                    level.layer1[i * layerSize + j] = null;
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
