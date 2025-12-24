using UnityEngine;

public class LuminolLightSender : MonoBehaviour
{
    public Light uvLight;
    public Renderer targetRenderer; // 피 흔적 메시의 렌더러

    private Material targetMaterial;

    // 셰이더 프로퍼티 ID (미리 캐싱하여 성능 최적화)
    private readonly int LightPosID = Shader.PropertyToID("_LightPositionWS");
    private readonly int LightDirID = Shader.PropertyToID("_LightDirectionWS");
    private readonly int LightRangeID = Shader.PropertyToID("_LightRange");
    private readonly int LightAngleID = Shader.PropertyToID("_LightSpotAngle");

    void Start()
    {
        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
        }
    }

    void Update()
    {
        if (uvLight != null && targetMaterial != null)
        {
            // 1. 위치와 방향 전달
            targetMaterial.SetVector(LightPosID, uvLight.transform.position);
            targetMaterial.SetVector(LightDirID, -uvLight.transform.forward); // 라이트 방향의 반대 (관습적으로 라이트가 향하는 방향)

            // 2. 범위 및 각도 전달
            targetMaterial.SetFloat(LightRangeID, uvLight.range);
            // Spot Angle의 코사인 값을 전달하여 셰이더에서 계산을 단순화할 수 있습니다.
            targetMaterial.SetFloat(LightAngleID, Mathf.Cos(uvLight.spotAngle * 0.5f * Mathf.Deg2Rad));
        }
    }
}