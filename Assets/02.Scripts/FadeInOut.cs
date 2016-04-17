using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour
{

    private RenderTexture renderTex;
    private GameObject Live2D_Quad;
    private GameObject Live2D_Cam;
    private Renderer Quad_render;
    private Camera dummyCam;
    // CameraとLive2DモデルのY軸ずらす
    private float transY = 60.0f;
    // ぼやける場合は2048にする
    public int renderSize = 1024;
    // シーンのLive2Dモデル数
    public static int Live2D_Count = 0;
    // 透明度調整用
    [Range(0.0f, 1.0f)]
    public float opacity = 1.0f;


    void Awake()
    {
        // モデル数カウント
        Live2D_Count++;
        // モデルごとにY軸をずらす
        transY = transY * Live2D_Count;

        // RenderTextureを生成
        //      renderTex = new RenderTexture(renderSize, renderSize, 16, RenderTextureFormat.ARGB32);
        // 一時的なレンダリングテクスチャを割り当てます(迅速にRenderTextureを表示したい場合)
        renderTex = RenderTexture.GetTemporary(renderSize, renderSize, 16, RenderTextureFormat.ARGB32);

        // Quadを生成(RenderTexture描画用)
        Live2D_Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        // Live2Dモデルの座標をセット
        Live2D_Quad.transform.position = gameObject.transform.position;

        // シェーダー指定とRenderTextureをセット
        Quad_render = Live2D_Quad.GetComponent<Renderer>();
        // Unity5.3からUI/Defaultが上手く機能しなくなったのでSpritesにする
        Quad_render.material.shader = Shader.Find("UI/Default");
        Quad_render.material.shader = Shader.Find("Sprites/Default");
        Quad_render.material.SetTexture("_MainTex", renderTex);
        Quad_render.name = gameObject.name + "_Quad";

        // Live2DモデルのY軸をずらす
        gameObject.transform.position = new Vector3(0.0f, transY + gameObject.transform.position.y, 0.0f);

        // Live2Dを映す第2カメラ
        Live2D_Cam = new GameObject("Live2D Camera");
        Live2D_Cam.transform.position = new Vector3(0.0f, transY, -10.0f);
        Live2D_Cam.AddComponent<Camera>();

        // カメラの設定とRenderTextureをセット
        dummyCam = Live2D_Cam.GetComponent<Camera>();
        dummyCam.orthographic = true;
        dummyCam.orthographicSize = 1;
        dummyCam.clearFlags = CameraClearFlags.SolidColor;
        dummyCam.targetTexture = renderTex;

    }

    void Update()
    {
        // QuadとLive2Dモデルサイズを同期
        Live2D_Quad.transform.localScale = gameObject.transform.localScale * 4.0f;
        // orthographicSizeとLive2Dモデルサイズを同期
        dummyCam.orthographicSize = Mathf.Max(gameObject.transform.localScale.x, gameObject.transform.localScale.y) * 2.0f;

        if (Live2D_Quad)
        {
            // Quadの透明度を動的に変更
            Quad_render.material.color = new Color(1.0f, 1.0f, 1.0f, opacity);
        }
    }

    void OnDestroy()
    {
        // Live2DのGameObjectが削除されたらダミーで作ったものも削除
        RenderTexture.ReleaseTemporary(renderTex);
        Destroy(Live2D_Cam);
        Destroy(Live2D_Quad);
        Live2D_Count--;
    }
}